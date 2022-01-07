namespace Data.Entities.Common
{
    public interface IEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime CreationTime { get; set; }

    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }

    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
