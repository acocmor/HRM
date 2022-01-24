using System.Threading.Tasks;
using HRM.Entity.Entities;

namespace HRM.Entity.Constracts
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<Department> GetByName(string name);
    }
}