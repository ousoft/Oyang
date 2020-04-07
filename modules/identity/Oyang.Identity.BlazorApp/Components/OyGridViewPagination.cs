using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.BlazorApp.Components
{
    public class OyGridViewPagination<T>
    {
        public OyGridViewPagination(Pagination pagination) : this(pagination, 5, new List<int>() { 10, 25, 50, 100 })
        {

        }

        public OyGridViewPagination(Pagination pagination, int buttonCount, List<int> numberOfPages)
        {
            Pagination = pagination;
            ButtonCount = buttonCount;
            NumberOfPages = numberOfPages;
        }
        public Pagination Pagination { get; }

        public int PageIndex { get; private set; }
        public int PageSize { get;private set; }
        public string SortField { get; private set; }
        public bool IsAscending { get; private set; }
        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public IReadOnlyList<T> Items { get; private set; }

        public int ButtonCount { get; }
        public List<int> Buttons { get; private set; } = new List<int>();
        public List<int> NumberOfPages { get; }

        public bool HasPrevious { get => this.PageIndex > 1; }
        public bool HasNext { get => this.PageIndex < this.PageCount; }
        public bool HasFirst { get => this.PageIndex > 1; }
        public bool HasLast { get => this.PageIndex < this.PageCount; }

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

        public event Action PaginationChangeEvent;

        public void OnPageIndexChange(int pageIndex)
        {
            Pagination.PageIndex = pageIndex;
            PaginationChangeEvent();
        }

        public void OnPageSizeChange(int pageSize)
        {
            Pagination.PageSize = pageSize;
            PaginationChangeEvent();
        }

        public void OnSortChange(string sortField)
        {
            if (Pagination.SortField == sortField)
            {
                Pagination.IsAscending = !Pagination.IsAscending;
            }
            else
            {
                Pagination.SortField = sortField;
                Pagination.IsAscending = true;
            }            
            PaginationChangeEvent();
        }

        public string GetSortIcon(string sortField)
        {
            if (Pagination.SortField == sortField)
            {
                string icon = Pagination.IsAscending ? "∧" : "∨";
                return icon;
            }
            else
            {
                return null;
            }
        }
    }
}

