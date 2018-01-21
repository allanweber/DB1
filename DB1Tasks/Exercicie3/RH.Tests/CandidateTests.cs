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
    public class CandidateTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public CandidateTests()
        {
            ServiceCollectionExtensions.UseStaticRegistration = false;
            server = new TestServer(new WebHostBuilder()
                .UseEnvironment("IntegrationTests")
                .UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task TestCandidateCrud()
        {
            Func<ICollection<CandidateDto>> getFunc = () =>
            {
                var res = client.GetAsync("api/v1/Candidate").Result;
                res.EnsureSuccessStatusCode();
                Assert.True(res.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");
                ICollection<CandidateDto> result = JsonConvert.DeserializeObject<ICollection<CandidateDto>>(res.Content.ReadAsStringAsync().Result);
                return result;
            };


            ICollection<CandidateDto> candidates = getFunc();
            Assert.False(candidates.Any(), "Retornou Candidato quando não deveria");


            CandidateInsertDto insert = new CandidateInsertDto { Name = "Allan" };
            var response = await client.PostAsync("api/v1/Candidate", new StringContent(JsonConvert.SerializeObject(insert), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.True(candidates.Any(), "Deveria retornar uma Candidato");

            CandidateDto update = new CandidateDto { Id = 1, Name = "Allan Weber" };
            response = await client.PutAsync("api/v1/Candidate", new StringContent(JsonConvert.SerializeObject(update), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.True(candidates.FirstOrDefault().Name == "Allan Weber", $"O nome estava {candidates.FirstOrDefault().Name} mas deveria ser Allan Weber");

            response = await client.DeleteAsync("api/v1/Candidate/1");
            response.EnsureSuccessStatusCode();
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK, "Status deveria ser 200");

            candidates = getFunc();
            Assert.False(candidates.Any(), "Retornou Candidato quando não deveria após o delete");
        }
    }
}
