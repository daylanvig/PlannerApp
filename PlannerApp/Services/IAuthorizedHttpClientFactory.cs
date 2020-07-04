using System.Net.Http;
using System.Threading.Tasks;

namespace PlannerApp.Client.Services
{
    public interface IAuthorizedHttpClientFactory
    {
        Task<HttpClient> CreateClient();
    }
}