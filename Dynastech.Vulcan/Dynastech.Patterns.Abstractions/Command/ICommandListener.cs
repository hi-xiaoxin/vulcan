using System;

namespace Dynastech.Patterns
{
    public interface ICommandListener
    {
        event EventHandler<CommandReceivedEventArgs> Received;
        void StartListen();
        void Close();
    }

    public class CommandReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// RabbitMq的队列名
        /// </summary>
        public string RoutingKey { get; set; }
        public ICommand Command { get; set; }
    }

}