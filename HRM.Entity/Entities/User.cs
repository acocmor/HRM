using HRM.Entity.Common;
using Microsoft.EntityFrameworkCore;

namespace HRM.Entity.Entities
{
    [Index(nameof(Email))]
    public class User : EntityBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Employee Employee { get; set; }
    }
}
