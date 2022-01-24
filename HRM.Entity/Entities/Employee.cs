using System;
using HRM.Entity.Common;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Entity.Entities
{
    public class Employee : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public string? Avatar { get; set; }
        public Guid? GenderId { get; set; }
        public Gender Gender { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Address? Address { get; set; }
        public Guid? PositionId { get; set; }
        public Position Position { get; set; }
        public Guid? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
