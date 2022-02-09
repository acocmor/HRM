using System.Linq;
using System.Threading.Tasks;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HRM.Infrastructure.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Token == token);
        }
    }
}