using System.Net.Http;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public interface IAuthorizedHttpClientFactory
    {
        Task<HttpClient> CreateClient();
    }
}