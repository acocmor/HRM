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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Employee> GetAll()
        {
            return DbSet.AsNoTracking()
                .AsNoTracking()
                .Include(x => x.Gender)
                .Include(x => x.User)
                .Include(x => x.Address)
                .Include(x => x.Department)
                .Include(x => x.Position);
        }

        public override async Task<Employee> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Gender)
                .Include(x => x.User)
                .Include(x => x.Address)
                .Include(x => x.Department)
                .Include(x => x.Position)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
