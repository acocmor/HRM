using System;
using HRM.Core.Filters;
using HRM.Models.Employee;

namespace HRM.Core.Filters
{
    public class GetUsersFilter : PaginationFilter
    {
        public string Email { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}