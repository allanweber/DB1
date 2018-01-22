using MediatR;

namespace RH.Domain.CommandHandlers.Commands
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        object Result { get; set; }
    }
}
