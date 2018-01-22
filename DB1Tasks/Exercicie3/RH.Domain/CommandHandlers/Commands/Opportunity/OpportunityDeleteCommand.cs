using MediatR;
using RH.Domain.Core.CommandHandlers;
using RH.Domain.Core.Entities;

namespace RH.Domain.CommandHandlers.Commands
{
    public class OpportunityDeleteCommand: BaseEntity, IRequest<ICommandResult>
    {
    }
}
