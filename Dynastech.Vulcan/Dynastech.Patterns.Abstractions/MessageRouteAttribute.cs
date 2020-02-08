using System;

namespace Dynastech.Patterns
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class MessageRouteAttribute : Attribute
    {
        public MessageRouteAttribute() { }

        public MessageRouteAttribute(string routingKey)
        {
            this.RoutingKey = routingKey;
        }

        /// <summary>
        /// 消息路由Key
        /// </summary>
        public string RoutingKey { get; set; }
    }
}
