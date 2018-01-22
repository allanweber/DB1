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
    /// APi de Tecnologias do candidato
    /// </summary>
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class CandidateTechController :
        BaseCrudController
        <ICandidateTechRepository,
        CandidateTech,
        CandidateTechInsertCommand,
        CandidateTechUpdateCommand,
        CandidateTechDeleteCommand,
        CandidateTechDto>
    {
        public CandidateTechController(IMapper mapper, IMediator mediator, ICandidateTechRepository repository) 
            : base(mapper, mediator, repository)
        {
        }
    }
}
