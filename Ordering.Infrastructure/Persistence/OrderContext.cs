using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public Guid TenantId { get; private set; } = Guid.Parse("F8040104-03A2-41AE-8EA4-8DB568790BF6");

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = "user";
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = "user";
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var entityType = entity.ClrType;

                if (!typeof(IMultiTenant).IsAssignableFrom(entityType)) continue;

                var method = typeof(OrderContext).GetMethod(nameof(MultitenantExpression),
                    BindingFlags.NonPublic| BindingFlags.Static)?.MakeGenericMethod(entityType);


                var filter = method?.Invoke(null,new object[] { this });

                entity.SetQueryFilter((LambdaExpression)filter!);

                entity.AddIndex(entity.FindProperty(nameof(IMultiTenant.TenantId))!);

            }
        }

        private static LambdaExpression MultitenantExpression<T>(OrderContext context)
            where T : EntityBase, IMultiTenant
        {
            Expression<Func<T, bool>> tenantFilter = x => x.TenantId == context.TenantId;
            return tenantFilter;
        }
    }
}
