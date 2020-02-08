using Dynastech.Patterns;
using Dynastech.Vulcan.HealthDay;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynastech.Vulcan.EntityFrameworkCore
{
    public class VulcanHealthDataStore : AbstractEntityFrameworkCoreStore<VulcanDbContext>, IVulcanHealthDataStore
    {
        public VulcanHealthDataStore(VulcanDbContext dbContext) : base(dbContext)
        {
        }

        public Task CreateDataAsync(VulcanHealthData data)
        {
            this.DbContext.Add(data);
            return this.DbContext.SaveChangesAsync();
        }

        public async Task<VulcanHealthData> GetDataByOwnerIdAsync(Guid ownerId)
        {
            return await this.DbContext.HealthDatas.FirstOrDefaultAsync(x => x.CreatorId == ownerId);
        }

        public async Task UpdateDataAsync(VulcanHealthData data)
        {
            var dbData = await this.DbContext.HealthDatas.FirstOrDefaultAsync(x => x.Id == data.Id);
            if (dbData == null)
                throw new Exception("没有找到指定的健康日报数据");

            dbData.WhenModified = DateTime.Now;
            this.DbContext.Update(dbData);
            await this.DbContext.SaveChangesAsync();
        }
    }
}