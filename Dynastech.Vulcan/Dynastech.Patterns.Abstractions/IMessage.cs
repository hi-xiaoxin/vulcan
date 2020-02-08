using System;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public interface IMessage
    {
        Guid _TrackId { get; }
        Guid? _TenantId { get; }
    }
}