using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Limit.OfficialSite.Domain.Entities;
using Limit.OfficialSite.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Limit.OfficialSite.EntityFrameworkCore.Repositories
{
    public class EfCoreRepository<TEntity, TPrimaryKey> : Repository<TEntity, TPrimaryKey>, IRepositoryWithDbContext
      where TEntity : class, IEntity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        private readonly ApplicationDbContext _dbContext;

        public DbContext GetDbContext()
        {
            return _dbContext;
        }

        public virtual DbSet<TEntity> Table => _dbContext.Set<TEntity>();

        public EfCoreRepository(ApplicationDbContext dbDbContext)
        {
            _dbContext = dbDbContext;
        }
        public override IQueryable<TEntity> Load(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? Table
                : Table.Where(predicate);
        }

        public override async Task<IQueryable<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await Task.Run(() => Load(predicate));
        }

        public override bool Exist(TPrimaryKey id)
        {
            return Table.Any(CreateEqualityExpressionForId(id));
        }

        public override Task<bool> ExistAsync(TPrimaryKey id)
        {
            return Task.Run(() => Table.Any(CreateEqualityExpressionForId(id)));
        }

        public override bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Any(predicate);
        }

        public override async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table.AsQueryable();
        }

        public override List<TEntity> PagingQuery(
            int pageIndex,
            int pageSize,
            out int total,
            Expression<Func<TEntity, bool>> wherePredicate = null,
            Expression<Func<TEntity, bool>> orderByPredicate = null,
            bool isDesc = true)
        {
            var data = wherePredicate == null
                ? GetAll()
                : GetAll().Where(wherePredicate);

            total = data.Count();

            if (isDesc)
            {
                return orderByPredicate == null
                    ? data.ToList()
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize)
                        .ToList()
                    : data.OrderByDescending(orderByPredicate)
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize)
                        .ToList();
            }

            return orderByPredicate == null
                ? data.ToList()
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList()
                : data.OrderBy(orderByPredicate)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList();
        }

        public override async Task<(List<TEntity>, long)> PagingQueryAsync<TKey>(
            int pageIndex,
            int pageSize,
            Expression<Func<TEntity, bool>> wherePredicate = null,
            Expression<Func<TEntity, TKey>> orderByPredicate = null,
            bool isDesc = true)
        {
            var data = wherePredicate == null
                ? GetAll()
                : GetAll().Where(wherePredicate);

            var total = await data.CountAsync();

            if (isDesc)
            {
                return orderByPredicate == null
                    ? (data.ToList().Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize)
                        .ToList(), total)
                    : (await data.OrderByDescending(orderByPredicate)
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize)
                        .ToListAsync(), total);
            }

            return orderByPredicate == null
                ? (data.ToList().Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList(), total)
                : (data.OrderBy(orderByPredicate).ToList()
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToList(), total);
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await Table.ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.Where(predicate).ToListAsync();
        }

        public override async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Table.FirstOrDefaultAsync();
        }

        public override async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await Table.FindAsync(id);
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        public override TEntity Insert(TEntity entity)
        {
            return Table.Add(entity).Entity;
        }

        public override  Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public override void InsertRange(IEnumerable<TEntity> entities)
        {
            Table.AddRange(entities);
        }

        public override async Task InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => Table.AddRange(entities));
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.Run(() => Update(entity));
        }

        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity == null)
                return;
            Delete(entity);
        }


        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id)
        {
            var entry = _dbContext.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity entity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, entity.Id)
                );

            return entry?.Entity as TEntity;
        }

    }
}