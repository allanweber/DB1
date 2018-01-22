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
    /// Api de Candidatos
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class CandidateController :
        BaseCrudAppServiceController<
            ICandidateRepository,
            Candidate,
            CandidateInsertCommand,
            CandidateUpdateCommand,
            CandidateDeleteCommand,
            CandidateDto>
    {
        public CandidateController(IMapper mapper, IMediator mediator, ICandidateRepository repository) 
            : base(mapper, mediator, repository)
        {
        }
    }
}
