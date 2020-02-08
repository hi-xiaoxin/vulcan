namespace Dynastech.Patterns
{
    public interface IQuery : IMessage { }
    public interface IQuery<TResult> : IQuery { }
}
