using System;
using HRM.Models.Address;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Employee
{
    public class UpdateEmployeeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public string Avatar { get; set; }
        public Guid GenderId { get; set; }
        public UpdateAddressDTO Address { get; set; }
        public Guid? PositionId { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
