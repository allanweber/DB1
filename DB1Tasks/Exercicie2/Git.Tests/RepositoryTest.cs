using AutoMapper;
using Git.Domain.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Git.Tests
{
    public class RepositoryTest
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public RepositoryTest()
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Path.GetFullPath(@"../../../../Git/"))
                    .AddJsonFile("appsettings.json", optional: false).Build();

            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>().UseConfiguration(configuration));
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestGetUserRepositories()
        {
            // Act
            var response = await client.GetAsync("api/v1/User");
            response.EnsureSuccessStatusCode();

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            var responseString = await response.Content.ReadAsStringAsync();

            ICollection<User> users = JsonConvert.DeserializeObject<ICollection<User>>(await response.Content.ReadAsStringAsync());

            Assert.True(users.Any(), "Não retornou nenhum usuário");

            response = await client.GetAsync($"api/v1/Repository/{users.FirstOrDefault().Login}");
            response.EnsureSuccessStatusCode();

            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            responseString = await response.Content.ReadAsStringAsync();

            ICollection<Repository> repos = JsonConvert.DeserializeObject<ICollection<Repository>>(await response.Content.ReadAsStringAsync());

            Assert.True(repos.Any(), "Não retornou nenhum reposítório");

        }
    }
}
