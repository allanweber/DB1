using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Git.Infrastructure.Services.Facades
{
    public class BaseFacade
    {
        protected BaseFacade()
        {
            
        }

        protected HttpClient GetHttpClient(string url, List<KeyValuePair<string, string>> headers = null)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);

            headers?.ForEach(p =>
            {
                client.DefaultRequestHeaders.Add(p.Key, p.Value);
            });

            return client;
        }
    }
}
