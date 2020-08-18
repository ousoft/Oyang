using System;
using System.Collections.Generic;
using System.Text;

namespace Oyang.Blazor.Components.DataTable
{
    public class PaginationEventArgs
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public bool IsAscending { get; set; }
    }
}
