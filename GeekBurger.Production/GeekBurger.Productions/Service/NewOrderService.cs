using GeekBurger.Orders.Contract.Enums;
using GeekBurger.Orders.Contract.Messages;
using GeekBurger.Productions.Model;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
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
        private const string SubscriptionName = "ProductionAreaSubscription";
        private readonly ILogService _logService;

        public NewOrderService(IConfiguration configuration, IOrderChangedService orderChangedService, ILogService logService)
        {
            _configuration = configuration;
            _namespace = _configuration.GetServiceBusNamespace();
            _orderChangedService = orderChangedService;
            _logService = logService;
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

            try
            {
                var rules = await subscriptionClient.GetRulesAsync();

                if (rules.Any(x => x.Name == "$Default"))
                {
                    //await subscriptionClient.RemoveRuleAsync("$Default");
                }

                var messageHandlerOptions = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

                _logService.SendMessagesAsync("New Order Received in Production");

                subscriptionClient.RegisterMessageHandler(Handle, messageHandlerOptions);
            }
            catch(Exception)
            {

            }
        }

        private static Task Handle(Message message, CancellationToken arg2)
        {
            var newOrderString = Encoding.UTF8.GetString(message.Body);
            var newOrder = JsonConvert.DeserializeObject<OrderChangedMessage>(newOrderString);

            OrderChangedMessage orderReadyMessage = new OrderChangedMessage
            {
                OrderId = new Guid(message.Label),
                State = (OrderState)ProductionState.Ready,
                Value = newOrder.Value,
                StoreId = newOrder.StoreId
            };

            Random readyTime = new Random();
            int seconds = readyTime.Next(5 * 1000, 21 * 1000);
            Thread.Sleep(seconds);

            // TODO: persisitir a informação de READY para validação após o pagamento do pedido

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
