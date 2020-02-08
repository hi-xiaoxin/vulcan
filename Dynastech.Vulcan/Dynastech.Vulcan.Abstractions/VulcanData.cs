using Dynastech.Patterns;
using System;

namespace Dynastech.Vulcan
{
    public class VulcanData : IAggregate
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 一级组织ID
        /// </summary>
        public Guid TenantId { get; set; }

        public Guid OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAccountName { get; set; }
        public string OwnerPhone { get; set; }

        public DateTime WhenCreated { get; set; }
        public DateTime WhenModified { get; set; }

    }
}
