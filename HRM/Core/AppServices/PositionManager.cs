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
using HRM.Models.Position;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Models.Paginate;
using HRM.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace HRM.Core.AppServices
{
    public class PositionManager : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionManager(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }

        public async Task<Paginate<GetPositionDTO>> GetAll(GetPositionFilter request)
        {
            request ??= new GetPositionFilter();
            
            var positions = _positionRepository.GetAll();
     
            if (!string.IsNullOrEmpty(request.Name))
            {
                positions.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%")).Load();
            }

            var result = _mapper.ProjectTo<GetPositionDTO>(positions.OrderBy(x => x.Id));
            return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }

        public async Task<GetPositionDTO> GetById(Guid id)
        {
            return _mapper.Map<GetPositionDTO>(await _positionRepository.GetById(id));
        }

        public async Task<GetPositionDTO> Create(CreatePositionDTO request)
        {
            var name = await _positionRepository.GetByName(request.Name);
            if (name != null)
            {
                throw new ApiException("Phòng ban này đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            var newPosition = _mapper.Map<Position>(request);
            newPosition.CreatedAt = DateTime.Now;
            newPosition.UpdatedAt = DateTime.Now;
            
            var position = _positionRepository.Create(newPosition);
            await _positionRepository.SaveChangesAsync();
            return _mapper.Map<GetPositionDTO>(position);
        }

        public async Task<bool> Update(Guid id, UpdatePositionDTO request)
        {
            var original = await _positionRepository.GetById(id);
            if (original == null) throw new ApiException("Phòng ban này không tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            var name = await _positionRepository.GetByName(request.Name);
            if (name != null) throw new ApiException("Phòng ban này đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            original.Name = request.Name;
            original.UpdatedAt = DateTime.Now;

            _positionRepository.Update(original);
            await _positionRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _positionRepository.Delete(id);
            return await _positionRepository.SaveChangesAsync() > 0;
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
                _positionRepository.Dispose();
            }
        }
    }
}
