using System.Net.Http;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public interface IAuthorizedHttpClientFactory
    {
        Task<HttpClient> CreateClient();
    }
}