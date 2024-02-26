using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Client.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Models.RequestModel;
using Models.ResponseModel;

namespace Client.Services
{
    public class AuthServices
    {
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthServices(ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<LoginResponse> Login(LoginRequest obj)
        {
            var result = await Program.httpClient_auth.PostAsJsonAsync("/api/Accounts/Login", obj);
            var jsonString = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(jsonString,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (!result.IsSuccessStatusCode)
            {
                return loginResponse!;
            }
            await _localStorage.SetItemAsync("authToken", loginResponse!.Data);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(obj.Email!);
            Program.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data);
            return loginResponse;
        }
        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            Program.httpClient_auth.DefaultRequestHeaders.Authorization = null;
            Program.httpClient_server.DefaultRequestHeaders.Authorization = null;
        }
    }
}