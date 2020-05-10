using OnlineStore.Services.Infrastructure.Models;
using System.Threading.Tasks;

namespace OnlineStore.Services.Infrastructure.Interfaces
{
    public interface IEventBusHandler<in TEvent>
        where TEvent : EventBase
    {
        Task HandlerAsync(TEvent @event);

        string GetRoutingKey();
    }
}