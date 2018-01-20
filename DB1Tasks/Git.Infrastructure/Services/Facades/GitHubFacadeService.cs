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

            var response = await this.GetResponse(client, this.UsersPath);

            ICollection<GitUser> users = await this.DeserializeResponse<ICollection<GitUser>>(response);

            return users;
        }

        public async Task<GitUserDetail> GetUser(string userName)
        {
            HttpClient client = this.GetHttpClient(this.configuration["git:url"]);

            var response = await this.GetResponse(client, $"{this.UsersPath}/{userName}");

            GitUserDetail users = await this.DeserializeResponse<GitUserDetail>(response);

            return users;
        }

        public async Task<ICollection<GitRepository>> GetUserRepositories(string userName)
        {
            HttpClient client = this.GetHttpClient(this.configuration["git:url"]);

            var response = await this.GetResponse(client, $"{this.UsersPath}/{userName}/repos");

            ICollection<GitRepository> users = await this.DeserializeResponse<ICollection<GitRepository>>(response);

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
