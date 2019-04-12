using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


using static System.Console;
namespace GRPADS01BNT401_TP3Console
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            WriteLine("TP3 - SAMUEL DIOGO DE JESUS");
            WriteLine("LIST OF FRIENDS....");

            Task.Run(async () =>
            {

                var service = new FriendWebService();

                var ress = await service.Login();

                var friends = await service.GetAllAsync();

                if(friends.Any())
                {
                    foreach (var friend in friends)
                    {
                        WriteLine(friend);
                    }
                }


            });

         

            ReadLine();


        }
    }

    public class FriendViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }


        public string FullName => $"{Name} {LastName}";

        public override string ToString()
        {
            return $"{nameof(Id)}:{Id:N} | {nameof(FullName)}:{FullName} | {nameof(Email)}:{Email} | {nameof(BirthDate)}:{BirthDate:dd/MM/yyyy}";
        }
    }

    public class FriendWebService
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