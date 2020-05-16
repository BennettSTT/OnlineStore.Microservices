using OnlineStore.Services.Catalog.Models;
using OnlineStore.Services.Infrastructure.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace OnlineStore.Services.Catalog.EventBusHandlers
{
    public class CreateUserHandler : IEventBusHandler<CreateUserEvent>
    {
        public Task HandlerAsync([NotNull] CreateUserEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}