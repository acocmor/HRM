using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public override IQueryable<Department> GetAll()
        {
            return DbSet.AsNoTracking()
                .AsNoTracking()
                .Include(x => x.Employees)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Position);
        }

        public override async Task<Department> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employees)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Position)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<Department> GetByName(string name)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employees)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Position)
                .FirstOrDefaultAsync(e => e.Name == name);
        }
    }
}
