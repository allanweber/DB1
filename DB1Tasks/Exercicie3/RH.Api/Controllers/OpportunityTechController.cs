using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RH.Api.Core;
using RH.Domain.CommandHandlers.Commands;
using RH.Domain.Constants;
using RH.Domain.Dtos;
using RH.Domain.Entities;
using RH.Domain.Repositories;

namespace RH.Api.Controllers
{
    /// <summary>
    /// Api de tecnologias para oportunidade
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class OpportunityTechController :
        BaseCrudController<
            IOpportunityTechRepository,
            OpportunityTech,
            OpportunityTechInsertCommand,
            OpportunityTechUpdateCommand,
            OpportunityTechDeleteCommand,
            OpportunityTechDto>
    {
        public OpportunityTechController(IMapper mapper, IMediator mediator, IOpportunityTechRepository repository) 
            : base(mapper, mediator, repository)
        {
        }
    }
}
