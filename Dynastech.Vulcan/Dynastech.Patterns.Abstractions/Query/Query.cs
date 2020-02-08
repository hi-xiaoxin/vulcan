using System;

namespace Dynastech.Patterns
{
    public abstract class Query<TResult> : Message, IQuery<TResult> { }
}
