using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Constants;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class CandidateTechController: Controller
    {
        public IMapper Mapper { get; }
        public IMediator Mediator { get; }
        public ICandidateTechRepository Repository { get; }

        public CandidateTechController(IMapper mapper, IMediator mediator, ICandidateTechRepository repository)
        {
            Mapper = mapper;
            Mediator = mediator;
            Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<CandidateTech>, List<CandidateTechDto>>(entity);

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CandidateTechInsertCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CandidateTechUpdateCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CandidateTechDeleteCommand candidate = new CandidateTechDeleteCommand(id);
            ICommandResult result = await this.Mediator.Send(candidate);

            return this.Ok(result);
        }
    }
}
