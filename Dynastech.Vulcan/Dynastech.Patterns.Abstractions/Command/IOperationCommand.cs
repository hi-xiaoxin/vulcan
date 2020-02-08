using System;

namespace Dynastech.Patterns
{
    public interface IOperationCommand : ICommand
    {
        Guid? OperatorId { get; }
        string ClientId { get; }
    }
}
