using System.Collections.Generic;
using HRM.Entity.Common;

namespace HRM.Entity.Entities
{
    public class Role : EntityBase
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}