using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public class TechnologyDeleteCommand: IRequest<ICommandResult>
    {
        public TechnologyDeleteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
