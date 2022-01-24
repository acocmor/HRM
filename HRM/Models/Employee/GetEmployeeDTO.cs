using System;
using HRM.Models.Address;
using HRM.Models.Department;
using HRM.Models.Gender;
using HRM.Models.Position;
using HRM.Models.User;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Employee
{
    public class GetEmployeeDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public string Avatar { get; set; }
        public virtual GetGenderDTO Gender { get; set; }
        public virtual GetUserDTO User { get; set; }
        public virtual GetAddressDTO Address { get; set; }
        public virtual GetPositionDTO Position { get; set; }
        public virtual GetDepartmentDTO Department { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public GetEmployeeDTO(){}
        
        public GetEmployeeDTO(Entity.Entities.Employee employee
            , Entity.Entities.Gender gender
            , Entity.Entities.User user
            , Entity.Entities.Address address
            , Entity.Entities.Position position
            , Entity.Entities.Department department)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            DayOfBirth = employee.DayOfBirth;
            MonthOfBirth = employee.MonthOfBirth;
            YearOfBirth = employee.DayOfBirth;
            Avatar = employee.Avatar;
            Gender ??= new GetGenderDTO(employee.Gender, null);
            Address ??= new GetAddressDTO(employee.Address, null);
            User ??= new GetUserDTO(employee.User, null);
            Position ??= new GetPositionDTO(employee.Position, null);
            Department ??= new GetDepartmentDTO(employee.Department, null);
        }
    }
}
