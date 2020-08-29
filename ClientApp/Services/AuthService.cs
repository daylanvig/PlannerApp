using Application.Accounts.Commands.RegisterNewUser;
using Application.Accounts.Commands.SignInUser;
using Blazored.LocalStorage;
using ClientApp.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public class AuthService : IAuthService
    {
        private const string tokenStore = "authToken";
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            
            var result = await _httpClient.PostJSONAsync<RegisterModel, RegisterResult>("api/Accounts", registerModel);
            return result.Response;
        }

        public async Task<SignInUserResult> Login(SignInUserModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PutAsync("api/Accounts/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<SignInUserResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync(tokenStore, loginResult.Token);
            ((ApiAuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(tokenStore);
            ((ApiAuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<AuthenticationHeaderValue> GetAuthToken()
        {
            var tokenValue = await _localStorage.GetItemAsync<string>(tokenStore);
            return new AuthenticationHeaderValue("bearer", tokenValue);
        }
    }
}
