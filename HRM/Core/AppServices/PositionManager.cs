using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Interfaces;
using HRM.Models.Position;
using HRM.Entity.Constracts;
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

        public Task<List<GetPositionDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GetPositionDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetPositionDTO> Create(CreatePositionDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, UpdatePositionDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
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
                _positionRepository.Dispose();
            }
        }
    }
}
