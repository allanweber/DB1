using MediatR;
using RH.Domain.Core.CommandHandlers;
using RH.Domain.Core.Entities;

namespace RH.Domain.CommandHandlers.Commands
{
    public class TechnologyDeleteCommand: BaseEntity, IRequest<ICommandResult>
    {
    }
}
