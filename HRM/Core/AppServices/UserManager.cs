using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

            List<GetUserDTO> list = new List<GetUserDTO>();
            foreach (var item in users)
            {
                list.Add(new GetUserDTO()
                {
                    Id = item.Id,
                    Email = item.Email,
                });
            }
            //var result = _mapper.ProjectTo<GetUserDTO>(users).OrderBy(x => x.Id);
            
            return await list.AsQueryable().ToPaginatedListAsync(request.CurrentPage, request.PageSize);
            return null;
        }

        public Task<GetUserDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDTO> Create(CreateUserDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(Guid id, ChangePasswordDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
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
