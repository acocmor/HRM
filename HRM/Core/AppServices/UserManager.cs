using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Exceptions;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Auth;
using HRM.Models.Paginate;
using HRM.Models.User;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace HRM.Core.AppServices
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Paginate<GetUserDTO>> GetAll(GetUsersFilter request)
        {
            request ??= new GetUsersFilter();
            
            var users = _userRepository.GetAll();
     
            if (!string.IsNullOrEmpty(request.Email))
            {
                users.Where(x => EF.Functions.Like(x.Email, $"%{request.Email}%")).Load();
            }
            
            var list = users.OrderBy(u => u.Id).Select(u => new GetUserDTO(u, u.Employee)).AsQueryable();
            return await list.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
            
            // var result = _mapper.ProjectTo<GetUserDTO>(users.OrderBy(x => x.Id));
            // return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }

        public async Task<GetUserDTO> GetById(Guid id)
        {
            return _mapper.Map<GetUserDTO>(await _userRepository.GetById(id));
        }

        public async Task<GetUserDTO> Create(CreateUserDTO request)
        {
            try
            {
                var users = await _userRepository.GetByEmail(request.Email);
                if (users != null)
                {
                    throw new ApiException("Địa chỉ email đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };
                }
                var user = _userRepository.Create(_mapper.Map<User>(request));
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                
                await _userRepository.SaveChangesAsync();
                return _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception e)
            {
                throw new ApiException($"{e.Message}") { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<bool> ChangePassword(Guid id, ChangePasswordDTO request)
        {
            var original = await _userRepository.GetById(id);
            if (original == null) throw new ApiException("Tài khoản này không tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            if(original.Password.Equals(request.OldPassword))
            {
                original.Password = request.NewPassword;
                original.UpdatedAt = DateTime.Now;
            } else
            {
                throw new ApiException("Mật khẩu cũ không chính xác.") { StatusCode = (int)HttpStatusCode.BadRequest };;
            }

            _userRepository.Update(original);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _userRepository.Delete(id);
            return await _userRepository.SaveChangesAsync() > 0;
        }

        public Task<GetUserDTO> Authencate(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        public Task<string> Generate(GetUserDTO login)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userRepository.Dispose();
            }
        }
    }
}
