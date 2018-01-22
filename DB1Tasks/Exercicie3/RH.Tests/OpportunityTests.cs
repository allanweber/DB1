using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RH.Domain.CommandHandlers.Commands;
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
    public class OpportunityTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public OpportunityTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestOpportunityCrud()
        {
            Func<ICollection<OpportunityDto>> getFunc = () =>
            {
                var res = client.GetAsync("api/v1/Opportunity").Result;
                res.EnsureSuccessStatusCode();
                Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
                ICollection<OpportunityDto> result = JsonConvert.DeserializeObject<ICollection<OpportunityDto>>(res.Content.ReadAsStringAsync().Result);
                return result;
            };


            ICollection<OpportunityDto> opportunities = getFunc();
            Assert.False(opportunities.Any(), "Retornou Oportunidade quando não deveria");


            OpportunityInsertCommand insert = new OpportunityInsertCommand { Name = "Programador" };
            var response = await client.PostAsync("api/v1/Opportunity", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Any(), "Deveria retornar uma tecnologia");

            OpportunityUpdateCommand update = new OpportunityUpdateCommand { Id = 1, Name = "Programador.NET" };
            response = await client.PutAsync("api/v1/Opportunity", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.FirstOrDefault().Name == "Programador.NET", $"O nome estava {opportunities.FirstOrDefault().Name} mas deveria ser Programador.NET");

            response = await client.DeleteAsync("api/v1/Opportunity/1");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.False(opportunities.Any(), "Retornou Oportunidade quando não deveria após o delete");
        }
    }
}
