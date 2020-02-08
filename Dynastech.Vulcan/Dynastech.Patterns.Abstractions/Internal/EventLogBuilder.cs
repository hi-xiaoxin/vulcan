using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dynastech.Patterns
{
    public class EventLogBuilder : IEventLogBuilder
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions;
        static EventLogBuilder()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true,
            };
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        private readonly EventLogBuilderOptions _options;

        public EventLogBuilder(EventLogBuilderOptions options)
        {
            _options = options;
        }

        public EventLog Create(Guid? invokeUserId, string invokeUClientId, IMessage message, AffectedAggregatesDescriptor affectedAggregates, Exception exception = null)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            MessageKind messageKind;
            if (message is ICommand)
                messageKind = MessageKind.Command;
            else if (message is IQuery)
                messageKind = MessageKind.Query;
            else
                messageKind = MessageKind.Event;

            var messageType = message.GetType().Name;
            var messageDisplayType = message.GetDisplayType();

            return new EventLog
            {
                UserId = invokeUserId,
                Source = _options.Source,
                ClientId = invokeUClientId ?? _options.ClientId,
                _TrackId = message._TrackId,
                TimeStamp = DateTimeOffset.Now,
                _TenantId = message._TenantId,

                MessageType = messageType,
                MessageDisplayType = messageDisplayType,
                MessageData = JsonSerializer.Serialize(message, message.GetType(), _jsonSerializerOptions),
                MessageKind = messageKind,

                AffectedAggregates = affectedAggregates,

                IsError = exception != null,
                ErrorMessage = exception?.Message,
                ErrorStackTrace = exception?.StackTrace,
            };
        }
    }
}
