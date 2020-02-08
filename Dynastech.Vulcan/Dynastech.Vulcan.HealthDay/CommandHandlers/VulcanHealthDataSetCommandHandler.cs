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
        private readonly IVulcanHealthDataStore _store;

        public VulcanHealthDataSetCommandHandler(
            IVulcanHealthDataStore store,
            IEventLogBuilder logBuilder,
            IEventLogStore eventLogStore,
            IEventPublisher eventPublisher,
            ILogger<VulcanHealthDataSetCommandHandler> logger)
            : base(logBuilder, eventLogStore, eventPublisher, logger)
        {
            _store = store;
        }

        protected override async Task<CommandExectionResult> ExecuteAsync(CommandExectionContext<VulcanHealthDataSetCommand> context, CancellationToken cancellationToken = default)
        {
            var data = await _store.GetDataByOwnerIdAsync(context.Command.OperatorId);
            if(data == null)
            {
                data =  new VulcanHealthData
                {
                    Id = context.Command.DataId
                };
                await _store.CreateDataAsync(data);
            }
            else
            {
                data.IsGotoHuBei = false;
                await _store.UpdateDataAsync(data);
            }

            return CommandExectionResult.Handle(data);
        }
    }
}