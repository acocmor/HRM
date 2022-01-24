using System;

namespace HRM.Core.Filters
{
    public class GetPositionFilter : PaginationFilter
    {
        public string Name { get; set; }
        public virtual DateTime? CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
    }
}