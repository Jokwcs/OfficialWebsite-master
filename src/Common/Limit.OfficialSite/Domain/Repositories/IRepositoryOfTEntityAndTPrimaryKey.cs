using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Limit.OfficialSite.Domain.Entities;

namespace Limit.OfficialSite.Domain.Repositories
{
    /// <summary>
    /// 此接口由所有Repository实现
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    /// <typeparam name="TPrimaryKey">泛型主键</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository
        where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {

        #region Select

        /// <summary>
        /// 具有给定主键的实体是否存在.
        /// </summary>
        /// <param name="id">实体的主键</param>
        /// <returns><see cref="bool"/></returns>
        bool Exist(TPrimaryKey id);
        /// <summary>
        /// 具有给定主键的实体是否存在.
        /// </summary>
        /// <param name="id">实体的主键</param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> ExistAsync(TPrimaryKey id);

        /// <summary>
        /// 满足给定的<paramref name="predicate"/>的实体是否存在.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns><see cref="bool"/></returns>
        bool Exist(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 满足给定的<paramref name="predicate"/>的实体是否存在.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns><see cref="bool"/></returns>
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于获取用于从整个表中检索实体的IQueryable.
        /// </summary>
        /// <returns>可用于从数据库中选择实体</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 获取具有给定主键的实体.
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体</returns>
        TEntity Get(TPrimaryKey id);

        /// <summary>
        /// 获取具有给定主键的实体.
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体</returns>
        Task<TEntity> GetAsync(TPrimaryKey id);

        /// <summary>
        /// 用于获取所有实体.
        /// </summary>
        /// <returns>用于列出所有实体获取所有实体</returns>
        List<TEntity> GetAllList();

        IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate = null);
        Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="total">总数</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">每页的数据</param>
        /// <param name="wherePredicate">筛选条件</param>
        /// <param name="orderByPredicate">排序条件</param>
        /// <param name="isDesc">是否降序</param>
        /// <returns></returns>
        List<TEntity> PagingQuery(
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
        /// <param name="orderByPredicate">排序条件</param>
        /// <param name="isDesc">是否降序</param>  
        /// <returns></returns>
        Task<(List<TEntity>, long)> PagingQueryAsync<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity,bool>> wherePredicate=null,
            Expression<Func<TEntity, TKey>> orderByPredicate=null,
            bool isDesc=true);
           
        /// <summary> 
        /// 用于获取所有实体.
        /// </summary>
        /// <returns>用于列出所有实体获取所有实体</returns>
        Task<List<TEntity>> GetAllListAsync();

        /// <summary>
        /// 用于根据给定的<paramref name="predicate"/>获取所有实体.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns>所有实体列表</returns>
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///用于根据给定的<paramref name="predicate"/>获取所有实体.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        /// <returns>所有实体列表</returns>
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 用于在整个实体上运行查询.
        /// 如果<paramref name="queryMethod"/>完成IQueryable与ToList, FirstOrDefault等……
        /// </summary>
        /// <typeparam name="T">此方法的返回值的类型</typeparam>
        /// <param name="queryMethod">此方法用于查询实体</param>
        /// <returns>查询结果</returns>
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);

        /// <summary>
        /// 查询集合中的第一条数据，如果集合空返回则null
        /// </summary>
        /// <returns></returns>
        TEntity FirstOrDefault();

        /// <summary>
        /// 查询集合中的第一条数据，如果集合空返回则null
        /// </summary>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync();

        /// <summary>
        /// 获取具有给定主键的实体，如果没有找到，则返回null.
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体或null</returns>
        TEntity FirstOrDefault(TPrimaryKey id);

        /// <summary>
        /// 获取具有给定主键的实体，如果没有找到，则返回null.
        /// </summary>
        /// <param name="id">要获取的实体的主键</param>
        /// <returns>实体或null</returns>
        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        /// <summary>
        /// 获取具有给定给定谓词的实体，如果没有找到，则返回null.
        /// </summary>
        /// <param name="predicate">谓词过滤实体</param>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取具有给定给定谓词的实体，如果没有找到，则返回null.
        /// </summary>
        /// <param name="predicate">谓词过滤实体</param>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Insert

