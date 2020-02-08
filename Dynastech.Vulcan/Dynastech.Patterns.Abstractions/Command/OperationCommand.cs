using System;

namespace Dynastech.Patterns
{
    public abstract class OperationCommand : Command, IOperationCommand
    {
        public Guid? OperatorId { get; set; }
        public string ClientId { get; set; }
    }
}