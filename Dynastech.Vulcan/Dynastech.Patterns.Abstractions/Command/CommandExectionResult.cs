using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dynastech.Patterns
{
    public class CommandExectionResult
    {
        public int AffectedAggregatesCount { get; set; }
        public string AffectedAggregateType { get; set; }
        public List<AggregateDescriptor> AffectedAggregates { get; set; } = new List<AggregateDescriptor>();
        public bool IsHandled { get; set; }

        public static CommandExectionResult Unhandle() => new CommandExectionResult { IsHandled = false };

        public static CommandExectionResult Handle<T>(int aggregatesCount) => Handle(typeof(T), aggregatesCount);

        public static CommandExectionResult Handle(Type aggregateType, int aggregatesCount)
        {
            if (aggregateType == null)
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregatesCount = aggregatesCount
                };
            }
            else
            {
                var type = aggregateType.GetAttribute<DisplayAttribute>()?.Name ?? aggregateType.Name;
                return Handle(type, aggregatesCount);
            }
        }

        public static CommandExectionResult Handle(string aggregateType, int aggregatesCount)
        {
            return new CommandExectionResult
            {
                IsHandled = true,
                AffectedAggregateType = aggregateType,
                AffectedAggregatesCount = aggregatesCount,
            };
        }

        public static CommandExectionResult Handle(AggregateDescriptor aggregateDescriptor)
        {
            if (aggregateDescriptor == null)
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregatesCount = 1,
                };
            }
            else
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregateType = aggregateDescriptor.Type,
                    AffectedAggregatesCount = 1,
                    AffectedAggregates = new List<AggregateDescriptor> { aggregateDescriptor }
                };
            }
        }

        public static CommandExectionResult Handle(IEnumerable<AggregateDescriptor> aggregateDescriptors)
        {
            if (aggregateDescriptors == null)
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregatesCount = 0,
                };
            }
            else
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregateType = aggregateDescriptors.FirstOrDefault()?.Type,
                    AffectedAggregatesCount = aggregateDescriptors.Count(),
                    AffectedAggregates = new List<AggregateDescriptor>(aggregateDescriptors)
                };
            }
        }

        public static CommandExectionResult Handle(IAggregate aggregateRoot)
        {
            if (aggregateRoot == null)
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregatesCount = 1,
                };
            }
            else
            {
                return Handle(aggregateRoot.GetDescriptor());
            }
        }

        public static CommandExectionResult Handle(IEnumerable<IAggregate> aggregateRoot)
        {
            if (aggregateRoot == null)
            {
                return new CommandExectionResult
                {
                    IsHandled = true,
                    AffectedAggregatesCount = 1,
                };
            }
            else
            {
                return Handle(aggregateRoot.Select(x => x.GetDescriptor()));
            }
        }
    }
}
