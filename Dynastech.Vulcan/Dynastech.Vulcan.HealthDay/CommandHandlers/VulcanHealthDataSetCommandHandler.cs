using Dynastech.Patterns;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dynastech.Vulcan.HealthDay
{
    public class VulcanHealthDataSetCommandHandler : CommandHandler<VulcanHealthDataSetCommand>
    {
        public VulcanHealthDataSetCommandHandler(
            IEventLogBuilder logBuilder, 
            IEventLogStore eventLogStore, 
            IEventPublisher eventPublisher, 
            ILogger<VulcanHealthDataSetCommandHandler> logger) 
            : base(logBuilder, eventLogStore, eventPublisher, logger)
        {
        }

        protected override Task<CommandExectionResult> ExecuteAsync(CommandExectionContext<VulcanHealthDataSetCommand> context, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}