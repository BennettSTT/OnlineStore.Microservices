using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.Infrastructure.Interfaces;
using OnlineStore.Services.Infrastructure.Models;
using OnlineStore.Services.Users.Models;
using System.Net;

namespace OnlineStore.Services.Users.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public UserController(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpGet]
        [Route("info/{id:int}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserInfo), (int) HttpStatusCode.OK)]
        public ActionResult<UserInfo> GetInfoById(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            _eventBus.Publish(new CreateUserEvent
            {
                FirstName = "Anna"
            });

            var info = new UserInfo
            {
                FirstName = "Anna",
                LastName = "Rog"
            };

            return info;
        }

        public class CreateUserEvent : EventBase
        {
            public string FirstName { get; set; }
        }
    }
}