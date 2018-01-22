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

namespace RH.Api.Controllers
{
    /// <summary>
    /// Api de candidatos para oportunidade
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class OpportunityCandidateController
        : BaseCrudController
        <IOpportunityCandidateRepository,
            OpportunityCandidate,
            OpportunityCandidateInsertCommand,
            OpportunityCandidateUpdateCommand,
            OpportunityCandidateDeleteCommand,
            OpportunityCandidateDto>
    {
        public OpportunityCandidateController(IMapper mapper, IMediator mediator, IOpportunityCandidateRepository repository) 
            : base(mapper, mediator, repository)
        {
        }
    }
}
