using System;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Models.Paginate;
using HRM.Models.Position;

namespace HRM.Core.Interfaces
{
    public interface IPositionService : IDisposable
    {
        Task<Paginate<GetPositionDTO>> GetAll(GetPositionFilter filter);
        Task<GetPositionDTO> GetById(Guid id);
        Task<GetPositionDTO> Create(CreatePositionDTO request);
        Task<bool> Update(Guid id, UpdatePositionDTO request);
        Task<bool> Delete(Guid id);
    }
}
