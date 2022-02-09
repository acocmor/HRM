using System.Threading.Tasks;
using HRM.Models.Auth;

namespace HRM.Core.Interfaces
{
    public interface IAuthService
    {
        Task<TokenModel> Login(LoginDTO request);
        Task<bool> Logout(TokenModel request);
        Task<TokenModel> RenewToken(TokenModel request);
    }
}