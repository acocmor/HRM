using System;
using System.Collections.Generic;
using FluentValidation.Validators;
using HRM.Models.Employee;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Gender
{
    public class GetGenderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetEmployeeDTO> Employees { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
        
        public GetGenderDTO(){}
        
        public GetGenderDTO(Entity.Entities.Gender gender, ICollection<Entity.Entities.Employee> list)
        {
            if (gender == null) return;
                Id = gender.Id;
            Name = gender.Name;
            if (list != null)
            {
                foreach (var item in list)
                {
                    Employees.Add(new GetEmployeeDTO(item, null, item?.User, item?.Address, item?.Position, item?.Department));
                }
            }
        }
    }
}
