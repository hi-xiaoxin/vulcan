using Dynastech.Vulcan.HealthDay;
using Microsoft.EntityFrameworkCore;
using System;

namespace Dynastech.Vulcan.EntityFrameworkCore
{
    public class VulcanDbContext : DbContext
    {
        public DbSet<HealthData> HealthDatas { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HealthData>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}
