using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Core.CommandHandlers;
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
    public class OpportunityTechTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public OpportunityTechTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestOpportunityTechCrud()
        {
            Func<ICollection<OpportunityTechDto>> getFunc = () =>
            {
                var res = client.GetAsync("api/v1/OpportunityTech").Result;
                res.EnsureSuccessStatusCode();
                Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
                ICollection<OpportunityTechDto> result = JsonConvert.DeserializeObject<ICollection<OpportunityTechDto>>(res.Content.ReadAsStringAsync().Result);
                return result;
            };

            ICollection<OpportunityTechDto> opportunities = getFunc();
            Assert.False(opportunities.Any(), "Retornou Tecnologia da oportunidade quando não deveria");

            await this.InsertOpportunity("Programador");
            await this.InsertTechnology(".NET");

            OpportunityTechInsertCommand insert = new OpportunityTechInsertCommand { OpportunityId = 1, TechnologyId = 1, Percentage = 100 };
            var response = await client.PostAsync("api/v1/OpportunityTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Any(), "Deveria retornar uma Tecnologia da oportunidade");

            OpportunityTechUpdateCommand update = new OpportunityTechUpdateCommand { Id = 1, OpportunityId = 1, TechnologyId = 1, Percentage = 50 };
            response = await client.PutAsync("api/v1/OpportunityTech", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.FirstOrDefault().Percentage == 50, $"O percentual estava {opportunities.FirstOrDefault().Percentage} mas deveria ser 50");

            insert = new OpportunityTechInsertCommand { OpportunityId = 1, TechnologyId = 1, Percentage = 50 };
            response = await client.PostAsync("api/v1/OpportunityTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest, "Status deveria ser 400");
            ICommandResult commandResult = JsonConvert.DeserializeObject<FailureResult>(response.Content.ReadAsStringAsync().Result);
            Assert.True(commandResult.IsFailure, "Post deveria ter falhado");

            await this.InsertTechnology("Java");
            insert = new OpportunityTechInsertCommand { OpportunityId = 1, TechnologyId = 2, Percentage = 70 };
            response = await client.PostAsync("api/v1/OpportunityTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Count == 2, $"Deveria ter 2 tecnologias da oportunidade mas tinha {opportunities.Count}");

            response = await client.DeleteAsync("api/v1/OpportunityTech/1");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Count == 1, $"Retornou {opportunities.Count} Tecnologia da oportunidade quando deveria retornar 1");

            response = await client.DeleteAsync("api/v1/OpportunityTech/2");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.False(opportunities.Any(), "Retornou Tecnologia da oportunidade quando não deveria após o delete");
        }

        public async Task InsertOpportunity(string name)
        {
            OpportunityInsertCommand insert = new OpportunityInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Opportunity", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }

        public async Task InsertTechnology(string name)
        {
            TechnologyInsertCommand insert = new TechnologyInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Technology", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }
    }
}
