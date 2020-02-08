using System;

namespace Dynastech.Patterns
{
    public class EventReceivedEventArgs : EventArgs
    {
        public string RoutingKey { get; set; }
        public IEvent Event { get; set; }
    }
}