using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace GeekBurger.Production.Service
{
    public class ProductionAreaChangedService : IProductionAreaChangedService
    {
        private const string Topic = "ProductionChangedTopic";
        private IConfiguration _configuration;
        private IMapper _mapper;
        private List<Message> _messages;
        private Task _lastTask;
        private IServiceBusNamespace _namespace;

        public ProductionAreaChangedService(IMapper mapper, IConfiguration configuration)
        {
            _mapper = mapper;
            _configuration = configuration;
            _messages = new List<Message>();
            _namespace = _configuration.GetServiceBusNamespace();
            EnsureTopicIsCreated();
        }

        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic =>
                    topic.Name.Equals(Topic, StringComparison.InvariantCultureIgnoreCase)))
            {
                _namespace.Topics.Define(Topic)
                    .WithSizeInMB(1024).Create();
            }
        }

        public void AddToMessageList(IEnumerable<EntityEntry<Production>> changes)
        {
            
        }

        public void SendMessagesAsync()
        {
            
        }
    }
}
