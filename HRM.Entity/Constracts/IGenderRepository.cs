using System.Threading.Tasks;
using HRM.Entity.Entities;

namespace HRM.Entity.Constracts
{
    public interface IGenderRepository : IRepository<Gender>
    {
        Task<Gender> GetByName(string name);
    }
}