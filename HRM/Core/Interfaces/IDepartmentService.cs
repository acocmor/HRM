using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Models.Department;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IDepartmentService : IDisposable
    {
        Task<List<GetDepartmentDTO>> GetAll();
        Task<GetDepartmentDTO> GetById(Guid id);
        Task<GetDepartmentDTO> Create(CreateDepartmentDTO request);
        Task<bool> Update(Guid id, UpdateDepartmentDTO request);
        Task<bool> Delete(Guid id);
    }
}
