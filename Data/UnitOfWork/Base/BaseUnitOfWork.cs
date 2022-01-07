using System;
using System.Threading;
using System.Threading.Tasks;
using Data.Entities.Common;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Data.UnitOfWork.Base
{
    public abstract class BaseUnitOfWork : IBaseUnitOfWork
    {
        public BaseUnitOfWork(Tools.Options options) : base()
        {
            Options = options;
        }

        protected Tools.Options Options { get; set; }

        private ApplicationDbContext _databaseContext;

        internal ApplicationDbContext DatabaseContext
        {
            get
            {
                if (_databaseContext == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                    optionsBuilder.UseSqlServer(Options.ConnectionString);

                    _databaseContext = new ApplicationDbContext(options: optionsBuilder.Options);
                }

                return _databaseContext;
            }
        }

        //todo check public
        public BaseRepository<T> GetRepository<T>() where T : class, IEntity
        {
            return new BaseRepository<T>(dbContext: DatabaseContext);
        }

        public virtual void Save()
        {
            DatabaseContext.SaveChanges();
        }

        public virtual async Task SaveAsync(CancellationToken cancellationToken)
        {
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        public bool IsDisposed { get; protected set; }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).

                if (_databaseContext != null)
                {
                    _databaseContext.Dispose();
                    _databaseContext = null;
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            IsDisposed = true;
        }

        ~BaseUnitOfWork()
        {
            Dispose(false);
        }
    }
}