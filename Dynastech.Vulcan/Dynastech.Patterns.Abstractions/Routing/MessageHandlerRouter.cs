using System;
using System.Reflection;

namespace Dynastech.Patterns
{
    public class MessageHandlerRouter
    {
        public Type MessageType { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public Type HandlerType { get; set; }
    }
}
