using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PresentationClient.Services
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
