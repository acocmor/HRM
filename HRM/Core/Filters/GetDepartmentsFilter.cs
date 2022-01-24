using System;

namespace HRM.Core.Filters
{
    public class GetDepartmentsFilter : PaginationFilter
    {
        public string Name { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}