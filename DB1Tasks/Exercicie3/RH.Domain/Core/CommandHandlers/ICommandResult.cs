namespace RH.Domain.Core.CommandHandlers
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }
        object Result { get; set; }
    }
}
