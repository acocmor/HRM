using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Address
{
    public class CreateAddressDTO
    {
        public Guid Id { get; set; }
        public string Detail { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
