using System.Collections.Generic;
using Common.Utilities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities.Common;
using Data.Entities.Role;
using Data.Entities.User;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Data
{
    //todo make this class internal
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyApiDb;Integrated Security=true");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
            //modelBuilder.AddSoftDelete<BaseEntity>(entitiesAssembly);
        }

        #region OverRide Save Change Methods

        public override int SaveChanges()
        {
            _cleanString();

            var removedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);
            foreach (var removedEntity in removedEntities)
            {
                var prop = removedEntity.GetType().GetProperty("IsDeleted", typeof(bool));
                if (prop != null)
                    prop.SetValue(removedEntity, false);
            }

            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();

            var removedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);
            foreach (var removedEntity in removedEntities)
            {
                var prop = removedEntity.GetType().GetProperty("IsDeleted", typeof(bool));
                if (prop != null)
                    prop.SetValue(removedEntity, false);
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();

            var removedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);
            foreach (var removedEntity in removedEntities)
            {
                var prop = removedEntity.GetType().GetProperty("IsDeleted", typeof(bool));
                if (prop != null)
                    prop.SetValue(removedEntity, false);
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();

            var removedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified);
            foreach (var removedEntity in removedEntities)
            {
                var prop = removedEntity.GetType().GetProperty("IsDeleted", typeof(bool));
                if (prop != null)
                    prop.SetValue(removedEntity, false);
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        #endregion

        #region OverRide Add Methods

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
            if (prop != null)
                prop.SetValue(entity, DateTime.UtcNow);

            return base.Add(entity);
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var prop =  entity.GetType().GetProperty("CreationTime", typeof(DateTime));
            if (prop != null)
                prop.SetValue(entity, DateTime.UtcNow);
            
            return base.AddAsync(entity, cancellationToken);
        }

        public override Task AddRangeAsync(params object[] entities)
        {
            foreach (var entity in entities)
            {
                var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
                if (prop != null)
                    prop.SetValue(entity, DateTime.UtcNow);
            }

            return base.AddRangeAsync(entities);
        }

        public override Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entity in entities)
            {
                var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
                if (prop != null)
                    prop.SetValue(entity, DateTime.UtcNow);
            }

            return base.AddRangeAsync(entities, cancellationToken);
        }

        public override ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = new CancellationToken())
        {
            var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
            if (prop != null)
                prop.SetValue(entity, DateTime.UtcNow);

            return base.AddAsync(entity, cancellationToken);
        }

        public override EntityEntry Add(object entity)
        {
            var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
            if (prop != null)
                prop.SetValue(entity, DateTime.UtcNow);

            return base.Add(entity);
        }

        public override void AddRange(params object[] entities)
        {
            foreach (var entity in entities)
            {
                var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
                if (prop != null)
                    prop.SetValue(entity, DateTime.UtcNow);
            }

            base.AddRange(entities);
        }

        public override void AddRange(IEnumerable<object> entities)
        {
            foreach (var entity in entities)
            {
                var prop = entity.GetType().GetProperty("CreationTime", typeof(DateTime));
                if (prop != null)
                    prop.SetValue(entity, DateTime.UtcNow);
            }

            base.AddRange(entities);
        }

        #endregion

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.Fa2En().FixPersianChars();
                        if (newVal == val)
                            continue;
                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }
    }
}
