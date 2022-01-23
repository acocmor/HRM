using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Models.Employee;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IEmployeeService: IDisposable
    {
        Task<List<GetEmployeeDTO>> GetAll();
        Task<GetEmployeeDTO> GetById(Guid id);
        Task<GetEmployeeDTO> Create(CreateEmployeeDTO request);
        Task<GetEmployeeDTO> Update(Guid id, UpdateEmployeeDTO request);
        Task<bool> Delete(Guid id);
    }
}
