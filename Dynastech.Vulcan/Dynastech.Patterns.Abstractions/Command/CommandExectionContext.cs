namespace Dynastech.Patterns
{
    public class CommandExectionContext<TCommand> where TCommand : ICommand
    {
        public CommandExectionContext(TCommand command)
        {
            this.Command = command;
        }

        public TCommand Command { get; }
    }
}
