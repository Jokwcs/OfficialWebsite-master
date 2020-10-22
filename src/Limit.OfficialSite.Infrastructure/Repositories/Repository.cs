using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Limit.OfficialSite.Domain.Entities;
using Limit.OfficialSite.Domain.Repositories;

namespace Limit.OfficialSite.Repositories
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    /// <typeparam name="TPrimaryKey">泛型主键</typeparam>
    public abstract class Repository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
      where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {

        #region select

        public abstract bool Exist(TPrimaryKey id);
        public abstract Task<bool> ExistAsync(TPrimaryKey id);
        public abstract bool Exist(Expression<Func<TEntity, bool>> predicate);
        public abstract Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

        public abstract IQueryable<TEntity> GetAll();
        public abstract IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate = null);
        public abstract Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate = null);

        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public abstract Task<List<TEntity>> GetAllListAsync();

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public abstract Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            return FirstOrDefault(id);
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await FirstOrDefaultAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">每页的数据</param>
        /// <param name="total">总数</param>
        /// <param name="wherePredicate">筛选条件</param>
        /// <param name="orderByPredicate">降序条件</param>
        /// <param name="isDesc">是否降序</param>
        /// <returns></returns>
        public abstract List<TEntity> PagingQuery(
            int pageIndex,
            int pageSize,
            out int total,
            Expression<Func<TEntity, bool>> wherePredicate = null,
            Expression<Func<TEntity, bool>> orderByPredicate = null,
            bool isDesc = true);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">每页的数据</param>
        /// <param name="wherePredicate">筛选条件</param>
        /// <param name="orderByPredicate">降序条件</param>
        /// <param name="isDesc">是否降序</param>
        /// <returns></returns>
        public abstract Task<(List<TEntity>, long)> PagingQueryAsync<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> wherePredicate = null,
            Expression<Func<TEntity, TKey>> orderByPredicate = null,
            bool isDesc = true);


        public virtual TEntity FirstOrDefault()
        {
            return GetAll().FirstOrDefault();
        }

        public abstract Task<TEntity> FirstOrDefaultAsync();

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public abstract Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public abstract Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Insert

        public abstract TEntity Insert(TEntity entity);

        public abstract Task<TEntity> InsertAsync(TEntity entity);

        public abstract void InsertRange(IEnumerable<TEntity> entities);
        public abstract Task InsertRangeAsync(IEnumerable<TEntity> entities);

        public virtual TPrimaryKey InsertAndGetId(TEntity entity) => Insert(entity).Id;

        public virtual Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            return Task.Run(async () => await InsertAndGetIdAsync(entity));
        }

        #endregion

        #region Update

        public abstract TEntity Update(TEntity entity);

        public abstract Task<TEntity> UpdateAsync(TEntity entity);

        #endregion

        #region Delete

        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(TPrimaryKey id);

        public virtual Task DeleteAsync(TPrimaryKey id)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var entity in GetAll().Where(predicate).ToList())
            {
                Delete(entity);
            }
        }

        public virtual Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(predicate);
            return Task.FromResult(0);
        }

        #endregion

        #region Aggregates

        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        #endregion


        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
            );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}