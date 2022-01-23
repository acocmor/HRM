using HRM.Entity.Constracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Entity.Common;
using HRM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        protected ApplicationDbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        public Repository(ApplicationDbContext dbContext)
        {
            Context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual TEntity Create(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            //DbSet.Update(entity);
            return entity;
        }

        public virtual async Task Delete(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
            {
                //Context.Entry(entity).State = EntityState.Deleted;
                DbSet.Remove(entity);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) Context.Dispose();
        }
    }
}
