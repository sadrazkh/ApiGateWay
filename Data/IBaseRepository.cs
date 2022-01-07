using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities.Common;


namespace Data
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        Task AddAsync(T entity, CancellationToken cancellationToken);
        void AddRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void Detach(T entity);
        T GetById(params object[] ids);
        ValueTask<T> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        void LoadCollection<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
        Task LoadCollectionAsync<TProperty>(T entity, Expression<Func<T, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
        void LoadReference<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty) where TProperty : class;
        Task LoadReferenceAsync<TProperty>(T entity, Expression<Func<T, TProperty>> referenceProperty, CancellationToken cancellationToken) where TProperty : class;
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}