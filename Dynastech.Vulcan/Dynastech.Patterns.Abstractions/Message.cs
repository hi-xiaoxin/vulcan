using System;

namespace Dynastech.Patterns
{
    public abstract class Message : IMessage
    {
        /// <summary>
        /// 跟踪ID
        /// </summary>
        public Guid _TrackId { get; set; }

        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid? _TenantId { get; set; }
    }
}