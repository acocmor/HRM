using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Models.Position;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IPositionService : IDisposable
    {
        Task<List<GetPositionDTO>> GetAll();
        Task<GetPositionDTO> GetById(Guid id);
        Task<GetPositionDTO> Create(CreatePositionDTO request);
        Task<bool> Update(Guid id, UpdatePositionDTO request);
        Task<bool> Delete(Guid id);
    }
}
