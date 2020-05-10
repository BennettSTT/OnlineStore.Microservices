using Newtonsoft.Json;
using OnlineStore.Services.Infrastructure.Interfaces;
using OnlineStore.Services.Infrastructure.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace OnlineStore.Services.Infrastructure.Implementation
{
    public class EventBus : IEventBus
    {
        private const string EventBusName = "OnlineStoreEventBus";
        private readonly IModel _consumerChannel;

        private readonly IEventBusConnectionManager _eventBusConnectionManager;

        public EventBus(IEventBusConnectionManager eventBusConnectionManager)
        {
            _eventBusConnectionManager = eventBusConnectionManager;
            _consumerChannel = CreateConsumerChannel();
        }

        public void Publish(EventBase @event)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<TRoutingKey, THandler>()
            where TRoutingKey : EventBase, new()
            where THandler : IEventBusHandler<TRoutingKey>, new()
        {
            var handler = new THandler();

            var queueName = handler.GetRoutingKey();

            _consumerChannel.QueueDeclare(
                queueName,
                false,
                false,
                false,
                null);

            _consumerChannel.QueueBind(
                queueName,
                EventBusName,
                queueName);

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var eventType = typeof(TRoutingKey);

                var integrationEvent = (TRoutingKey) JsonConvert.DeserializeObject(message, eventType);

                await handler.HandlerAsync(integrationEvent);
            };

            _consumerChannel.BasicConsume(queueName, false, consumer);
        }

        private IModel CreateConsumerChannel()
        {
            var channel = _eventBusConnectionManager.CreateChannel();

            channel.ExchangeDeclare(EventBusName, ExchangeType.Direct);

            return channel;
        }
    }
}