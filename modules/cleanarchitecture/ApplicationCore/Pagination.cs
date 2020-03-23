using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore
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
        public Pagination(Pagination pagination, List<T> list,int totalCount)
        {
            this.PageIndex = pagination.PageIndex;
            this.PageSize = pagination.PageSize;
            this.SortField = pagination.SortField;
            this.IsAscending = pagination.IsAscending;
            this.Items = list;
            this.TotalCount = totalCount;
        }

        public int TotalCount { get; }
        public int PageCount { get => (int)Math.Ceiling((decimal)TotalCount / PageSize); }
        public List<T> Items { get; }
    }
}
