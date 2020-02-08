using System;
using System.ComponentModel.DataAnnotations;

namespace Dynastech.Patterns
{
    public static class AggregateDescriptorExtensions
    {
        public static AggregateDescriptor GetDescriptor(this IAggregate aggregateRoot)
        {
            if (aggregateRoot == null)
                return null;

            var type = aggregateRoot.GetType();

            return new AggregateDescriptor(
                aggregateRoot.Id,
                aggregateRoot.ToString(),
                type.Name,
                type.GetAttribute<DisplayAttribute>()?.Name);
        }
    }
}