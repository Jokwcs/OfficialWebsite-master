using System;
using System.Threading.Tasks;
using Limit.OfficialSite.Domain.Entities;
using Limit.OfficialSite.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Limit.OfficialSite.Uow
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public UnitOfWork(TContext context) => _dbContext = context ?? throw new ArgumentNullException(nameof(context));

        /// <summary>
        /// 获取 DbSet
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns>DbSet</returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : Entity
        {
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 将在此上下文中进行的所有更改保存到底层数据库
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}