using System;
using System.Collections.Generic;

namespace Dynastech.Patterns
{
    public interface IEventListener
    {
        event EventHandler<EventReceivedEventArgs> Received;
        void StartListen();
        void Close();
    }
}