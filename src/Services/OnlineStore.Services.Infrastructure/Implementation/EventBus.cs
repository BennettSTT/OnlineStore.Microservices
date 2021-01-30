using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OnlineStore.Services.Infrastructure.Interfaces;
using OnlineStore.Services.Infrastructure.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Services.Infrastructure.Implementation
{
    public class EventBus : IEventBus
    {
        private const string ExchangeName = Constants.EventBus.Exchange.Name.Default;
        private const string ExchangeType = Constants.EventBus.Exchange.Type.Direct;

        private readonly IEventBusConnectionManager _eventBusConnectionManager;
        private readonly IRouteResolver _routeResolver;
        private readonly IServiceProvider _serviceProvider;

        private readonly IModel _consumerChannel;

        public EventBus(
            [NotNull] IEventBusConnectionManager eventBusConnectionManager,
            [NotNull] IRouteResolver routeResolver,
            [NotNull] IServiceProvider serviceProvider)
        {
            _eventBusConnectionManager = eventBusConnectionManager;
            _routeResolver = routeResolver;
            _serviceProvider = serviceProvider;
            _consumerChannel = CreateConsumerChannel();
        }

        public void Publish<TEvent>([NotNull] TEvent @event)
            where TEvent : EventBase
        {
            var route = _routeResolver.Resolve<TEvent>();

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            _consumerChannel.BasicPublish(ExchangeName, route, true, null, body);
        }

        public void Subscribe<THandler, TEvent>()
            where THandler : IEventBusHandler<TEvent>
            where TEvent : EventBase
        {
            var route = _routeResolver.Resolve<TEvent>();

            _consumerChannel.QueueDeclare(route, false, false, false, null);
            _consumerChannel.QueueBind(route, ExchangeName, route);

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

            consumer.Received += ConsumerOnReceivedAsync<THandler, TEvent>;

            _consumerChannel.BasicConsume(route, false, consumer);
        }

        private async Task ConsumerOnReceivedAsync<THandler, TEvent>(
            [NotNull] object sender,
            [NotNull] BasicDeliverEventArgs eventArgs)
            where THandler : IEventBusHandler<TEvent>
            where TEvent : EventBase
        {
            var body = eventArgs.Body.ToArray();

            if (TryDeserializeEvent(body, out TEvent @event))
            {
                var handler = _serviceProvider.GetService<THandler>();

                await handler.HandlerAsync(@event);
            }
        }

        private static bool TryDeserializeEvent<TEvent>(
            [NotNull] byte[] body,
            [NotNull] out TEvent @event)
            where TEvent : EventBase
        {
            @event = null;

            var message = Encoding.UTF8.GetString(body);

            try
            {
                @event = JsonConvert.DeserializeObject<TEvent>(message);
            }
            catch (JsonReaderException)
            {
                // TODO: log error
                return false;
            }

            return true;
        }

        private IModel CreateConsumerChannel()
        {
            var channel = _eventBusConnectionManager.CreateChannel();

            channel.ExchangeDeclare(ExchangeName, ExchangeType);

            return channel;
        }
    }
}