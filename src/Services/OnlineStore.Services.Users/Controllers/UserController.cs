using Microsoft.AspNetCore.Mvc;
using OnlineStore.Services.Users.Models;
using System.Net;

namespace OnlineStore.Services.Users.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("info/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserInfo), (int)HttpStatusCode.OK)]
        public ActionResult<UserInfo> GetInfoById(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            
            var info = new UserInfo
            {
                FirstName = "Anna",
                LastName = "Rog"
            };

            return info;
        }
    }
}