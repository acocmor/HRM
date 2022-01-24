using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Models.Department;
using HRM.Models.Paginate;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IDepartmentService : IDisposable
    {
        Task<Paginate<GetDepartmentDTO>> GetAll(GetDepartmentsFilter filter);
        Task<GetDepartmentDTO> GetById(Guid id);
        Task<GetDepartmentDTO> Create(CreateDepartmentDTO request);
        Task<bool> Update(Guid id, UpdateDepartmentDTO request);
        Task<bool> Delete(Guid id);
    }
}
