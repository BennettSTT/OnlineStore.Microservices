using GRPCCatalog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineStore.Gateways.Web.HttpAggregator.Infrastructure;
using OnlineStore.Gateways.Web.HttpAggregator.Models;
using System.Threading.Tasks;

namespace OnlineStore.Gateways.Web.HttpAggregator.Services
{
    internal class CatalogService : GrpcServiceBase, ICatalogService
    {
        private readonly IOptions<UrlsOption> _options;

        public CatalogService(
            IOptions<UrlsOption> options,
            ILoggerFactory loggerFactory) 
            : base(loggerFactory)
        {
            _options = options;
        }
        
        public async Task<CatalogInfo> GetInfoByAsync(long id)
        {
            return await CallService(_options.Value.Catalog, async channel =>
            {
                var client = new Catalog.CatalogClient(channel);
                var request = new CatalogItemRequest { Id = id };
                var response = await client.GetInfoByAsync(request);

                return new CatalogInfo
                {
                    Title = response.Title
                };
            });
        }
    }
}