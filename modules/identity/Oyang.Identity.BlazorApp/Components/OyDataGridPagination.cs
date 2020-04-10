using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.BlazorApp.Components
{
    public class OyDataGridPagination
    {
        public OyDataGridPagination(Pagination pagination, int buttonCount, List<int> numberOfPages)
            : this(pagination.PageIndex, pagination.PageSize, pagination.SortField, pagination.IsAscending, buttonCount, numberOfPages)
        {
            PaginationChangeEvent += () =>
            {
                pagination.PageIndex = this.PageIndex;
                pagination.PageSize = this.PageSize;
                pagination.SortField = this.SortField;
                pagination.IsAscending = this.IsAscending;
            };
        }

        public OyDataGridPagination(int pageIndex, int pageSize, string sortField, bool isAscending, int buttonCount, List<int> numberOfPages)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.SortField = sortField;
            this.IsAscending = isAscending;
            this.ButtonCount = buttonCount;
            this.NumberOfPages = numberOfPages;
        }

        public int PageIndex { get; protected set; }
        public int PageSize { get; protected set; }
        public string SortField { get; protected set; }
        public bool IsAscending { get; protected set; }
        public int TotalCount { get; protected set; }
        public int PageCount { get; protected set; }

        public int ButtonCount { get; }
        public List<int> Buttons { get; protected set; } = new List<int>();
        public List<int> NumberOfPages { get; }

        public bool HasPrevious { get => this.PageIndex > 1; }
        public bool HasNext { get => this.PageIndex < this.PageCount; }
        public bool HasFirst { get => this.PageIndex > 1; }
        public bool HasLast { get => this.PageIndex < this.PageCount; }

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
            Buttons.Clear();
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                Buttons.Add(i);
            }
        }

        public bool IsLoading { get; protected set; }

        public event Action PaginationViewChangeEvent;
        public event Action PaginationChangeEvent;
        public void OnPaginationChange()
        {
            this.IsLoading = true;
            PaginationViewChangeEvent?.Invoke();
            PaginationChangeEvent?.Invoke();
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
            if (this.SortField == sortField)
            {
                this.IsAscending = !this.IsAscending;
            }
            else
            {
                this.SortField = sortField;
                this.IsAscending = true;
            }
            OnPaginationChange();
        }
    }

    public class OyDataGridPagination<T> : OyDataGridPagination
    {
        public OyDataGridPagination(Pagination pagination)
            : base(pagination, 5, new List<int>() { 10, 25, 50, 100 })
        {

        }

        public OyDataGridPagination(Pagination pagination, int buttonCount, List<int> numberOfPages)
            : base(pagination, buttonCount, numberOfPages)
        {

        }


        public OyDataGridPagination(int pageIndex, int pageSize, string sortField, bool isAscending)
            : this(pageIndex, pageSize, sortField, isAscending, 5, new List<int>() { 10, 25, 50, 100 })
        {

        }

        public OyDataGridPagination(int pageIndex, int pageSize, string sortField, bool isAscending, int buttonCount, List<int> numberOfPages)
            : base(pageIndex, pageSize, sortField, isAscending, buttonCount, numberOfPages)
        {

        }

        public IReadOnlyList<T> Items { get; private set; }

        public void BindPagination(Pagination<T> pagination)
        {
            this.PageIndex = pagination.PageIndex;
            this.PageSize = pagination.PageSize;
            this.SortField = pagination.SortField;
            this.IsAscending = pagination.IsAscending;
            this.TotalCount = pagination.TotalCount;
            this.PageCount = pagination.PageCount;
            this.Items = pagination.Items;
            ResetButtons();
            this.IsLoading = false;
        }


        public string ShowCustomText(string format)
        {
            //format = "第 {StartNumber} - {EndNumber} 条，共 {TotalCount} 条"
            var startNumber = (this.PageIndex - 1) * this.PageSize + 1;
            var endNumber = startNumber + this.Items.Count - 1;
            format = format.Replace("{StartNumber}", startNumber.ToString());
            format = format.Replace("{EndNumber}", endNumber.ToString());
            format = format.Replace("{PageCount}", PageCount.ToString());
            format = format.Replace("{TotalCount}", TotalCount.ToString());
            return format;
        }
    }

}

