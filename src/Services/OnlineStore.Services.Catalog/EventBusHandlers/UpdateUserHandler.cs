using OnlineStore.Services.Catalog.Models;
using OnlineStore.Services.Infrastructure.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace OnlineStore.Services.Catalog.EventBusHandlers
{
    public class UpdateUserHandler : IEventBusHandler<UpdateUserEvent>
    {
        public Task HandlerAsync([NotNull] UpdateUserEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}