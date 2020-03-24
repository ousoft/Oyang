using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Identity.Domain
{
    public class Sort
    {
        public bool IsAscending { get; set; }
        public string SortField { get; set; }
    }
    public class Pagination
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public bool IsAscending { get; set; }

        public List<Sort> SecondarySort { get; set; }
    }

    public class Pagination<T> : Pagination
    {
        public Pagination()
        {

        }
        public Pagination(Pagination pagination, int totalCount, List<T> list)
        {
            this.PageIndex = pagination.PageIndex;
            this.PageSize = pagination.PageSize;
            this.SortField = pagination.SortField;
            this.IsAscending = pagination.IsAscending;
            this.SecondarySort = pagination.SecondarySort;
            this.TotalCount = totalCount;
            this.Items = list;
        }

        public int TotalCount { get; }
        public int PageCount { get => (int)Math.Ceiling((decimal)TotalCount / PageSize); }
        public List<T> Items { get; }
    }
}
