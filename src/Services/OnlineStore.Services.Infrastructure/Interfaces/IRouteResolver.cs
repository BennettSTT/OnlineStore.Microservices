using OnlineStore.Services.Infrastructure.Models;

namespace OnlineStore.Services.Infrastructure.Interfaces
{
    public interface IRouteResolver
    {
        string Resolve<TEvent>()
            where TEvent : EventBase;
    }
}