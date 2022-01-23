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
        public virtual GetPositionDTO Position { get; set; }
        public virtual GetDepartmentDTO Department { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
