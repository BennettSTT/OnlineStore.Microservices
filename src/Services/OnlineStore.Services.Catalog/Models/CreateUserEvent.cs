using OnlineStore.Services.Infrastructure.Models;

namespace OnlineStore.Services.Catalog.Models
{
    public class CreateUserEvent : EventBase
    {
        public string FirstName { get; set; }
    }
}