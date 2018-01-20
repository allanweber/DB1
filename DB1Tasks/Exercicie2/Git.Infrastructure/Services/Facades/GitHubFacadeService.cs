using Git.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Git.Infrastructure.Services.Facades
{
    public class GitHubFacadeService : BaseFacade
    {
        protected IConfiguration configuration;

        private readonly string UsersPath = "users";

        private List<KeyValuePair<string, string>> headers = null;

        private string baseUrl = string.Empty;

        public GitHubFacadeService(IConfiguration configuration) : base()
        {
            this.configuration = configuration;

            this.baseUrl = this.configuration.GetSection("git:url").Get<string>();

            headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("User-Agent", "Other")
            };
        }

        public async Task<ICollection<GitUser>> GetAllUsers()
        {
            var url = this.configuration.GetSection("git");

            HttpClient client = this.GetHttpClient(this.baseUrl, this.headers);

            var response = await this.GetResponse(client, this.UsersPath);

            ICollection<GitUser> users = await this.DeserializeResponse<ICollection<GitUser>>(response);

            return users;
        }

        public async Task<GitUserDetail> GetUser(string userName)
        {
            HttpClient client = this.GetHttpClient(this.baseUrl, this.headers);

            var response = await this.GetResponse(client, $"{this.UsersPath}/{userName}");

            GitUserDetail users = await this.DeserializeResponse<GitUserDetail>(response);

            return users;
        }

        public async Task<ICollection<GitRepository>> GetUserRepositories(string userName)
        {
            HttpClient client = this.GetHttpClient(this.baseUrl, this.headers);

            var response = await this.GetResponse(client, $"{this.UsersPath}/{userName}/repos");

            ICollection<GitRepository> users = await this.DeserializeResponse<ICollection<GitRepository>>(response);

            return users;
        }
    }
}
