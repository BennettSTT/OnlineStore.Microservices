using OnlineStore.Services.Catalog.Models;
using OnlineStore.Services.Infrastructure.Implementation;
using System;
using System.Threading.Tasks;

namespace OnlineStore.Services.Catalog.EventBusHandlers
{
    public class CreateUserHandler : EventBusHandlerBase<CreateUserEvent>
    {
        public override Task HandlerAsync(CreateUserEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}