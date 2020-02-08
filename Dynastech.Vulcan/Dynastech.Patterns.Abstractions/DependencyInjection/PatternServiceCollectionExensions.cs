using Dynastech.Patterns;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PatternServiceCollectionExensions
    {
        public static IServiceCollection AddPattern(this IServiceCollection services, Action<PatternBuilder> builderSetup = null)
        {
            var builder = new PatternBuilder(services);
            builderSetup?.Invoke(builder);

            services.AddScoped<ICommandProcessor>(provider => new CommandProcessor(provider, builder.Routes.CommandRoutes));
            services.AddScoped<IEventProcessor>(provider => new EventProcessor(provider, builder.Routes.EventRoutes));
            services.AddScoped<IQueryProcessor>(provider => new QueryProcessor(provider, builder.Routes.QueryRoutes));

            var handlerTypes = new List<Type>();
            handlerTypes.AddRange(builder.Routes.CommandRoutes.Select(x => x.HandlerType));
            handlerTypes.AddRange(builder.Routes.EventRoutes.Select(x => x.HandlerType));
            handlerTypes.AddRange(builder.Routes.QueryRoutes.Select(x => x.HandlerType));

            foreach (var handlerType in handlerTypes.Distinct())
                services.AddScoped(handlerType);

            services.TryAddSingleton<IEventLogStore, EventLogStore>();
            services.TryAddScoped<IEventPublisher, EventPublisher>();
            services.TryAddScoped<IEventStore, EventStore>();

            return services;
        }
    }
}