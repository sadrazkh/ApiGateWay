using Data.Repositories;

using Data.UnitOfWork.Base;

namespace Data.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(Tools.Options options) : base(options)
        {
        }

    }
}
