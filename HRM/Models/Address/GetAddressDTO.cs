using System;
using HRM.Models.Employee;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Address
{
    public class GetAddressDTO
    {
        public Guid Id { get; set; }
        public string Detail { get; set; }
        public string SubDistrict { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public virtual GetEmployeeDTO Employee { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        
        public GetAddressDTO(){}
        
        public GetAddressDTO(Entity.Entities.Address address, Entity.Entities.Employee employee)
        {
            Id = address.Id;
            Detail = address.Detail;
            SubDistrict = address.SubDistrict;
            District = address.District;
            City = address.City;
            Employee ??= new GetEmployeeDTO(employee, employee.Gender, employee.User, null, employee.Position, employee.Department);
            CreatedAt = address.CreatedAt;
            UpdatedAt = address.UpdatedAt;
        }
    }
}
