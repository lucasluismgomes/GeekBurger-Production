using GeekBurger.Orders.Contract.Messages;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Productions.Service
{
    public class NewOrderService : INewOrderService
    {
        private IConfiguration _configuration;
        private IServiceBusNamespace _namespace;
        private static ServiceBusConfiguration serviceBusConfiguration;
        private static IOrderChangedService _orderChangedService;
        private const string SubscriptionName = "NewOrder";

        public NewOrderService(IConfiguration configuration, IOrderChangedService orderChangedService)
        {
            _configuration = configuration;
            _namespace = _configuration.GetServiceBusNamespace();
            _orderChangedService = orderChangedService;
        }
        
        public void SubscribeToTopic(string topicName)
        {
            var topic = _namespace.Topics.GetByName(topicName);

            topic.Subscriptions.DeleteByName(SubscriptionName);

            if (!topic.Subscriptions.List().Any(subscription => subscription.Name.Equals(SubscriptionName, StringComparison.InvariantCultureIgnoreCase)))
            {
                topic.Subscriptions.Define(SubscriptionName).Create();
            }

            ReceiveMessages(topicName);
        }

        private async void ReceiveMessages(string topicName)
        {
            serviceBusConfiguration = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var subscriptionClient = new SubscriptionClient(serviceBusConfiguration.ConnectionString, topicName, SubscriptionName);

            await subscriptionClient.RemoveRuleAsync("$Default");
            
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

            subscriptionClient.RegisterMessageHandler(Handle, messageHandlerOptions);

            Console.ReadLine();
        }

        private static Task Handle(Message message, CancellationToken arg2)
        {
            OrderChangedMessage orderFinishedMessage = new OrderChangedMessage
            {
                OrderId = Int32.Parse(message.Label)
            };

            Random waitTime = new Random();
            int seconds = waitTime.Next(5 * 1000, 21 * 1000);

            Thread.Sleep(seconds);

            _orderChangedService.AddToMessageList(orderFinishedMessage);
            _orderChangedService.SendMessagesAsync();

            Thread.Sleep(10000);

            return Task.CompletedTask;
        }

        private static Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
