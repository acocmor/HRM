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
using HRM.Models.Gender;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Models.Paginate;
using HRM.Models.Position;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.AppServices
{
    public class GenderManager : IGenderService
    {

        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GenderManager(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        public async Task<Paginate<GetGenderDTO>> GetAll(GetGenderFilter request)
        {
            request ??= new GetGenderFilter();
            
            var positions = _genderRepository.GetAll();
     
            if (!string.IsNullOrEmpty(request.Name))
            {
                positions.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%")).Load();
            }

            var result = _mapper.ProjectTo<GetGenderDTO>(positions.OrderBy(x => x.Id));
            return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }

        public async Task<GetGenderDTO> GetById(Guid id)
        {
            return _mapper.Map<GetGenderDTO>(await _genderRepository.GetById(id));
        }

        public async Task<GetGenderDTO> Create(CreateGenderDTO request)
        {
            var name = await _genderRepository.GetByName(request.Name);
            if (name != null)
            {
                throw new ApiException("Giới tính này đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            var newGender = _mapper.Map<Gender>(request);
            newGender.CreatedAt = DateTime.Now;
            newGender.UpdatedAt = DateTime.Now;
            
            var gender = _genderRepository.Create(newGender);
            await _genderRepository.SaveChangesAsync();
            return _mapper.Map<GetGenderDTO>(gender);
        }

        public async Task<bool> Update(Guid id, UpdateGenderDTO request)
        {
            var original = await _genderRepository.GetById(id);
            if (original == null) throw new ApiException("Giới tính này không tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            var name = await _genderRepository.GetByName(request.Name);
            if (name != null) throw new ApiException("Giới tính này đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            original.Name = request.Name;
            original.UpdatedAt = DateTime.Now;

            _genderRepository.Update(original);
            await _genderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _genderRepository.Delete(id);
            return await _genderRepository.SaveChangesAsync() > 0;
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
                _genderRepository.Dispose();
            }
        }
    }
}
