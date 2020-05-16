using OnlineStore.Services.Infrastructure.Models;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace OnlineStore.Services.Infrastructure.Interfaces
{
    public interface IEventBusHandler<in TEvent>
        where TEvent : EventBase
    {
        Task HandlerAsync([NotNull] TEvent @event);
    }
}