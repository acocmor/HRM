using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HRM.Models.Paginate
{
    public static class PaginateHelper
    {
        public const int DefaultPageSize = 15;
        public const int DefaultCurrentPage = 1;

        public async static Task<Paginate<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int currentPage, int pageSize)
        {                        
            currentPage = currentPage > 0 ? currentPage : DefaultCurrentPage;
            pageSize = pageSize > 0 ? pageSize : DefaultPageSize;
            var count = await source.CountAsync();
            var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Paginate<T>(items, count, currentPage, pageSize);
        }
    }
}