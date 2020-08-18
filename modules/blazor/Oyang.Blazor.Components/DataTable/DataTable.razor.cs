using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oyang.Blazor.Components.DataTable
{
    public partial class DataTable<TItem> : DataTableBase, IDisposable
    {
        public DataTable()
        {
            this.PageIndex = 1;
            this.PageSize = 10;
            this.SortField = "Id";
            this.IsAscending = true;
            this.totalCount = 0;
            this.ButtonCount = 5;
            this.NumberOfPages = new List<int>() { 10, 20, 25, 50, 100 };
            this.ShowRowSelector = true;
            this.ShowRowIndex = true;

            this._buttons = new List<int>();
        }


        #region Component参数和事件

        [Parameter]
        public int PageIndex { get; set; }
        [Parameter]
        public int PageSize { get; set; }
        [Parameter]
        public string SortField { get; set; }
        [Parameter]
        public bool IsAscending { get; set; }
        //[Parameter]
        //public int TotalCount { get; set; }

        private int totalCount;
        [Parameter]
        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;
                ResetButtons();
            }
        }

        [Parameter]
        public int ButtonCount { get; set; }
        [Parameter]
        public List<int> NumberOfPages { get; set; }
        [Parameter]
        public bool ShowRowSelector { get; set; }
        [Parameter]
        public bool ShowRowIndex { get; set; }

        [Parameter]
        public List<TItem> Items { get; set; }
        [Parameter]
        public List<TItem> SelectedItems { get; set; }

        [Parameter]
        public RenderFragment HeaderTemplate { get; set; }
        [Parameter]
        public RenderFragment<TItem> RowTemplate { get; set; }

        [Parameter]
        public EventCallback<PaginationEventArgs> OnPaginationChanged { get; set; }
        [Parameter]
        public EventCallback OnDataTableInitialized { get; set; }



        [Parameter]
        public EventCallback<int> PageIndexChanged { get; set; }
        [Parameter]
        public EventCallback<int> PageSizeChanged { get; set; }
        [Parameter]
        public EventCallback<string> SortFieldChanged { get; set; }
        [Parameter]
        public EventCallback<bool> IsAscendingChanged { get; set; }
        [Parameter]
        public EventCallback<int> TotalCountChanged { get; set; }
        [Parameter]
        public EventCallback<int> ButtonCountChanged { get; set; }
        [Parameter]
        public EventCallback<int> NumberOfPagesChanged { get; set; }
        [Parameter]
        public EventCallback<int> SelectedColumnTypeChanged { get; set; }
        [Parameter]
        public EventCallback<List<TItem>> SelectedItemsChanged { get; set; }

        #endregion

        #region Component生命周期


        //public override Task SetParametersAsync(ParameterView parameters)
        //{
        //    return Task.CompletedTask;
        //    //return base.SetParametersAsync(parameters);
        //}

        protected override void OnInitialized()
        {
            if (string.IsNullOrWhiteSpace(_defaultSortField))
            {
                _defaultSortField = this.SortField;
                _defaultIsAscending = this.IsAscending;
            }
            SortChangedEvent += HandleSortChanged;
        }

        public void Dispose()
        {
            SortChangedEvent -= HandleSortChanged;
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                PaginationChanged();
            }
            return base.OnAfterRenderAsync(firstRender);
        }

        #endregion

        #region 字段、属性、事件

        private int PageCount { get => (int)Math.Ceiling((decimal)this.TotalCount / this.PageSize); }
        private bool HasPrevious { get => PageIndex > 1; }
        private bool HasNext { get => this.PageIndex < this.PageCount; }
        private bool HasFirst { get => this.PageIndex > 1; }
        private bool HasLast { get => this.PageIndex < this.PageCount; }

        private bool isSelectedAll;
        private bool IsSelectedAll
        {
            get { return isSelectedAll; }
            set { SetSelectedAll(value); }
        }

        private List<int> _buttons;
        private string _defaultSortField;
        private bool _defaultIsAscending;

        #endregion

        #region 方法

        private void PaginationChanged()
        {
            var eventArgs = new PaginationEventArgs()
            {
                PageIndex = this.PageIndex,
                PageSize = this.PageSize,
                SortField = this.SortField,
                IsAscending = this.IsAscending,
            };
            OnPaginationChanged.InvokeAsync(eventArgs);
            SelectedItems.Clear();
            SetSelectedAll(false);
        }

        private void HandlePageIndexChanged(int pageIndex)
        {
            this.PageIndex = pageIndex;
            this.PageIndexChanged.InvokeAsync(PageIndex);
            PaginationChanged();
        }

        private void HandlePageSizeChanged(int pageSize)
        {
            this.PageSize = pageSize;
            this.PageSizeChanged.InvokeAsync(PageSize);
            PaginationChanged();
        }

        private void ResetButtons()
        {
            int temp1 = (ButtonCount - 1) / 2;
            int temp2 = ButtonCount - 1 - temp1;
            int startPageIndex;
            int endPageIndex;
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
            _buttons.Clear();
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                _buttons.Add(i);
            }
        }

        private async Task HandlePaginationChanged()
        {
            await InvokeAsync(() =>
            {
                PaginationChanged();
            });
        }

        private async Task HandleSortChanged(string sortField, bool? isAscending)
        {
            await InvokeAsync(() =>
            {
                if (isAscending.HasValue)
                {
                    this.SortField = sortField;
                    this.IsAscending = isAscending.Value;
                }
                else
                {
                    this.SortField = _defaultSortField;
                    this.IsAscending = _defaultIsAscending;
                }
                this.SortFieldChanged.InvokeAsync(SortField);
                this.IsAscendingChanged.InvokeAsync(IsAscending);
                PaginationChanged();
            });
        }



        private void HandleSelectedItemChanged(DataTableSelectedItem selectedItem)
        {
            if (selectedItem.IsSelected)
            {
                if (!SelectedItems.Contains((TItem)selectedItem.Value))
                {
                    SelectedItems.Add((TItem)selectedItem.Value);
                }
                if (SelectedItems.Count == Items.Count)
                {
                    isSelectedAll = true;
                }
            }
            else
            {
                SelectedItems.Remove((TItem)selectedItem.Value);
                isSelectedAll = false;
            }
        }




        private string CustomPaginationText()
        {
            string format = "第 {StartNumber} - {EndNumber} 条，共 {PageCount} 页 {TotalCount} 条数据";
            var startNumber = (this.PageIndex - 1) * this.PageSize + 1;
            var endNumber = startNumber + this.Items?.Count - 1;
            format = format.Replace("{StartNumber}", startNumber.ToString());
            format = format.Replace("{EndNumber}", endNumber.ToString());
            format = format.Replace("{PageCount}", PageCount.ToString());
            format = format.Replace("{TotalCount}", TotalCount.ToString());
            return format;
        }


        private void SetSelectedAll(bool isSelected)
        {
            isSelectedAll = isSelected;
            NotifySelectedAllChangedEvent(isSelected).Wait();
        }

        #endregion


    }
}
