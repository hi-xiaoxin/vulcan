using System;

namespace Dynastech.Patterns
{
    public abstract class Event : Message, IEvent
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
    }
}