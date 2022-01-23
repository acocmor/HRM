using System;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRM.Models.Gender
{
    public class GetGenderNoList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}