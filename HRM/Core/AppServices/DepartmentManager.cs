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
using HRM.Models.Department;
using HRM.Entity.Constracts;
using HRM.Entity.Entities;
using HRM.Models.Paginate;
using HRM.Models.Position;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Paginate<GetDepartmentDTO>> GetAll(GetDepartmentsFilter request)
        {
            request ??= new GetDepartmentsFilter();
            
            var positions = _departmentRepository.GetAll();
     
            if (!string.IsNullOrEmpty(request.Name))
            {
                positions.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%")).Load();
            }

            var result = _mapper.ProjectTo<GetDepartmentDTO>(positions.OrderBy(x => x.Id));
            return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        }

        public async Task<GetDepartmentDTO> GetById(Guid id)
        {
            return _mapper.Map<GetDepartmentDTO>(await _departmentRepository.GetById(id));
        }

        public async Task<GetDepartmentDTO> Create(CreateDepartmentDTO request)
        {
            var name = await _departmentRepository.GetByName(request.Name);
            if (name != null)
            {
                throw new ApiException("Chức vụ này đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            var newDepartment = _mapper.Map<Department>(request);
            newDepartment.CreatedAt = DateTime.Now;
            newDepartment.UpdatedAt = DateTime.Now;
            var department = _departmentRepository.Create(newDepartment);;

            await _departmentRepository.SaveChangesAsync(); 
            return _mapper.Map<GetDepartmentDTO>(department);
        }

        public async Task<bool> Update(Guid id, UpdateDepartmentDTO request)
        {
            var original = await _departmentRepository.GetById(id);
            if (original == null) throw new ApiException("Chức vụ này không tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            var name = await _departmentRepository.GetByName(request.Name);
            if (name != null) throw new ApiException("Chức vụ này đã tồn tại.") { StatusCode = (int)HttpStatusCode.BadRequest };

            original.Name = request.Name;
            original.UpdatedAt = DateTime.Now;

            _departmentRepository.Update(original);
            await _departmentRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _departmentRepository.Delete(id);
            return await _departmentRepository.SaveChangesAsync() > 0;
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
