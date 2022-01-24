﻿using System;
using HRM.Models.Employee;

namespace HRM.Models.User
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public virtual GetEmployeeDTO Employee { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public GetUserDTO(){}
        
        public GetUserDTO(Entity.Entities.User user, Entity.Entities.Employee employee)
        {
            Id = user.Id;
            Email = user.Email;
            Employee ??= new GetEmployeeDTO(employee, employee.Gender, null, employee.Address, employee.Position, employee.Department);
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
        }
    }
}
