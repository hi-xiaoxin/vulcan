using System;

namespace Dynastech.Patterns
{
    public interface IEventLogBuilder
    {
        EventLog Create(Guid? invokeUserId, string invokeUClientId, IMessage message, AffectedAggregatesDescriptor affectedAggregates, Exception exception = null);

        EventLog Create(Guid invokeUserId, IMessage message, AffectedAggregatesDescriptor affectedAggregates)
            => Create(invokeUserId, null, message, affectedAggregates, null);

        EventLog Create(string invokeClientId, IMessage message, AffectedAggregatesDescriptor affectedAggregates)
            => Create(null, invokeClientId, message, affectedAggregates, null);

        EventLog Create(Guid invokeUserId, IMessage message, Exception exception)
            => Create(invokeUserId, null, message, null, exception);

        EventLog Create(string invokeClientId, IMessage message, Exception exception)
            => Create(null, invokeClientId, message, null, exception);
    }
}
