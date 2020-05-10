using OnlineStore.Services.Infrastructure.Interfaces;
using OnlineStore.Services.Infrastructure.Models;
using System.Threading.Tasks;

namespace OnlineStore.Services.Infrastructure.Implementation
{
    public abstract class EventBusHandlerBase<TEvent> : IEventBusHandler<TEvent>
        where TEvent : EventBase
    {
        public abstract Task HandlerAsync(TEvent @event);

        public virtual string GetRoutingKey()
        {
            return typeof(TEvent).Name;
        }
    }
}