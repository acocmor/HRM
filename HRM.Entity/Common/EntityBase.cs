using System;
using System.ComponentModel.DataAnnotations;

namespace HRM.Entity.Common
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
