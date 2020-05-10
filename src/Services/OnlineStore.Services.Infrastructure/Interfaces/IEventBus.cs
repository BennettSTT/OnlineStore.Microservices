using OnlineStore.Services.Infrastructure.Models;

namespace OnlineStore.Services.Infrastructure.Interfaces
{
    public interface IEventBus
    {
        void Publish(EventBase @event);

        void Subscribe<TRoutingKey, THandler>()
            where TRoutingKey : EventBase, new()
            where THandler : IEventBusHandler<TRoutingKey>, new();
    }
}