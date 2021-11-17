using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace StarbucksMobileApp.Api.ClientManagers
{
    public class BaseClientManager
    {
        private string url;
        public HttpClient client { get; private set; }
        public JsonSerializerSettings settings { get; private set; }

        public BaseClientManager(string url)
        {
            if (String.IsNullOrEmpty(url))
                throw new Exception("Api url geçersiz!");

            this.url = url;
            if (!this.url.EndsWith("/"))
                this.url += "/";

            client = new HttpClient
            {
                BaseAddress = new Uri(this.url)
            };

            settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
        }
    }
}
