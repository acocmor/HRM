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
        
        public GetPositionDTO(){}
        
        public GetPositionDTO(Entity.Entities.Position position, ICollection<Entity.Entities.Employee> list)
        {
            Id = position.Id;
            Name = position.Name;
            if (list != null)
            {
                foreach (var item in list)
                {
                    Employees.Add(new GetEmployeeDTO(item, item.Gender, item.User, item.Address, null, item.Department));
                }
            }
        }
    }
}
