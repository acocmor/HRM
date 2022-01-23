using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Entity.Common;

namespace HRM.Entity.Entities
{
    public class Department : EntityBase
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
