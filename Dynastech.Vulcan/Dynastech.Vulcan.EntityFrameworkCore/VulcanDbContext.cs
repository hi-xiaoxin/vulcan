using Dynastech.Vulcan.HealthDay;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dynastech.Vulcan.EntityFrameworkCore
{
    public class VulcanDbContext : DbContext
    {
        public DbSet<VulcanHealthData> HealthDatas { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VulcanHealthData>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}
