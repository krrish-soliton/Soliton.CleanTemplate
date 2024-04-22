using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Soliton.Shared.TestExtensions
{

    public class HttpTestClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public HttpTestClient(string baseUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }

        public async Task<HttpResponseMessage> GetAsync(string apiEndPoint)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiEndPoint);
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string apiEndPoint)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(apiEndPoint);
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync<TRequest>(string apiEndPoint, TRequest requestData)
        {
            string serializedRequestData = JsonSerializer.Serialize(requestData);
            HttpContent contentData = new StringContent(serializedRequestData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(apiEndPoint, contentData);
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync<TRequest>(string apiEndPoint, TRequest requestData)
        {
            string serializedRequestData = JsonSerializer.Serialize(requestData);
            HttpContent contentData = new StringContent(serializedRequestData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(apiEndPoint, contentData);
            return response;
        }

        public async Task<HttpResponseMessage> PatchAsync<TRequest>(string apiEndPoint, TRequest requestData)
        {
            string serializedRequestData = JsonSerializer.Serialize(requestData);
            HttpContent contentData = new StringContent(serializedRequestData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PatchAsync(apiEndPoint, contentData);
            return response;
        }

        public async Task<T> GetAsync<T>(string apiEndPoint)
        {
            HttpResponseMessage response = await GetAsync(apiEndPoint);
            string responseString = await response.Content.ReadAsStringAsync();
            T? responseModel = JsonSerializer.Deserialize<T>(responseString, options);
            return responseModel!;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string apiEndPoint, TRequest requestData)
        {
            HttpResponseMessage response = await PostAsync<TRequest>(apiEndPoint, requestData);
            string responseString = await response.Content.ReadAsStringAsync();
            TResponse? responseModel = JsonSerializer.Deserialize<TResponse>(responseString, options);
            return responseModel!;
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string apiEndPoint, TRequest requestData)
        {
            HttpResponseMessage response = await PutAsync(apiEndPoint, requestData);
            string responseString = await response.Content.ReadAsStringAsync();
            TResponse? responseModel = JsonSerializer.Deserialize<TResponse>(responseString, options);
            return responseModel!;
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}