using OnlineStore.Services.Infrastructure.Models;
using System.Diagnostics.CodeAnalysis;

namespace OnlineStore.Services.Infrastructure.Interfaces
{
    public interface IEventBus
    {
        void Publish<TEvent>([NotNull] TEvent @event)
            where TEvent : EventBase;

        void Subscribe<THandler, TEvent>()
            where THandler : IEventBusHandler<TEvent>
            where TEvent : EventBase;
    }
}