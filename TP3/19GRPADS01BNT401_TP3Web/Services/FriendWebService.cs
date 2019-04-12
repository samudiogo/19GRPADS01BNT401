using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using _19GRPADS01BNT401_TP3Web.Models;

namespace _19GRPADS01BNT401_TP3Web.Services
{
    class FriendWebService : IFriendWebService
    {
        private string ApiBasePath = "https://19grpads01bnt401tp3api.azurewebsites.net/";
        private static string _bearerToken = string.Empty;
        private const int LimiteTentativa = 4;

        public static readonly JsonSerializerSettings Configuracoes = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new IsoDateTimeConverter
                {
                    DateTimeStyles = DateTimeStyles.AssumeUniversal
                }
            }
        };

        public async Task<HttpStatusCode> Login()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBasePath);
                client.DefaultRequestHeaders.Clear();
                var login = new { UserName = "samudiogo", Password = "nocdilos" };

                var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{ApiBasePath}/api/users/authenticate", content);

                if (!response.IsSuccessStatusCode) return response.StatusCode;

                var tokenResult = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
                _bearerToken = tokenResult["token"];

                return HttpStatusCode.OK;
            }
        }
        public async Task<FriendViewModel> GetAsync(Guid id)
        {
            if (!string.IsNullOrEmpty(_bearerToken))
                await Login();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBasePath);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"{ApiBasePath}/api/friends/{id}");
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<FriendViewModel>(result);

            }
        }

        public async Task<FriendViewModel> PostAsync(FriendViewModel model)
        {
            if (!string.IsNullOrEmpty(_bearerToken))
                await Login();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBasePath);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApiBasePath}/api/friends", httpContent);
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<FriendViewModel>(result);

            }
        }

        public async Task<FriendViewModel> PutAsync(FriendViewModel model)
        {
            int ErrorCount(int errorCount)
            {
                var task = Task.Run(() => errorCount++);
                task.Wait(TimeSpan.FromSeconds(2));
                return errorCount;
            }

            var count = 0;

            if (!string.IsNullOrEmpty(_bearerToken))
                await Login();

            while (count <= LimiteTentativa)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiBasePath);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8,
                        "application/json");
                    var response = await client.PutAsync($"{ApiBasePath}/api/friends/{model.Id}", httpContent);

                    if (response.IsSuccessStatusCode)
                    {
                        break;
                    }
                    await Login();
                    count = ErrorCount(count);
                }

            }

            return model;
        }

        public async Task<FriendViewModel> DeleteAsync(Guid id)
        {
            if (!string.IsNullOrEmpty(_bearerToken))
                await Login();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBasePath);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.DeleteAsync($"{ApiBasePath}/api/friends/{id}");

                return JsonConvert.DeserializeObject<FriendViewModel>(await response.Content.ReadAsStringAsync());

            }
        }

        public async Task<IEnumerable<FriendViewModel>> GetAllAsync()
        {
            int ErrorCount(int errorCount)
            {
                var task = Task.Run(() => errorCount++);
                task.Wait(TimeSpan.FromSeconds(2));
                return errorCount;
            }

            var count = 0;

            var modelListResult = new List<FriendViewModel>();
            if (!string.IsNullOrEmpty(_bearerToken))
                await Login();
            while (count <= LimiteTentativa)
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiBasePath);
                    client.DefaultRequestHeaders.Clear();

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_bearerToken}");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{ApiBasePath}/api/friends");

                    if (!response.IsSuccessStatusCode)
                    {
                        await Login();
                        count = ErrorCount(count);
                        continue;
                    }

                    var responseResult = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseResult) && responseResult != "[]")
                    {
                        modelListResult = JsonConvert.DeserializeObject<List<FriendViewModel>>(responseResult);

                        break;
                    }

                    count = ErrorCount(count);

                }
            }

            return modelListResult;
        }
    }
}