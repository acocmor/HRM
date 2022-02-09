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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<User> GetAll()
        {
            return DbSet.AsNoTracking()
                .Include(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Position)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department);
        }

        public override async Task<User> GetById(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Position)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Position)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(e => e.Email.Equals(email));
        }

        public async Task<User> Login(string email, string password)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Employee)
                .ThenInclude(x => x.Gender)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Address)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Position)
                .Include(x => x.Employee)
                .ThenInclude(x => x.Department)
                .FirstOrDefaultAsync(e => e.Email.Equals(email) && e.Password == password);
        }
    }
}
