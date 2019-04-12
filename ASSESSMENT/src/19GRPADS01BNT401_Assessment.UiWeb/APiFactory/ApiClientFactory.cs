using _19GRPADS01BNT401_Assessment.UiWeb.Utils;
using System;
using System.Threading;

namespace _19GRPADS01BNT401_Assessment.UiWeb.APiFactory
{
    internal class ApiClientFactory
    {
        private static readonly Uri ApiUri;
        private static readonly Lazy<ApiClient> RestClient = new Lazy<ApiClient>(() => new ApiClient(ApiUri), LazyThreadSafetyMode.ExecutionAndPublication);

        static ApiClientFactory() => ApiUri = new Uri(ApplicationSettings.WebApiUrl);

        public static ApiClient Instance => RestClient.Value;
    }
}