using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynastech.Patterns
{
    public class PatternBuilder
    {
        public PatternBuilder(IServiceCollection services)
        {
            this.ServiceCollection = services;
        }

        public IServiceCollection ServiceCollection { get; }
        public MessageHandlerRouterBuilder Routes { get; } = new MessageHandlerRouterBuilder();
    }
}
