using System;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IEventLogStore : IStore
    {
        Task LogAsync(EventLog log);
    }
}