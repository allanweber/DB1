using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RH.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RH.Tests
{
    public class TechnologyTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public TechnologyTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestTechnologyCrud()
        {
            Func<ICollection<TechnologyDto>> getFunc = () =>
            {
                var res = client.GetAsync("api/v1/Technology").Result;
                res.EnsureSuccessStatusCode();
                Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
                ICollection<TechnologyDto> result = JsonConvert.DeserializeObject<ICollection<TechnologyDto>>(res.Content.ReadAsStringAsync().Result);
                return result;
            };

            
            ICollection<TechnologyDto> techs = getFunc();
            Assert.False(techs.Any(), "Retornou tecnologia quando não deveria");


            TechnologyInsertDto insert = new TechnologyInsertDto { Name = ".NET" };
            var response = await client.PostAsync("api/v1/Technology", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            techs = getFunc();
            Assert.True(techs.Any(), "Deveria retornar uma tecnologia");

            TechnologyDto update = new TechnologyDto { Id = 1, Name = ".NET Core" };
            response = await client.PutAsync("api/v1/Technology", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            techs = getFunc();
            Assert.True(techs.FirstOrDefault().Name == ".NET Core", $"O nome estava {techs.FirstOrDefault().Name} mas deveria ser .NET Core");

            response = await client.DeleteAsync("api/v1/Technology/1");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            techs = getFunc();
            Assert.False(techs.Any(), "Retornou tecnologia quando não deveria após o delete");
        }
    }
}
