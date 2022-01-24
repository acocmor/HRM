using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Models.Gender;
using HRM.Models.Paginate;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IGenderService : IDisposable
    {
        Task<Paginate<GetGenderDTO>> GetAll(GetGenderFilter filter);
        Task<GetGenderDTO> GetById(Guid id);
        Task<GetGenderDTO> Create(CreateGenderDTO request);
        Task<bool> Update(Guid id, UpdateGenderDTO request);
        Task<bool> Delete(Guid id);
    }
}
