using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Ui.Services
{
    public class GenericApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _httpContextAccessor = httpContextAccessor;

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        // ✅ يضيف AccessToken للهيدر لو موجود في الكوكي
        private void AddAuthorizationHeader(string endpoint)
        {
            if (endpoint.Contains("auth/login", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Skipping token for: " + endpoint);  // ✅ لازم يظهر دا
                return;
            }

            var accessToken = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
            Console.WriteLine("Access Token: " + accessToken);  // ✅ هل بيظهر null؟

            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", accessToken);
            }
        }



        // ✅ GET
        public async Task<T> GetAsync<T>(string endpoint)
        {
            AddAuthorizationHeader(endpoint);
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        // ✅ POST
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            AddAuthorizationHeader(endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Response: {errorContent}");
                throw new HttpRequestException($"Error {response.StatusCode}: {errorContent}");
            }

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        // ✅ PUT
        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            AddAuthorizationHeader(endpoint);
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        // ✅ DELETE
        public async Task DeleteAsync(string endpoint)
        {
            AddAuthorizationHeader(endpoint);
            var response = await _httpClient.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
        }
    }
}
