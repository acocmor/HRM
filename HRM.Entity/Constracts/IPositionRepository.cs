using System.Threading.Tasks;
using HRM.Entity.Entities;

namespace HRM.Entity.Constracts
{
    public interface IPositionRepository : IRepository<Position>
    {
        Task<Position> GetByName(string name);
    }
}