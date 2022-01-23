using System;
using HRM.Models.Employee;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.User
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public GetEmployeeDTO Employee { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
