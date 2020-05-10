using Grpc.Core;
using GRPCCatalog;
using System;
using System.Threading.Tasks;

namespace OnlineStore.Services.Catalog.Services
{
    public class CatalogService : GRPCCatalog.Catalog.CatalogBase
    {
        public override Task<CatalogItemResponse> GetInfoBy(CatalogItemRequest request, ServerCallContext context)
        {
            var random = new Random();
            
            var response = new  CatalogItemResponse
            {
                Id = request.Id,
                Description = "This is description",
                Title = "Title",
                Price = random.Next()
            };

            return Task.FromResult(response);
        }
    }
}