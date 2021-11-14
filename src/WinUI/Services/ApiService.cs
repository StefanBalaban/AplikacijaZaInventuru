using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WinUI.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;
        private bool _clientSet = false;

        public ApiService(IHttpClientFactory httpClientFactory, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _authService = authService;
            _httpClient = _httpClientFactory.CreateClient();
        }

        public async Task<T> GetAsync<T>(string path)
        {
            InitiateClient();

            using var httpResponseMessage = await _httpClient.GetAsync($"api/{path}");

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {httpResponseMessage.ReasonPhrase} - {await httpResponseMessage.Content.ReadAsStringAsync()}");
            }

            var body = await httpResponseMessage.Content.ReadAsStringAsync();

            if (body != null)
            {
                return JsonConvert.DeserializeObject<T>(body);
            }

            throw new Exception("Error: Missing response body");


        }

        private void InitiateClient()
        {
            if (!_clientSet)
            {
                _httpClient.BaseAddress = new Uri($"{Program.Configuration["ConnectionStrings:Api"]}");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authService.GetAccessToken());
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _clientSet = true;
            }
            
        }
    }
}
