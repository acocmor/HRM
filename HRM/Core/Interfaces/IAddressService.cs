using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Models.Address;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IAddressService
    {
        Task<List<GetAddressDTO>> GetAll();
        Task<GetAddressDTO> GetById(Guid id);
        Task<GetAddressDTO> Create(CreateAddressDTO request);
        Task<bool> Update(Guid id, UpdateAddressDTO request);
        Task<bool> Delete(Guid id);
    }
}
