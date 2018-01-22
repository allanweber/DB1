using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RH.Api.Core;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Constants;
using RH.Domain.Core.Repositories;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace RH.Controllers
{
    /// <summary>
    /// Api de tecnologias
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class TechnologyController :
        BaseCrudController<
            ITechnologyRepository,
            Technology,
            TechnologyInsertCommand,
            TechnologyUpdateCommand,
            TechnologyDeleteCommand,
            TechnologyDto>
    {
        public TechnologyController(IMapper mapper, IMediator mediator, IRepository<Technology> repository) 
            : base(mapper, mediator, repository)
        {
        }
    }
}
