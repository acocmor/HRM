using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Interfaces;
using HRM.Models.Employee;
using HRM.Entity.Constracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.AppServices
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeManager(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public Task<List<GetEmployeeDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GetEmployeeDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetEmployeeDTO> Create(CreateEmployeeDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<GetEmployeeDTO> Update(Guid id, UpdateEmployeeDTO request)
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
                _employeeRepository.Dispose();
            }
        }
    }
}
