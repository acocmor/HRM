using System;
using System.Collections.Generic;
using HRM.Models.Employee;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Position
{
    public class GetPositionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetEmployeeDTO> Employees { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
