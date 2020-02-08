using Dynastech.Vulcan.HealthDay;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dynastech.Vulcan.EntityFrameworkCore
{
    public class HealthDataStore : IHealthDataStore
    {
        public HealthDataStore()
        {

        }

        public Task CreateDataAsync(HealthData data)
        {
            throw new NotImplementedException();
        }

        public Task<HealthData> GetDataByOwnerIdAsync(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDataAsync(HealthData data)
        {
            throw new NotImplementedException();
        }
    }
}
