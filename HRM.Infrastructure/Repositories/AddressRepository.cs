
using System;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Infrastructure.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<Address> GetAll()
        {
            return DbSet.AsNoTracking()
                .AsNoTracking()
                .Include(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employee)
                .ThenInclude(x => x.User)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Position);
        }

        public override async Task<Address> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employee)
                .ThenInclude(x => x.User)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Position)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
