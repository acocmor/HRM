using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Interfaces;
using HRM.Models.Gender;
using HRM.Entity.Constracts;
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

        public Task<List<GetGenderDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GetGenderDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetGenderDTO> Create(CreateGenderDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, UpdateGenderDTO request)
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
                _genderRepository.Dispose();
            }
        }
    }
}
