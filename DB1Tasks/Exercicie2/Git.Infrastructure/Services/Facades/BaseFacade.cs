using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services.Facades
{
    public class BaseFacade
    {
        protected BaseFacade()
        {
            
        }

        protected HttpClient GetHttpClient(string url, List<KeyValuePair<string, string>> headers = null, string accept = "application/json")
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);

            headers?.ForEach(p =>
            {
                client.DefaultRequestHeaders.Add(p.Key, p.Value);
            });

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(accept));

            return client;
        }

        protected async Task<HttpResponseMessage> GetResponse(HttpClient client, string route)
        {
            if (client == null) throw new NullReferenceException("HttpClient não pode ser nulo.");
            return await client.GetAsync(route);
        }

        protected async Task<T> DeserializeResponse<T> (HttpResponseMessage response) where T : class
        {
            if (response == null) throw new NullReferenceException("HttpResponseMessage não pode ser nulo.");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(
                    await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
