using Microsoft.EntityFrameworkCore;
using Sales.Core.Interfaces;
using Sales.Infrastructure.Data.EfCore;
using Sales.Infrastructure.Data.EfCore.EntityConfigurations;

namespace Sales.Infrastructure.Data.EfCore
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;
            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
            modelBuilder.AddPluralizingTableNameConvention();
        }
    }
}