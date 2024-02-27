using System.IdentityModel.Tokens.Jwt;
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
        public async Task<bool> Login(LoginRequest obj)
        {
            var result = await Program.httpClient_auth.PostAsJsonAsync("/api/Accounts/Login", obj);
            var jsonString = await result.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(jsonString,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (loginResponse!.Status == 500)
            {
                return false!;
            }
            await _localStorage.SetItemAsync("authToken", loginResponse!.Data);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(obj.Email!);
            Program.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data);
            Program.httpClient_server.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Data);
            //await GetUserRole();
            return true;
        }
        public async Task<bool> Register(RegisterRequest obj)
        {
            var result = await Program.httpClient_auth.PostAsJsonAsync("/api/Accounts/register", obj);
            var jsonString = await result.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<RegisterResponse>(jsonString,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            if (!response!.Successful)
            {
                return false!;
            }
            return true;
        }
        public async Task LogOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            Program.httpClient_auth.DefaultRequestHeaders.Authorization = null;
            Program.httpClient_server.DefaultRequestHeaders.Authorization = null;
        }
        //public async Task GetUserRole()
        //{
        //    var token = await _localStorage.GetItemAsync<string>("authToken");
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        //        var roleClaim = tokenS!.Claims.FirstOrDefault(claim => claim.Type.ToLower() == "role");
        //        await _localStorage.SetItemAsync("roleAuth", roleClaim?.Value);
        //    }
        //}
    }
}