namespace HRM.Core.Filters
{
    public class PaginationFilter
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}