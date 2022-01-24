using System.Threading.Tasks;
using HRM.Entity.Entities;

namespace HRM.Entity.Constracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
        Task<User> Login(string email, string password);
    }
}