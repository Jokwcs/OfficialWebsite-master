using System.Reflection;

namespace Limit.OfficialSite.Domain.Entities
{
    public class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        public virtual TPrimaryKey Id { get; set; }

        //public virtual string CreateId { get; set; }
        //public virtual DateTime? CreateTime { get; set; } = DateTime.Now;
        //public virtual string LastModifyId { get; set; }
        //public virtual DateTime? LastModifyTime { get; set; } = DateTime.Now;

        //public virtual DateTime? DeleteTime { get; set; }
        //public virtual bool IsDelete { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity<TPrimaryKey>))
            {
                return false;
            }

            //同样的情况必须视为平等
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (Entity<TPrimaryKey>)obj;
            //必须具有类型的IS-A关系，还是必须具有相同的类型
            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Id);
        }

        public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
        {
            return !(left == right);
        }
    }
}