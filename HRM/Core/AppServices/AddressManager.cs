using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Interfaces;
using HRM.Models.Address;
using HRM.Entity.Constracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.AppServices
{
    public class AddressManager : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressManager(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public Task<List<GetAddressDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GetAddressDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetAddressDTO> Create(CreateAddressDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, UpdateAddressDTO request)
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
                _addressRepository.Dispose();
            }
        }
    }
}
