using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity, IAggregateRoot
    {

    }
}