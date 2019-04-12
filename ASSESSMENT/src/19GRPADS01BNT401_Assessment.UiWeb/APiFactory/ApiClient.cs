using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using _19GRPADS01BNT401_Assessment.UiWeb.Models;
using System.Net.Http.Headers;
using static System.Convert;
namespace _19GRPADS01BNT401_Assessment.UiWeb.APiFactory
{
    public partial class ApiClient
    {
        private readonly HttpClient _httpClient;
        private Uri BaseEndpoint { get; }

        public ApiClient(Uri baseEndpoint)
        {
            if (baseEndpoint == null) throw new ArgumentException("basepoint");
            BaseEndpoint = baseEndpoint;
            _httpClient = new HttpClient();
        }
        private async Task<T> GetAsync<T>(Uri requestUrl)
        {
            AddHeaders();
            var response = await _httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        private async Task<Message<T>> PostAsync<T>(Uri requestUrl, T content)
        {
            AddHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            
            var data = await response.Content.ReadAsStringAsync();
            var message = JsonConvert.DeserializeObject<Message<T>>(data);
            message.IsSuccess = response.IsSuccessStatusCode;
            return message;
        }
        private async Task<Message<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            AddHeaders();
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }

        private async Task<Message<T>> PutAsync<T>(Uri requestUrl, T content)
        {
            AddHeaders();
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T>>(data);
        }
        private async Task<Message<T1>> PutAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            AddHeaders();
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Message<T1>>(data);
        }

        private async Task DeleteAsync(Uri requestUrl)
        {
            AddHeaders();
            var response = await _httpClient.DeleteAsync(requestUrl);
            response.EnsureSuccessStatusCode();

            await response.Content.ReadAsStringAsync();
        }

        private Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings =>
            new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };

        private void AddHeaders()
        {
            var userAuth = "samuel:demo";
            _httpClient.DefaultRequestHeaders.Authorization = new
                AuthenticationHeaderValue("Basic", ToBase64String(Encoding.ASCII.GetBytes(userAuth)));

        }
    }
}