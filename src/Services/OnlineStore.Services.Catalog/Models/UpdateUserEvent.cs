using OnlineStore.Services.Infrastructure.Models;

namespace OnlineStore.Services.Catalog.Models
{
    public class UpdateUserEvent : EventBase
    {
        public string FirstName { get; set; }
    }
}