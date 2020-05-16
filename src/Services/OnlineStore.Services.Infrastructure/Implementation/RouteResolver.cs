using OnlineStore.Services.Infrastructure.Interfaces;
using OnlineStore.Services.Infrastructure.Models;

namespace OnlineStore.Services.Infrastructure.Implementation
{
    public class RouteResolver : IRouteResolver
    {
        public string Resolve<TEvent>()
            where TEvent : EventBase
        {
            return typeof(TEvent).Name;
        }
    }
}