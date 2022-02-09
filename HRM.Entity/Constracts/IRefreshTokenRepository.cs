using System.Threading.Tasks;
using HRM.Entity.Entities;

namespace HRM.Entity.Constracts
{
    public interface IRefreshTokenRepository: IRepository<RefreshToken>
    {
        Task<RefreshToken> GetByToken(string token);
    }
}