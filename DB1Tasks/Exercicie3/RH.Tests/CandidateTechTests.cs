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
    public class CandidateTechTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public CandidateTechTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestCandidateTechCrud()
        {
            Func<ICollection<CandidateTechDto>> getFunc = () =>
            {
                var res = client.GetAsync("api/v1/CandidateTech").Result;
                res.EnsureSuccessStatusCode();
                Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
                ICollection<CandidateTechDto> result = JsonConvert.DeserializeObject<ICollection<CandidateTechDto>>(res.Content.ReadAsStringAsync().Result);
                return result;
            };

            ICollection<CandidateTechDto> candidates = getFunc();
            Assert.False(candidates.Any(), "Retornou Tecnologia de Candidato quando não deveria");

            await this.InsertCandidate("Allan");
            await this.InsertTechnology(".NET");

            CandidateTechInsertCommand insert = new CandidateTechInsertCommand { CandidateId = 1, TechnologyId = 1, Percentage = 100 };
            var response = await client.PostAsync("api/v1/CandidateTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.True(candidates.Any(), "Deveria retornar uma Tecnologia de Candidato");

            CandidateTechUpdateCommand update = new CandidateTechUpdateCommand { Id = 1, CandidateId = 1, TechnologyId = 1, Percentage = 50 };
            response = await client.PutAsync("api/v1/CandidateTech", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.True(candidates.FirstOrDefault().Percentage == 50, $"O percentual estava {candidates.FirstOrDefault().Percentage} mas deveria ser 50");

            insert = new CandidateTechInsertCommand { CandidateId = 1, TechnologyId = 1, Percentage = 50 };
            response = await client.PostAsync("api/v1/CandidateTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest, "Status deveria ser 400");
            ICommandResult commandResult = JsonConvert.DeserializeObject<FailureResult>(response.Content.ReadAsStringAsync().Result);
            Assert.True(commandResult.IsFailure, "Post deveria ter falhado");

            await this.InsertTechnology("Java");
            insert = new CandidateTechInsertCommand { CandidateId = 1, TechnologyId = 2, Percentage = 100 };
            response = await client.PostAsync("api/v1/CandidateTech", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.True(candidates.Count == 2, $"Deveria ter 2 tecnologias do candidato mas tinha {candidates.Count}");

            response = await client.DeleteAsync("api/v1/CandidateTech/1");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.True(candidates.Count == 1, $"Retornou {candidates.Count} Tecnologia do candidato quando deveria retornar 1");

            response = await client.DeleteAsync("api/v1/CandidateTech/2");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.False(candidates.Any(), "Retornou Tecnologia de Candidato quando não deveria após o delete");
        }

        public async Task InsertCandidate(string name)
        {
            CandidateInsertCommand insert = new CandidateInsertCommand { Name = name };
            var response = await client.PostAsync("api/v1/Candidate", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
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
