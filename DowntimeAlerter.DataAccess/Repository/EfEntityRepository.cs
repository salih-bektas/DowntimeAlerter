using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.DataAccess.Repository
{
    public abstract class EfEntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _tContext = null;

        public EfEntityRepository(DbContext context)
        {
            _tContext = context;
        }
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _tContext.Set<TEntity>().AddAsync(entity);
            await _tContext.SaveChangesAsync();
            return entity;
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _tContext.Set<TEntity>().SingleOrDefault(filter);
        }

        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> filter)
        {
            return filter == null
                ? _tContext.Set<TEntity>().ToList()
                : _tContext.Set<TEntity>().Where(filter).ToList();
        }

        public virtual TEntity Add(TEntity entity)
        {
            var addedEntity = _tContext.Entry(entity);
            addedEntity.State = EntityState.Added;

            _tContext.SaveChanges();
            return addedEntity.Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            var updatedEntity = _tContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _tContext.SaveChanges();
            return updatedEntity.Entity;
        }

        public virtual TEntity Update(TEntity entity, params string[] excludeProperties)
        {
            var updatedEntity = _tContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;

            foreach (var propertyName in excludeProperties)
            {
                updatedEntity.Property(propertyName).IsModified = false;
            }

            _tContext.SaveChanges();
            return updatedEntity.Entity;
        }
                

        public IQueryable<TEntity> Table
        {
            get { return this.Entities; }
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                return _tContext.Set<TEntity>();
            }
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return _tContext.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
               ? _tContext.Set<TEntity>().ToListAsync()
               : _tContext.Set<TEntity>().Where(filter).ToListAsync();
        }

        public Task<int> AddAsync(TEntity entity)
        {
            var addedEntity = _tContext.Entry(entity);
            addedEntity.State = EntityState.Added;

            return _tContext.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntity = _tContext.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _tContext.SaveChanges();

            await _tContext.SaveChangesAsync();
            return updatedEntity.Entity;
        }

        public Task<int> UpdateAsync(TEntity entity, params string[] exludeProperties)
        {
            var updatedEntity = _tContext.Entry(entity);

            updatedEntity.State = EntityState.Modified;

            foreach (var propertyName in exludeProperties)
            {
                updatedEntity.Property(propertyName).IsModified = false;
            }

            return _tContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(TEntity entity)
        {
            var deletedEntity = _tContext.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            return _tContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {

            var entity = _tContext.Set<TEntity>().FindAsync(id);
            var deletedEntity = _tContext.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            return _tContext.SaveChangesAsync();
        }


    }
}
