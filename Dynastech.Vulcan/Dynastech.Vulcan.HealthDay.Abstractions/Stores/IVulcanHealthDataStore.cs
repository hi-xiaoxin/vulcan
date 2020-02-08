using Dynastech.Patterns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dynastech.Vulcan.HealthDay
{
    public interface IVulcanHealthDataStore : IStore
    {
        Task CreateDataAsync(VulcanHealthData data);

        Task UpdateDataAsync(VulcanHealthData data);

        /// <summary>
        /// 获取用户的健康日报数据
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        Task<VulcanHealthData> GetDataByOwnerIdAsync(Guid ownerId);
    }
}