using System.Threading.Tasks;
using HRM.Models.Auth;

namespace HRM.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDTO request);
    }
}