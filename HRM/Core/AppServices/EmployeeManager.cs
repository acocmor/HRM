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
using HRM.Models.Employee;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Models.Address;
using HRM.Models.Paginate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.AppServices
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public EmployeeManager(IEmployeeRepository employeeRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Paginate<GetEmployeeDTO>> GetAll(GetEmployeeFilter request)
        {
            request ??= new GetEmployeeFilter();
            
            var employees = _employeeRepository.GetAll();
     
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                employees.Where(x => EF.Functions.Like(x.FirstName, $"%{request.FirstName}%")).Load();
            }
            
            if (!string.IsNullOrEmpty(request.LastName))
            {
                employees.Where(x => EF.Functions.Like(x.LastName, $"%{request.LastName}%")).Load();
            }
            
            if (request.DayOfBirth != null)
            {
                employees.Where(x => x.DayOfBirth == request.DayOfBirth).Load();
            }
            
            if (request.MonthOfBirth != null)
            {
                employees.Where(x => x.MonthOfBirth == request.MonthOfBirth).Load();
            }
            
            if (request.YearOfBirth != null)
            {
                employees.Where(x => x.YearOfBirth == request.YearOfBirth).Load();
            }
            
            if (request.PositionId != null)
            {
                employees.Where(x => x.PositionId.Equals(request.PositionId)).Load();
            }
            
            if (request.GenderId != null)
            {
                employees.Where(x => x.GenderId.Equals(request.GenderId)).Load();
            }
            
            if (request.DepartmentId != null)
            {
                employees.Where(x => x.DepartmentId.Equals(request.DepartmentId)).Load();
            }
            
            var list = employees.OrderBy(u => u.Id).Select(u => new GetEmployeeDTO(u, u.Gender, u.User, u.Address, u.Position, u.Department)).AsQueryable();
            return await list.ToPaginatedListAsync(request.CurrentPage, request.PageSize);

            // var result = _mapper.ProjectTo<GetEmployeeDTO>(positions.OrderBy(x => x.Id));
            // return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }

        public async Task<GetEmployeeDTO> GetById(Guid id)
        {
            return _mapper.Map<GetEmployeeDTO>(await _employeeRepository.GetById(id));
        }

        public async Task<GetEmployeeDTO> Create(CreateEmployeeDTO request)
        {
            try
            {
                var newEmployee = _mapper.Map<Employee>(request);
                newEmployee.CreatedAt = DateTime.Now;
                newEmployee.UpdatedAt = DateTime.Now;
            
                var employee = _employeeRepository.Create(newEmployee);
                await _employeeRepository.SaveChangesAsync();
                return _mapper.Map<GetEmployeeDTO>(employee);
            }
            catch (Exception e)
            {
                throw new ApiException($"{e.Message}") {StatusCode = (int) HttpStatusCode.InternalServerError};
            }
        }
        
        public async Task<bool> Update(Guid id, UpdateEmployeeDTO request)
        {
            var original = await _employeeRepository.GetById(id);
            if (original == null) throw new ApiException("Nhân viên này không tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            original.FirstName ??= request.FirstName;
            original.LastName ??= request.LastName;
            original.DayOfBirth = request.DayOfBirth;
            original.MonthOfBirth = request.MonthOfBirth;
            original.YearOfBirth = request.YearOfBirth;
            original.GenderId ??= request.GenderId;
            original.PositionId ??= request.PositionId;
            original.DepartmentId ??= request.DepartmentId;
            original.UpdatedAt = DateTime.Now;
            
            var address = await _addressRepository.GetById(original.Address.Id);
            if (address == null)
            {
                _addressRepository.Create(_mapper.Map<Address>(request.Address));
            }
            else
            {
                address.Detail = request.Address.Detail;
                address.SubDistrict = request.Address.SubDistrict;
                address.District = request.Address.District;
                address.City = request.Address.City;
                _addressRepository.Update(address);
                await _addressRepository.SaveChangesAsync();
            }
            
            _employeeRepository.Update(original);
            await _employeeRepository.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> Delete(Guid id)
        {
            await _employeeRepository.Delete(id);
            return await _employeeRepository.SaveChangesAsync() > 0;
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
                _employeeRepository.Dispose();
            }
        }
    }
}
