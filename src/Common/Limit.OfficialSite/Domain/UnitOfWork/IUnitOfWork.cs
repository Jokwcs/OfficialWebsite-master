using System.Threading.Tasks;
using Limit.OfficialSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Limit.OfficialSite.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// 获取 DbSet
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : Entity;

        /// <summary>
        /// 将在此上下文中进行的所有更改保存到底层数据库
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}