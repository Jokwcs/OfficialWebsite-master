using Limit.OfficialSite.Domain.Entities;
using Limit.OfficialSite.Domain.Repositories;

namespace Limit.OfficialSite.EntityFrameworkCore.Repositories
{
    public class EfCoreRepository<TEntity> : EfCoreRepository<TEntity, int>, IRepository<TEntity> where TEntity : class, IEntity, IAggregateRoot
    {
        public EfCoreRepository(ApplicationDbContext dbDbContext)
            : base(dbDbContext)
        {

        }
    }
}