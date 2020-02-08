using Dynastech.Patterns;
using System;

namespace Dynastech.Vulcan
{
    public class VulcanDataSetCommand : OperationCommand
    {
        public new Guid OperatorId
        {
            get => base.OperatorId.Value;
            set => base.OperatorId = value;
        }

        public new Guid _TenantId
        {
            get => base._TenantId.Value;
            set => base._TenantId = value;
        }
    }
}
