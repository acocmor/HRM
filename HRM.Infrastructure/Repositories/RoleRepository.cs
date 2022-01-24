using System;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        
        public override IQueryable<Role> GetAll()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Position)
        }
        
        public async override Task<Role> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Position)
                .SingleOrDefaultAsync(r => r.Id.Equals(id));
        }

        public async Task<Role> GetByName(string name)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Include(x => x.Users)
                .ThenInclude(x => x.Employee)
                .ThenInclude(x => x.Position)
                .SingleOrDefaultAsync();
        }
    }
}