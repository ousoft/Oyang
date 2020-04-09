using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.BlazorApp.Components
{
    public class OyDataGridHeaderItem
    {
        public OyDataGridHeaderItem(string displayName, string columnName) : this(displayName, columnName, false)
        {

        }
        public OyDataGridHeaderItem(string displayName, string columnName, bool allowSort)
        {
            this.DisplayName = displayName;
            this.ColumnName = columnName;
            this.AllowSort = allowSort;
        }
        public string DisplayName { get; set; }
        public string ColumnName { get; set; }
        public bool AllowSort { get; set; }
    }
}
