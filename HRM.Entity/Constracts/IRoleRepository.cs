using System.Threading.Tasks;
using HRM.Entity.Entities;

namespace HRM.Entity.Constracts
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByName(string name);
    }
}