using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dynastech.Vulcan.HealthDay
{
    public interface IHealthDataStore
    {
        Task CreateDataAsync(HealthData data);

        Task UpdateDataAsync(HealthData data);

        /// <summary>
        /// 获取用户的健康日报数据
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<HealthData> GetDataByOwnerIdAsync(Guid ownerId);
    }
}