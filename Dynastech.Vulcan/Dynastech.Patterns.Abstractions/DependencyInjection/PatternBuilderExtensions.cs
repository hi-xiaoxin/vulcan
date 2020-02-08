using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dynastech.Patterns
{
    public static class PatternBuilderExtensions
    {
        public static PatternBuilder AddEventLogBuilder(this PatternBuilder builder, Action<EventLogBuilderOptions> builderSetup = null)
        {
            var options = new EventLogBuilderOptions();
            builderSetup?.Invoke(options);

            builder.ServiceCollection.AddSingleton(options);
            builder.ServiceCollection.AddSingleton<IEventLogBuilder, EventLogBuilder>();
            return builder;
        }

        public static PatternBuilder AddEventListenerHostedService(this PatternBuilder builder)
        {
            builder.ServiceCollection.AddHostedService<EventListenerHostedService>();
            return builder;
        }

        public static PatternBuilder AddCommandListenerHostedService(this PatternBuilder builder)
        {
            builder.ServiceCollection.AddHostedService<CommandListenerHostedService>();
            return builder;
        }

    }
}
