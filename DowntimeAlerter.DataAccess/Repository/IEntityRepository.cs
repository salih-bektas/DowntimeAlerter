using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.DataAccess.Repository
{
    public interface IEntityRepository<T> where T : class
    {
        IQueryable<T> Table { get; }

        Task<T> InsertAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(int id);

        T Get(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);

        List<T> GetList(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);

        T Add(T entity);
        Task<int> AddAsync(T entity);

        T Update(T entity);
        Task<T> UpdateAsync(T entity);

        T Update(T entity, params string[] exludeProperties);
        Task<int> UpdateAsync(T entity, params string[] exludeProperties);

        void Delete(T entity);

        IDbContextTransaction BeginTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();


        void AddWithoutSave(T entity);
        void UpdateWithoutSave(T entity);
        void UpdateWithoutSave(T entity, params string[] exludeProperties);
        void DeleteWithoutSave(T entity);


        void Detach(T entity);

        void SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
