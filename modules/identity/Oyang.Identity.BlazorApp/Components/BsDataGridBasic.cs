using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Oyang.Identity.BlazorApp.Components
{
    public class BsDataGridBasic : ComponentBase
    {
        [Parameter]
        public int PageIndex { get; set; }
        [Parameter]
        public int PageSize { get; set; } = 10;
        [Parameter]
        public string SortField { get; set; }
        [Parameter]
        public bool IsAscending { get; set; }
        [Parameter]
        public int TotalCount { get; set; }
        [Parameter]
        public int ButtonCount { get; set; } = 5;
        [Parameter]
        public List<int> NumberOfPages { get; set; } = new List<int>() { 10, 25, 50, 100 };

        [Parameter]
        public EventCallback<Domain.Pagination> OnPaginationChangeEvent { get; set; }

        public List<int> Buttons { get; protected set; }
        public int PageCount { get => (int)Math.Ceiling((decimal)TotalCount / PageSize); }
        public bool HasPrevious { get => this.PageIndex > 1; }
        public bool HasNext { get => this.PageIndex < this.PageCount; }
        protected void ResetButtons()
        {
            int temp1 = (ButtonCount - 1) / 2;
            int temp2 = ButtonCount - 1 - temp1;
            int startPageIndex = 1;
            int endPageIndex = 1;
            if (PageCount <= ButtonCount)
            {
                startPageIndex = 1;
                endPageIndex = PageCount;
            }
            else if (PageIndex <= temp1)
            {
                startPageIndex = 1;
                endPageIndex = startPageIndex + ButtonCount - 1;
            }
            else if (PageIndex + temp2 > PageCount)
            {
                endPageIndex = PageCount;
                startPageIndex = endPageIndex - ButtonCount + 1;
            }
            else
            {
                startPageIndex = PageIndex - temp1;
                endPageIndex = PageIndex + temp2;
            }
            Buttons = Buttons ?? new List<int>();
            Buttons.Clear();
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                Buttons.Add(i);
            }
        }

        protected List<BsDataGridColumn> _columns = new List<BsDataGridColumn>();

        public void AddColumn(BsDataGridColumn column)
        {
            if (!_columns.Exists(t => t.Name == column.Name))
            {
                _columns.Add(column);
            }
        }

        //public event Action PaginationChangeEvent;

        public void OnPaginationChange()
        {
            OnPaginationChangeEvent.InvokeAsync(new Domain.Pagination()
            {
                PageIndex = this.PageIndex,
                PageSize = this.PageSize,
                SortField = this.SortField,
                IsAscending = this.IsAscending,
            });
        }
        public void OnPageIndexChange(int pageIndex)
        {
            this.PageIndex = pageIndex;
            OnPaginationChange();
        }
        public void OnPageSizeChange(int pageSize)
        {
            this.PageSize = pageSize;
            OnPaginationChange();
        }
        public void OnSortChange(string sortField)
        {
            if (!string.IsNullOrWhiteSpace(this.SortField))
            {
                _columns.Find(t => t.Name == this.SortField).SortFieldIcon = null;
            }
            if (this.SortField == sortField)
            {
                this.IsAscending = !this.IsAscending;
            }
            else
            {
                this.SortField = sortField;
                this.IsAscending = true;
            }
            _columns.Find(t => t.Name == sortField).SortFieldIcon = this.IsAscending ? "∧" : "∨";
            OnPaginationChange();
        }

    }

}
