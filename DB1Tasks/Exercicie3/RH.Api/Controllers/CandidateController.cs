using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RH.Domain.Constants;
using RH.Domain.Dtos;
using RH.Domain.Services;
using System.Threading.Tasks;

namespace RH.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class CandidateController : Controller
    {
        public ICandidateService Service { get; }

        public CandidateController(ICandidateService candidateService)
        {
            Service = candidateService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dto = await this.Service.GetAll();

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CandidateInsertDto technology)
        {
            await this.Service.Insert(technology);

            return this.Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Post([FromBody] CandidateDto technology)
        {
            await this.Service.Update(technology);

            return this.Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.Service.Delete(id);

            return this.Ok();
        }
    }
}
