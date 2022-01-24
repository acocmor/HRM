using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Models.Address;
using HRM.Models.Department;
using HRM.Models.Paginate;
using HRM.Models.Position;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IAddressService
    {
        Task<Paginate<GetAddressDTO>> GetAll(GetAddressFilter filter);
        Task<GetAddressDTO> GetById(Guid id);
        Task<GetAddressDTO> Create(CreateAddressDTO request);
        Task<bool> Update(Guid id, UpdateAddressDTO request);
        Task<bool> Delete(Guid id);
    }
}
