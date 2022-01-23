using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Entity.Constracts
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(Guid id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        Task Delete(Guid id);
        Task<int> SaveChangesAsync();
    }
}
