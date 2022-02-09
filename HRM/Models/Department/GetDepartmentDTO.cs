using System;
using System.Collections.Generic;
using HRM.Models.Employee;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Department
{
    public class GetDepartmentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetEmployeeDTO> Employees { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
        
        public GetDepartmentDTO(){}
        public GetDepartmentDTO(Entity.Entities.Department department, ICollection<Entity.Entities.Employee> list)
        {
            if (department == null)
            {
                return;
            }
            Id = department.Id;
            Name = department.Name;
            if (list != null)
            {
                foreach (var item in list)
                {
                    Employees.Add(new GetEmployeeDTO(item, item?.Gender, item?.User, item?.Address, item?.Position, null));
                }
            }
        }
    }
}
