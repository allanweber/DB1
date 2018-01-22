using MediatR;
using RH.Domain.Core.Entities;

namespace RH.Domain.CommandHandlers.Commands
{
    public class CandidateDeleteCommand : BaseEntity,  IRequest<ICommandResult>
    {
    }
}
