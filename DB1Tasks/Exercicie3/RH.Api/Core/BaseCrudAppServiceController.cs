using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RH.Domain.Core.CommandHandlers;
using RH.Domain.Core.Dtos;
using RH.Domain.Core.Entities;
using RH.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Api.Core
{
    public class BaseCrudController<TRepository, TEntity, TInsertCommand, TUpdateCommand, TDeleteCommand, TGetListDto>: Controller
        where TRepository : IRepository<TEntity>
        where TEntity : BaseEntity
        where TInsertCommand : IRequest<ICommandResult>
        where TUpdateCommand : IRequest<ICommandResult>
        where TDeleteCommand : BaseEntity, IRequest<ICommandResult>, new()
        where TGetListDto : IDto
    {
        public IMapper Mapper { get; }
        public IMediator Mediator { get; }
        public IRepository<TEntity> Repository { get; }

        protected BaseCrudController(IMapper mapper, IMediator mediator, IRepository<TEntity> repository)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.Repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<TEntity>, List<TGetListDto>>(entity);

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TInsertCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TUpdateCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            TDeleteCommand delete = new TDeleteCommand();
            delete.Id = id;

            ICommandResult result = await this.Mediator.Send(delete);

            return this.Ok(result);
        }
    }
}
