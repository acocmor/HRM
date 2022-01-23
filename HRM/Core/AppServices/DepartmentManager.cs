using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HRM.Core.Interfaces;
using HRM.Models.Department;
using HRM.Entity.Constracts;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Core.AppServices
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentManager(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public Task<List<GetDepartmentDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<GetDepartmentDTO> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GetDepartmentDTO> Create(CreateDepartmentDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Guid id, UpdateDepartmentDTO request)
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
                _departmentRepository.Dispose();
            }
        }
    }
}
