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
    public class GenderRepository : Repository<Gender>, IGenderRepository
    {
        public GenderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Gender> GetAll()
        {
            return DbSet.AsNoTracking()
                .AsNoTracking()
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Position)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Department);
        }

        public override async Task<Gender> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employees)
                .ThenInclude(x => x.User)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Position)
                .Include(x => x.Employees)
                .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }

    
}
