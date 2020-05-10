using Microsoft.AspNetCore.Mvc;
using OnlineStore.Gateways.Web.HttpAggregator.Models;
using OnlineStore.Gateways.Web.HttpAggregator.Services;
using System.Net;
using System.Threading.Tasks;

namespace OnlineStore.Gateways.Web.HttpAggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("info/{id:long}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CatalogInfo), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<CatalogInfo>> GetInfoById(long id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            return await _catalogService.GetInfoByAsync(id);
        }
    }
}