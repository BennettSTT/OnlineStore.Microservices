using Grpc.Core;
using GRPCCatalog;
using System.Threading.Tasks;

namespace OnlineStore.Services.Catalog.Services
{
    public class CatalogService : GRPCCatalog.Catalog.CatalogBase
    {
        public override Task<CatalogInfoResponse> GetInfoBy(CatalogInfoRequest request, ServerCallContext context)
        {
            var response = new CatalogInfoResponse
            {
                Title = "Catalog info"
            };

            return Task.FromResult(response);
        }
    }
}