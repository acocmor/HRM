using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Models.Employee;
using HRM.Models.Paginate;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IEmployeeService: IDisposable
    {
        Task<Paginate<GetEmployeeDTO>> GetAll(GetEmployeeFilter filter);
        Task<GetEmployeeDTO> GetById(Guid id);
        Task<GetEmployeeDTO> Create(CreateEmployeeDTO request);
        Task<bool> Update(Guid id, UpdateEmployeeDTO request);
        Task<bool> Delete(Guid id);
    }
}
