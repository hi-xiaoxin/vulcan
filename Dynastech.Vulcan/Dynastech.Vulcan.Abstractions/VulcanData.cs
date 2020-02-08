using Dynastech.Patterns;
using System;

namespace Dynastech.Vulcan
{
    public class VulcanData : IAggregate
    {
        public Guid Id { get; set; }

        public Guid CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string CreatorAccountName { get; set; }

        public DateTime WhenCreated { get; set; }
        public DateTime WhenModified { get; set; }

    }
}
