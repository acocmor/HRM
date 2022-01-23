using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Core.Filters;
using HRM.Models.Auth;
using HRM.Models.Paginate;
using HRM.Models.User;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<Paginate<GetUserDTO>> GetAll(GetUsersFilter filter);
        Task<GetUserDTO> GetById(Guid id);
        Task<GetUserDTO> Create(CreateUserDTO request);
        Task<bool> ChangePassword(Guid id, ChangePasswordDTO request);
        Task<bool> Delete(Guid id);
        Task<GetUserDTO> Authencate(LoginDTO login);
        Task<string> Generate(GetUserDTO login);
    }
}
