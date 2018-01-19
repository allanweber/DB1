using Git.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services.Facades
{
    public class GitHubFacadeService : BaseFacade
    {
        protected IConfigurationRoot configuration;

        private readonly string UsersPath = "users";

        public GitHubFacadeService() : base()
        {
            this.loadConfiguration();
        }

        public async Task<ICollection<GitUser>> GetAllUsers()
        {
            HttpClient client = this.GetHttpClient(this.configuration["git:url"]);

            HttpResponseMessage responseMessage = await client.GetAsync(this.UsersPath);

            ICollection<GitUser> users = null;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                users = Newtonsoft.Json.JsonConvert.DeserializeObject<ICollection<GitUser>>(
                    await responseMessage.Content.ReadAsStringAsync());
            }

            return users;
        }

        private void loadConfiguration()
        {
            this.configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json.config", optional: true)
                .Build();
        }
    }
}