        /// <summary>
        /// 插入一个新实体.
        /// </summary>
        /// <param name="entity">插入的实体</param>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入一个新实体.
        /// </summary>
        /// <param name="entity">插入的实体</param>
        Task<TEntity> InsertAsync(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        Task InsertRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 插入一个新实体并获取它的Id.
        /// 它可能需要保存当前的工作单元 能够检索id
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体Id</returns>
        TPrimaryKey InsertAndGetId(TEntity entity);

        /// <summary>
        /// 插入一个新实体并获取它的Id.
        /// 它可能需要保存当前的工作单元 能够检索id
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体Id</returns>
        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        #endregion Insert

        #region Update

        /// <summary>
        /// 更新现有实体.
        /// </summary>
        /// <param name="entity">实体</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// 更新现有实体.
        /// </summary>
        /// <param name="entity">实体</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        #endregion Update

        #region Delete

        /// <summary>
        /// 删除一个实体.
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 删除一个实体.
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 通过主键删除实体.
        /// </summary>
        /// <param name="id">实体的主键</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 通过主键删除实体.
        /// </summary>
        /// <param name="id">实体的主键</param>
        Task DeleteAsync(TPrimaryKey id);

        /// <summary>
        /// 按谓词删除多个实体.
        /// 注意:所有符合给定谓词的实体都将被检索和删除.
        /// 如果使用给定谓词的实体太多，这可能会导致主要的性能问题.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 按谓词删除多个实体.
        /// 注意:所有符合给定谓词的实体都将被检索和删除.
        /// 如果使用给定谓词的实体太多，这可能会导致主要的性能问题.
        /// </summary>
        /// <param name="predicate">筛选实体的条件</param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Aggregates

        /// <summary>
        /// 获取此Repository中所有实体的个数.
        /// </summary>
        /// <returns>实体的个数</returns>
        int Count();

        /// <summary>
        /// 获取此Repository中所有实体的个数.
        /// </summary>
        /// <returns>实体的个数</returns>
        Task<int> CountAsync();

        /// <summary>
        /// 根据给定的<paramref name="predicate"/>获取存储库中所有实体的计数.
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体的个数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据给定的<paramref name="predicate"/>获取存储库中所有实体的计数.
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体的个数</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        ///方法获取此Repository中所有实体的计数(如果期望的返回值是greather而不是 <see cref="int.MaxValue"/>)则使用该方法来过滤.
        /// </summary>
        /// <returns>实体的个数</returns>
        long LongCount();

        /// <summary>
        ///方法获取此Repository中所有实体的计数(如果期望的返回值是greather而不是 <see cref="int.MaxValue"/>)则使用该方法来过滤.
        /// </summary>
        /// <returns>实体的个数</returns>
        Task<long> LongCountAsync();

        /// <summary>
        /// 根据给定的<paramref name="predicate"/>获取存储库中所有实体的计数
        /// (如果期望的返回值是greather，而不是<see cref="int.MaxValue"/>，则使用此重载).
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体的个数</returns>
        long LongCount(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据给定的<paramref name="predicate"/>获取存储库中所有实体的计数
        /// (如果期望的返回值是greather，而不是<see cref="int.MaxValue"/>，则使用此重载).
        /// </summary>
        /// <param name="predicate">一种过滤计数的方法</param>
        /// <returns>实体的个数</returns>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Execute Sql

        ///// <summary>
        ///// 创建一个原始SQL查询，该查询将返回给定泛型类型的元素
        ///// </summary>
        ///// <param name="sql">SQL查询字符串</param>
        ///// <param name="args">要应用到SQL查询字符串的参数</param>
        ///// <returns></returns>
        //List<TEntity> SqlQuery<T>(string sql, params object[] args);

        ///// <summary>
        ///// 创建一个原始SQL查询，该查询将返回给定泛型类型的元素
        ///// </summary>
        ///// <param name="sql">SQL查询字符串</param>
        ///// <param name="args">要应用到SQL查询字符串的参数</param>
        ///// <returns></returns>
        //Task<List<TEntity>> SqlQueryAsync<T>(string sql, params object[] args);

        ///// <summary>
        ///// 对数据库执行给定的DDL/DML命令
        ///// </summary>
        ///// <param name="sql">命令字符串</param>
        ///// <param name="parameters">要应用到命令字符串的参数</param>
        ///// <returns></returns>
        //int ExecuteSqlCommand(string sql, params object[] parameters);

        ///// <summary>
        ///// 对数据库执行给定的DDL/DML命令
        ///// </summary>
        ///// <param name="sql">命令字符串</param>
        ///// <param name="parameters">要应用到命令字符串的参数</param>
        ///// <returns></returns>
        //Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        #endregion

    }
}