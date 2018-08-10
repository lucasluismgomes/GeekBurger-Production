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
    public class OrderPaidService : IOrderPaidService
    {
        private IConfiguration _configuration;
        private IServiceBusNamespace _namespace;
        private static ServiceBusConfiguration serviceBusConfiguration;
        private static IOrderChangedService _orderChangedService;
        private const string SubscriptionName = "ProductionAreaSubscription";
        private readonly ILogService _logService;

        public OrderPaidService(IConfiguration configuration, IOrderChangedService orderChangedService, ILogService logService)
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
                    await subscriptionClient.RemoveRuleAsync("$Default");
                }

                var messageHandlerOptions = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

                _logService.SendMessagesAsync("Order Changed Received in Production");

                subscriptionClient.RegisterMessageHandler(Handle, messageHandlerOptions);
            }
            catch (Exception)
            {

            }
        }

        private static Task Handle(Message message, CancellationToken arg2)
        {
            var orderChangedString = Encoding.UTF8.GetString(message.Body);
            var orderChanged = JsonConvert.DeserializeObject<OrderChangedMessage>(orderChangedString);

            if (orderChanged.State == OrderState.Paid)
            {
                OrderChangedMessage orderFinishedMessage = new OrderChangedMessage
                {
                    OrderId = new Guid(message.Label),
                    State = (OrderState)ProductionState.Finished,
                    Valor = orderChanged.Valor,
                    StoreId = orderChanged.StoreId
                };

                // TODO: atribuir como status concluído apenas se o status de produção for READY

                _orderChangedService.AddToMessageList(orderFinishedMessage);
                _orderChangedService.SendMessagesAsync();
            }

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
