using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Patterns
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly IEventLogStore _eventLogStore;
        private readonly IEventLogBuilder _logBuilder;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger _logger;

        public CommandHandler(
            IEventLogBuilder logBuilder,
            IEventLogStore eventLogStore,
            IEventPublisher eventPublisher,
            ILogger<CommandHandler<TCommand>> logger)
        {
            _logBuilder = logBuilder;
            _eventLogStore = eventLogStore;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Guid? userId = null;
            string clientId = null;
            if (command is IOperationCommand operationCommand)
            {
                userId = operationCommand.OperatorId;
                clientId = operationCommand.ClientId;
            }

            EventLog log = null;

            try
            {
                var result = await ExecuteAsync(new CommandExectionContext<TCommand>(command), cancellationToken);

                if (result.IsHandled)
                {
                    log = _logBuilder.Create(userId, clientId, command, new AffectedAggregatesDescriptor
                    {
                        Aggregates = result.AffectedAggregates,
                        Count = result.AffectedAggregatesCount,
                        Type = result.AffectedAggregateType,
                    });
                }
            }
            catch (ValidationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                log = _logBuilder.Create(userId, clientId, command, null, ex);
                _logger.LogError(ex, $"命令处理时出现异常,DateTime:{DateTime.Now},CommandType:{command.GetType().Name}");
                throw ex;
            }

            if (log != null)
            {
                try
                {
                    await _eventLogStore.LogAsync(log);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"命令处理后,在本地记录日志时出现异常,DateTime:{DateTime.Now},CommandType:{command.GetType().Name}");
                }
                try
                {
                    await _eventPublisher.PublishAsync($"log.{log.Source}.{command.GetRoutingKey()}", log, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"命令处理后,在事件发布时出现异常,DateTime:{DateTime.Now},CommandType:{command.GetType().Name}");
                }
            }
        }

        protected abstract Task<CommandExectionResult> ExecuteAsync(CommandExectionContext<TCommand> context, CancellationToken cancellationToken = default);
    }
}
