using OnlineStore.Services.Catalog.Models;
using OnlineStore.Services.Infrastructure.Implementation;
using System;
using System.Threading.Tasks;

namespace OnlineStore.Services.Catalog.EventBusHandlers
{
    public class UpdateUserHandler : EventBusHandlerBase<UpdateUserEvent>
    {
        public override Task HandlerAsync(UpdateUserEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}