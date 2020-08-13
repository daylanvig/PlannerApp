using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace PresentationClient.Services
{
    public interface IApiAuthStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        void MarkUserAsAuthenticated(string email);
        void MarkUserAsLoggedOut();
    }
}