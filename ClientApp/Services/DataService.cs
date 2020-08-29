using AngleSharp;
using ClientApp.Models.HTTP;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public class DataService
    {
        private readonly IAuthorizedHttpClientFactory authorizedHttpClientFactory;
        public DataService(IAuthorizedHttpClientFactory authorizedHttpClientFactory)
        {
            this.authorizedHttpClientFactory = authorizedHttpClientFactory;
        }

        protected async Task<HttpClient> GetClient()
        {
            return await authorizedHttpClientFactory.CreateClient();
        }
    }
}
