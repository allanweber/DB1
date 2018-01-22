namespace RH.Domain.CommandHandlers.Commands
{
    public class FailureResult : ICommandResult
    {
        public bool IsSuccess => Result == null;

        public bool IsFailure => Result != null;

        public object Result { get; set; }
    }
}
