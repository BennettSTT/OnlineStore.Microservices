using OnlineStore.Gateways.Web.HttpAggregator.Models;
using System.Threading.Tasks;

namespace OnlineStore.Gateways.Web.HttpAggregator.Services
{
    public interface ICatalogService
    {
        Task<CatalogInfo> GetInfoByAsync(long id);
    }
}