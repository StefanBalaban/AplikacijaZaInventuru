using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WinUI.Models;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;

namespace WinUI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private string? _accessToken;

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri($"{Program.Configuration["ConnectionStrings:IdentityServer"]}");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<string> LoginAsync(string username, string password)
        {
            var authRequest = new AuthRequest
            {
                Username = username,
                Password = password
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(authRequest), Encoding.UTF8, Application.Json);

            var httpResponseMessage = await _httpClient.PostAsync("api/auth/login", content);

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "";
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {httpResponseMessage.ReasonPhrase} - {await httpResponseMessage.Content.ReadAsStringAsync()}");
            }

            var body = await httpResponseMessage.Content.ReadAsStringAsync();

            if (body != null)
            {
                _accessToken = JsonConvert.DeserializeObject<AuthResponse>(body)?.Token;

                if (string.IsNullOrEmpty(_accessToken))
                {
                    throw new Exception("Error: Token missing from response body");
                }

                return _accessToken;
            }


            throw new Exception("Error: Missing response body");


        }

        public string GetAccessToken()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                throw new UnauthorizedAccessException("Error: Missing token");
            }
            var token = new JwtSecurityTokenHandler().ReadToken(_accessToken);
            var expDate = token.ValidTo;
            if (expDate < DateTime.UtcNow.AddMinutes(1))
            {
                throw new UnauthorizedAccessException("Error: Expired token");
            }
            return _accessToken;
        }
    }
}
