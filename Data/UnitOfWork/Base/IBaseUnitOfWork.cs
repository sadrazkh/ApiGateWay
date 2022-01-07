using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities.Common;

namespace Data.UnitOfWork.Base
{
    public interface IBaseUnitOfWork : IDisposable
    {
        bool IsDisposed { get; }

        Task SaveAsync(CancellationToken cancellationToken);

        BaseRepository<T> GetRepository<T>() where T : class, IEntity;
    }
}