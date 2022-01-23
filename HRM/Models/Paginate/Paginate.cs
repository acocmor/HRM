using System;
using System.Collections.Generic;

namespace HRM.Models.Paginate
{
    public class Paginate<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public List<T> Result { get; set; } = new List<T>();

        public Paginate(List<T> items, int count, int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;
            Result.AddRange(items);
        }
    }
}