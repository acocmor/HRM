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
        public GetGenderDTO Gender { get; set; }
        public GetUserDTO User { get; set; }
        public GetAddressDTO Address { get; set; }
        public GetPositionDTO Position { get; set; }
        public GetDepartmentDTO Department { get; set; }
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
            if (employee == null) return;
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            DayOfBirth = employee.DayOfBirth;
            MonthOfBirth = employee.MonthOfBirth;
            YearOfBirth = employee.DayOfBirth;
            Avatar = employee.Avatar;
            if(gender != null)
                Gender = new GetGenderDTO(gender, null);
            if(address != null)
                Address = new GetAddressDTO(address, null);
            if(user != null)
                User = new GetUserDTO(user, null);
            if(position != null)
                Position = new GetPositionDTO(position, null);
            if(department != null)
                Department = new GetDepartmentDTO(department, null);
        }
    }
}
