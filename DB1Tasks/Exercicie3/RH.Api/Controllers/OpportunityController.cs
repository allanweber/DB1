﻿using Microsoft.AspNetCore.Cors;
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
    public class OpportunityController : Controller
    {
        public IOpportunityService Service { get; }

        public OpportunityController(IOpportunityService opportunityService)
        {
            Service = opportunityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dto = await this.Service.GetAll();

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OpportunityInsertDto technology)
        {
            await this.Service.Insert(technology);

            return this.Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Post([FromBody] OpportunityDto technology)
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
