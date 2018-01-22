using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Constants;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class OpportunityTechController: Controller
    {
        public IMapper Mapper { get; }
        public IMediator Mediator { get; }
        public IOpportunityTechRepository Repository { get; }

        public OpportunityTechController(IMapper mapper, IMediator mediator, IOpportunityTechRepository repository)
        {
            Mapper = mapper;
            Mediator = mediator;
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<OpportunityTech>, List<OpportunityTechDto>>(entity);

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OpportunityTechInsertCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OpportunityTechUpdateCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            OpportunityTechDeleteCommand candidate = new OpportunityTechDeleteCommand(id);
            ICommandResult result = await this.Mediator.Send(candidate);

            return this.Ok(result);
        }
    }
}
