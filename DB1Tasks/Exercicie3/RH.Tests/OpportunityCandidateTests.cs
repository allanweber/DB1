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
    public class OpportunityCandidateTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public OpportunityCandidateTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestOpportunityCandidateCrud()
        {
            Func<ICollection<OpportunityCandidateDto>> getFunc = () =>
            {
                var res = client.GetAsync("api/v1/OpportunityCandidate").Result;
                res.EnsureSuccessStatusCode();
                Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
                ICollection<OpportunityCandidateDto> result = JsonConvert.DeserializeObject<ICollection<OpportunityCandidateDto>>(res.Content.ReadAsStringAsync().Result);
                return result;
            };

            ICollection<OpportunityCandidateDto> opportunities = getFunc();
            Assert.False(opportunities.Any(), "Retornou candidado da oportunidade quando não deveria");

            await this.InsertOpportunity("Programador");
            await this.InsertCandidate("Allan");

            OpportunityCandidateInsertCommand insert = new OpportunityCandidateInsertCommand { OpportunityId = 1, CandidateId = 1 };
            var response = await client.PostAsync("api/v1/OpportunityCandidate", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Any(), "Deveria retornar um candidato da oportunidade");

            await this.InsertCandidate("Roger");
            OpportunityCandidateUpdateCommand update = new OpportunityCandidateUpdateCommand { Id = 1, OpportunityId = 1, CandidateId = 2 };
            response = await client.PutAsync("api/v1/OpportunityCandidate", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.FirstOrDefault().CandidateName == "Roger", $"O Nome do candidato estava {opportunities.FirstOrDefault().CandidateName} mas deveria ser 'Roger'");

            insert = new OpportunityCandidateInsertCommand { OpportunityId = 1, CandidateId = 2 };
            response = await client.PostAsync("api/v1/OpportunityCandidate", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest, "Status deveria ser 400");
            ICommandResult commandResult = JsonConvert.DeserializeObject<FailureResult>(response.Content.ReadAsStringAsync().Result);
            Assert.True(commandResult.IsFailure, "Post deveria ter falhado");

            insert = new OpportunityCandidateInsertCommand { OpportunityId = 1, CandidateId = 1 };
            response = await client.PostAsync("api/v1/OpportunityCandidate", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Count == 2, $"Deveria ter 2 candidadtos mas tinha {opportunities.Count}");

            response = await client.DeleteAsync("api/v1/OpportunityCandidate/1");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.True(opportunities.Count == 1, $"Retornou {opportunities.Count} candidatos quando deveria retornar 1");

            response = await client.DeleteAsync("api/v1/OpportunityCandidate/2");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            opportunities = getFunc();
            Assert.False(opportunities.Any(), "Retornou candidato quando não deveria após o delete");
        }

        public async Task InsertOpportunity(string name)
        {
            OpportunityInsertCommand insert = new OpportunityInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Opportunity", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }

        public async Task InsertCandidate(string name)
        {
            CandidateInsertCommand insert = new CandidateInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Candidate", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }
    }
}
