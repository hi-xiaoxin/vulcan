using System;
using System.Collections.Generic;

namespace Dynastech.Patterns
{
    public interface IEvent : IMessage
    {
        DateTimeOffset TimeStamp { get; set; }
    }
}