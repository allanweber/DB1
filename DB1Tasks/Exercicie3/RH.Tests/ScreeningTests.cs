using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Dtos;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RH.Tests
{
    public class ScreeningTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public ScreeningTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task ScreeningTest()
        {
            await this.InsertCandidate("Allan");
            await this.InsertTechnology(".NET");
            await this.InsertTechnology("Angular");
            await this.InsertTechnology("SqlServer");
            await this.InsertCandidateTech(1, 1, 100);
            await this.InsertCandidateTech(1, 2, 10);
            await this.InsertCandidateTech(1, 3, 50);

            await this.InsertCandidate("Maicon");
            await this.InsertCandidateTech(2, 1, 50);
            await this.InsertCandidateTech(2, 2, 100);
            await this.InsertCandidateTech(2, 3, 10);

            await this.InsertCandidate("Roger");
            await this.InsertTechnology("Outsystems");
            await this.InsertCandidateTech(3, 1, 10);
            await this.InsertCandidateTech(3, 2, 40);
            await this.InsertCandidateTech(3, 4, 100);

            await this.InsertOpportunity("Programador .NET");
            await this.InsertOpportunityTech(1, 1, 100);
            await this.InsertOpportunityTech(1, 2, 50);
            await this.InsertOpportunityTech(1, 3, 10);

            await this.InsertOpportunity("Programador Outsystems");
            await this.InsertOpportunityTech(2, 4, 100);

            IList<ScreeningDto> result = await this.GetScreenings();

            Assert.True(result.Any(), "Deveria retornar triagem");

            Assert.True(result.FirstOrDefault().Candidates.FirstOrDefault().Name == "Allan",
                $"O primeiro candidado da vaga .NET deveria ser Allan mas era {result.FirstOrDefault().Candidates.FirstOrDefault().Name}");

            Assert.True(result.FirstOrDefault().Candidates.LastOrDefault().Name == "Roger",
                $"O primeiro candidado da vaga .NET deveria ser Roger mas era {result.FirstOrDefault().Candidates.LastOrDefault().Name}");

            await this.InsertOpportunity("Programador Java");
            await this.InsertTechnology("Java");
            await this.InsertOpportunityTech(3, 5, 100);

            result = await this.GetScreenings();
            Assert.False(result.LastOrDefault().Candidates.Any(),
                $"Não deveria ter candidatos para a vaga, mas havia {result.LastOrDefault().Candidates.Count()}");
        }

        private async Task<IList<ScreeningDto>> GetScreenings()
        {
            var res = client.GetAsync("api/v1/Screening").Result;
            res.EnsureSuccessStatusCode();
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
            IList<ScreeningDto> result = JsonConvert.DeserializeObject<IList<ScreeningDto>>(res.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task InsertTechnology(string name)
        {
            TechnologyInsertCommand insert = new TechnologyInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Technology", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
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

        public async Task InsertCandidateTech(int candidateId, int technologyId, int percentage)
        {
            CandidateTechInsertCommand insert = new CandidateTechInsertCommand { CandidateId = candidateId, TechnologyId = technologyId, Percentage = percentage };
            var response = await client.PostAsync("api/v1/CandidateTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }

        public async Task<IList<CandidateDto>> GetCandidates()
        {
            var res = client.GetAsync("api/v1/Candidate").Result;
            res.EnsureSuccessStatusCode();
            Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
            IList<CandidateDto> result = JsonConvert.DeserializeObject<IList<CandidateDto>>(res.Content.ReadAsStringAsync().Result);
            return result;
        }

        public async Task InsertOpportunity(string name)
        {
            OpportunityInsertCommand insert = new OpportunityInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Opportunity", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }

        public async Task InsertOpportunityTech(int opportunityId , int technologyId , int percentage)
        {
            OpportunityTechInsertCommand insert = new OpportunityTechInsertCommand { OpportunityId = opportunityId, TechnologyId = technologyId, Percentage = percentage };
            var response = await client.PostAsync("api/v1/OpportunityTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
        }
    }
}
