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

namespace RH.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class TechnologyController : Controller
    {
        public IMapper Mapper { get; }
        public IMediator Mediator { get; }
        public ITechnologyRepository Repository { get; }

        public TechnologyController(IMapper mapper, IMediator mediator, ITechnologyRepository technologyRepositoryRepository)
        {
            Mapper = mapper;
            Mediator = mediator;
            Repository = technologyRepositoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<Technology>, List<TechnologyDto>>(entity);

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TechnologyInsertCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TechnologyUpdateCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            TechnologyDeleteCommand candidate = new TechnologyDeleteCommand(id);
            ICommandResult result = await this.Mediator.Send(candidate);

            return this.Ok(result);
        }
    }
}
