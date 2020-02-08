using System;
using System.ComponentModel.DataAnnotations;

namespace Dynastech.Patterns
{
    public static class IMessageExtensions
    {
        public static string GetRoutingKey(this IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return message.GetType().GetAttribute<MessageRouteAttribute>()?.RoutingKey ?? message.GetType().Name;
        }

        public static string GetDisplayType(this IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            return message.GetType().GetAttribute<DisplayAttribute>()?.Name ?? message.GetType().Name;
        }
    }
}