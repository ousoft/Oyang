using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.BlazorApp.Model
{
    public class BootstrapPagination<T>
    {
        public BootstrapPagination(Pagination pagination) : this(pagination, 5, new List<int>() { 10, 25, 50, 100 })
        {

        }

        public BootstrapPagination(Pagination pagination, int buttonCount, List<int> numberOfPages)
        {
            _pagination = pagination;
            ButtonCount = buttonCount;
            NumberOfPages = numberOfPages;
        }
        private Pagination _pagination;

        public int PageIndex { get; private set; }
        public int PageSize { get;private set; }
        public string SortField { get; private set; }
        public bool IsAscending { get; private set; }
        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public List<T> Items { get; private set; }

        public int ButtonCount { get; }
        public List<int> Buttons { get; private set; } = new List<int>();
        public List<int> NumberOfPages { get; }

        public bool HasPrevious { get => this.PageIndex > 1; }
        public bool HasNext { get => this.PageIndex < this.PageCount; }
        public bool HasFirst { get => this.PageIndex > 1; }
        public bool HasLast { get => this.PageIndex < this.PageCount; }

        public void SetPagination(Pagination<T> pagination)
        {
            this.PageIndex = pagination.PageIndex;
            this.PageSize = pagination.PageSize;
            this.SortField = pagination.SortField;
            this.IsAscending = pagination.IsAscending;
            this.TotalCount = pagination.TotalCount;
            this.PageCount = pagination.PageCount;
            this.Items = pagination.Items;
            ResetButtons();
        }
        private void ResetButtons()
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

        public event Action PaginationChange;

        public void OnPageIndexChange(int pageIndex)
        {
            _pagination.PageIndex = pageIndex;
            PaginationChange();
        }

        public void OnPageSizeChange(int pageSize)
        {
            _pagination.PageSize = pageSize;
            PaginationChange();
        }

        public void OnSortChange(string sortField)
        {
            if (_pagination.SortField == sortField)
            {
                _pagination.IsAscending = !_pagination.IsAscending;
            }
            else
            {
                _pagination.SortField = sortField;
                _pagination.IsAscending = true;
            }            
            PaginationChange();
        }

        public string GetSortIcon(string sortField)
        {
            if (_pagination.SortField == sortField)
            {
                string icon = _pagination.IsAscending ? "∧" : "∨";
                return icon;
            }
            else
            {
                return null;
            }
        }
    }
}

