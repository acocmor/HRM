using System.Collections.Generic;
using HRM.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace HRM.Entity.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Role> Roles { get; set; }
        public Employee? Employee { get; set; }
    }
}
