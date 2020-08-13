using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public class AuthorizedHttpClientFactory : IAuthorizedHttpClientFactory
    {
        readonly IWebAssemblyHostEnvironment env;
        readonly IAuthService authService;

        public AuthorizedHttpClientFactory(IWebAssemblyHostEnvironment env, IAuthService authService)
        {
            this.env = env;
            this.authService = authService;
        }

        public async Task<HttpClient> CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(env.BaseAddress)
            };
            client.DefaultRequestHeaders.Authorization = await authService.GetAuthToken();
            return client;
        }
    }
}
