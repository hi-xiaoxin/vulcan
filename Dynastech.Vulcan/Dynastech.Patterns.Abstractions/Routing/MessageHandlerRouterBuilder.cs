using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Dynastech.Patterns
{
    public class MessageHandlerRouterBuilder
    {
        private readonly List<MessageHandlerRouter> _queryRoutes = new List<MessageHandlerRouter>();
        private readonly List<MessageHandlerRouter> _eventRoutes = new List<MessageHandlerRouter>();
        private readonly List<MessageHandlerRouter> _commandRoutes = new List<MessageHandlerRouter>();

        public IReadOnlyList<MessageHandlerRouter> QueryRoutes => _queryRoutes.AsReadOnly();
        public IReadOnlyList<MessageHandlerRouter> EventRoutes => _eventRoutes.AsReadOnly();
        public IReadOnlyList<MessageHandlerRouter> CommandRoutes => _commandRoutes.AsReadOnly();


        public void AddCommandPart(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || !type.IsClass)
                    continue;

                var commandTypes = type.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICommandHandler<>))
                    .Select(x => x.GenericTypeArguments[0])
                    .ToList();

                foreach (var commandType in commandTypes)
                    AddCommand(commandType, type);
            }
        }

        public void AddEventPart(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || !type.IsClass)
                    continue;

                var eventTypes = type.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>))
                    .Select(x => x.GenericTypeArguments[0])
                    .ToList();

                foreach (var eventType in eventTypes)
                    AddEvent(eventType, type);
            }
        }

        public void AddQueryPart(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsAbstract || !type.IsClass)
                    continue;

                var eventTypes = type.GetInterfaces()
                    .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                    .Select(x => x.GenericTypeArguments[0])
                    .ToList();

                foreach (var queryType in eventTypes)
                    AddQuery(queryType, type);
            }
        }

        public void AddCommand<TCommand, TCommandHandler>()
            where TCommand : ICommand
            where TCommandHandler : class, ICommandHandler<TCommand>
            => AddCommand(typeof(TCommand), typeof(TCommandHandler));

        public void AddEvent<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : class, IEventHandler<TEvent>
            => AddEvent(typeof(TEvent), typeof(TEventHandler));

        public void AddQuery<TQuery, TResult, TQueryHandler>()
            where TQuery : IQuery<TResult>
            where TQueryHandler : class, IQueryHandler<TQuery, TResult>
            => AddQuery(typeof(TQuery), typeof(TQueryHandler));

        public void AddQuery<TQuery, TQueryHandler>()
            where TQueryHandler : class
        {
            var queryType = typeof(TQuery);

            if (queryType.IsAssignableFrom(typeof(IQuery<>)))
                throw new InvalidOperationException("");

            var queryInterface = queryType.GetInterfaces().FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQuery<>));
            if (queryInterface == null)
                throw new InvalidOperationException($"TQuery泛型参数必须继承于IQuery<>接口");

            var handlerType = typeof(TQueryHandler);

            var handlerInterfaces = handlerType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)).ToList();
            if (handlerInterfaces.Count == 0)
                throw new InvalidOperationException($"TQueryHandler泛型参数必须继承于IQueryHandler<,>接口");


            if (!handlerInterfaces.Any(x => queryInterface.IsAssignableFrom(x.GenericTypeArguments[0])))
                throw new InvalidOperationException($"TQueryHandler类型必须与TQuery类型匹配");

            this.AddQuery(queryType, handlerType);
        }

        private void AddCommand(Type commandType, Type handlerType)
        {
            var methodInfo = handlerType.GetMethod(nameof(ICommandHandler<ICommand>.HandleAsync), new Type[] { commandType, typeof(CancellationToken) });
            if (methodInfo == null)
                throw new InvalidOperationException($"在消息处理程序({handlerType.Name})中，未找到消息({commandType.Name})的处理方法");

            var router = _commandRoutes.FirstOrDefault(x => x.MessageType == commandType);
            if (router == null)
            {
                router = new MessageHandlerRouter
                {
                    MessageType = commandType,
                    HandlerType = handlerType,
                    MethodInfo = methodInfo,
                };
                _commandRoutes.Add(router);
            }
            else
            {
                router.HandlerType = handlerType;
                router.MethodInfo = methodInfo;
            }
        }

        private void AddEvent(Type eventType, Type handlerType)
        {
            var methodInfo = handlerType.GetMethod(nameof(IEventHandler<IEvent>.HandleAsync), new Type[] { eventType, typeof(CancellationToken) });
            if (methodInfo == null)
                throw new InvalidOperationException($"在消息处理程序({handlerType.Name})中，未找到消息({eventType.Name})的处理方法");

            var router = _eventRoutes.FirstOrDefault(x => x.MessageType == eventType);
            if (router == null)
            {
                router = new MessageHandlerRouter
                {
                    MessageType = eventType,
                    HandlerType = handlerType,
                    MethodInfo = methodInfo,
                };
                _eventRoutes.Add(router);
            }
            else
            {
                router.HandlerType = handlerType;
                router.MethodInfo = methodInfo;
            }
        }

        private void AddQuery(Type queryType, Type handlerType)
        {
            var methodInfo = handlerType.GetMethod(nameof(IQueryHandler<IQuery<object>, object>.HandleAsync), new Type[] { queryType, typeof(CancellationToken) });
            if (methodInfo == null)
                throw new InvalidOperationException($"在消息处理程序({handlerType.Name})中，未找到消息({queryType.Name})的处理方法");

            var router = _queryRoutes.FirstOrDefault(x => x.MessageType == queryType);
            if (router == null)
            {
                router = new MessageHandlerRouter
                {
                    MessageType = queryType,
                    HandlerType = handlerType,
                    MethodInfo = methodInfo,
                };
                _queryRoutes.Add(router);
            }
            else
            {
                router.HandlerType = handlerType;
                router.MethodInfo = methodInfo;
            }
        }

    }
}
