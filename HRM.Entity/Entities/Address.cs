using System;
using HRM.Entity.Common;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Entity.Entities
{
    public class Address : EntityBase
    {
        public string Detail { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string City { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
