using Git.Infrastructure.Services.Facades;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Git.Tests
{
    public class GitHubFacadeServiceTest
    {
        [Fact]
        public async Task ConfigurationTest()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Path.GetFullPath(@"../../../../Git/"))
                    .AddJsonFile("appsettings.json", optional: false).Build();

            Assert.False(configuration == null, "A configuração não foi criada");

            string gitUrl = configuration.GetSection("git:url").Get<string>();

            Assert.True(gitUrl == "https://api.github.com/", $"A url do git não estava {gitUrl} e deveria ser https://api.github.com/");
        }

        [Fact]
        public async Task TestGetAllUsers()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Path.GetFullPath(@"../../../../Git/"))
                    .AddJsonFile("appsettings.json", optional: false).Build();

            GitHubFacadeService facade = new GitHubFacadeService(configuration);

            var users = await facade.GetAllUsers();

            Assert.True(users.Any(), "Não retornou nenhum usuário");
        }

        public async Task TestGetOneUsers()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Path.GetFullPath(@"../../../../Git/"))
                    .AddJsonFile("appsettings.json", optional: false).Build();

            GitHubFacadeService facade = new GitHubFacadeService(configuration);

            var users = await facade.GetAllUsers();

            Assert.True(users.Any(), "Não retornou nenhum usuário");

            var user = facade.GetUser(users.FirstOrDefault().Login);

            Assert.True(user != null, "Não retornou nenhum usuário");
        }

        public async Task TestGetUserRepositories()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Path.GetFullPath(@"../../../../Git/"))
                    .AddJsonFile("appsettings.json", optional: false).Build();

            GitHubFacadeService facade = new GitHubFacadeService(configuration);

            var users = await facade.GetAllUsers();

            Assert.True(users.Any(), "Não retornou nenhum usuário");

            var repos = facade.GetUserRepositories(users.FirstOrDefault().Login);

            Assert.True(repos != null, "Não retornou nenhum respositório");
        }
    }
}
