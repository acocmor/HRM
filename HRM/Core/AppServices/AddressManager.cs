using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Exceptions;
using HRM.Core.Filters;
using HRM.Core.Interfaces;
using HRM.Models.Address;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Models.Paginate;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Paginate<GetAddressDTO>> GetAll(GetAddressFilter request)
        {
            request ??= new GetAddressFilter();
            
            var positions = _addressRepository.GetAll();
     
            if (!string.IsNullOrEmpty(request.City))
            {
                positions.Where(x => EF.Functions.Like(x.City, $"%{request.City}%")).Load();
            }
            
            if (!string.IsNullOrEmpty(request.District))
            {
                positions.Where(x => EF.Functions.Like(x.District, $"%{request.District}%")).Load();
            }
            
            if (!string.IsNullOrEmpty(request.SubDistrict))
            {
                positions.Where(x => EF.Functions.Like(x.SubDistrict, $"%{request.SubDistrict}%")).Load();
            }

            var result = _mapper.ProjectTo<GetAddressDTO>(positions.OrderBy(x => x.Id));
            return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }

        public async Task<GetAddressDTO> GetById(Guid id)
        {
            return _mapper.Map<GetAddressDTO>(await _addressRepository.GetById(id));
        }

        public async Task<GetAddressDTO> Create(CreateAddressDTO request)
        {
            try
            {
                var newAddress = _mapper.Map<Address>(request);
                newAddress.CreatedAt = DateTime.Now;
                newAddress.UpdatedAt = DateTime.Now;
            
                var address = _addressRepository.Create(newAddress);
                await _addressRepository.SaveChangesAsync();
                return _mapper.Map<GetAddressDTO>(address);
            }
            catch (Exception e)
            {
                throw new ApiException($"{e.Message}") {StatusCode = (int) HttpStatusCode.InternalServerError};
            }
        }
        
        public async Task<bool> Update(Guid id, UpdateAddressDTO request)
        {
            var original = await _addressRepository.GetById(id);
            if (original == null) throw new ApiException("Phòng ban này không tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            original.Detail ??= request.Detail;
            original.SubDistrict ??= request.SubDistrict;
            original.District ??= request.District;
            original.City ??= request.City;
            original.UpdatedAt = DateTime.Now;

            _addressRepository.Update(original);
            await _addressRepository.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> Delete(Guid id)
        {
            await _addressRepository.Delete(id);
            return await _addressRepository.SaveChangesAsync() > 0;
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
