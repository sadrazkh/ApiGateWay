using System.Linq.Expressions;
using Common.Utilities;
using Data.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntity
    {
        internal ApplicationDbContext DatabaseContext { get; }
        internal DbSet<T> DbSet { get; }

        internal BaseRepository(ApplicationDbContext dbContext) : base()
        {
            DatabaseContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            DbSet = DatabaseContext.Set<T>();
        }

        #region Async Method
        public virtual ValueTask<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return DbSet.FindAsync(ids, cancellationToken);
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            Assert.NotNull(entity, nameof(entity));

            await DbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            Assert.NotNull(entities, nameof(entities));

            await DbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region Sync Methods
        public virtual T GetById(params object[] ids)
        {
            return DbSet.Find(ids);
        }

        public virtual void Add(T entity)
        {
            Assert.NotNull(entity, nameof(entity));
            DbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            DbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            Assert.NotNull(entity, nameof(entity));
            DbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            DbSet.UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            Assert.NotNull(entity, nameof(entity));
            DbSet.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            Assert.NotNull(entities, nameof(entities));
            DbSet.RemoveRange(entities);
        }
        #endregion

        #region Attach & Detach
        public virtual void Detach(T entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = DatabaseContext.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(T entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (DatabaseContext.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);
        }


        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);

            var collection = DatabaseContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = DatabaseContext.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty, CancellationToken cancellationToken)
            where TProperty : class
        {
            Attach(entity);
            var reference = DatabaseContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = DatabaseContext.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion

    }
}