using Oyang.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.BlazorApp.Model
{
    public class BootstrapPagination<T> : Pagination<T>
    {
        public BootstrapPagination(Pagination<T> pagination) : this(pagination, 5)
        {

        }

        public BootstrapPagination(Pagination<T> pagination, int buttonCount) : base(pagination, pagination.TotalCount, pagination.Items)
        {
            _buttonCount = buttonCount;
        }

        private int _buttonCount = 5; 

        List<int> _buttons = null;
        public List<int> Buttons
        {
            get
            {
                if (_buttons == null)
                {
                    _buttons = GetPageButton();
                }
                return _buttons;
            }
        }

        private List<int> GetPageButton()
        {
            int temp1 = (_buttonCount - 1) / 2;
            int temp2 = _buttonCount - 1 - temp1;
            int startPageIndex = 1;
            int endPageIndex = 1;
            if (PageCount <= _buttonCount)
            {
                startPageIndex = 1;
                endPageIndex = PageCount;
            }
            else if (PageIndex <= temp1)
            {
                startPageIndex = 1;
                endPageIndex = startPageIndex + _buttonCount - 1;
            }
            else if (PageIndex + temp2 > PageCount)
            {
                endPageIndex = PageCount;
                startPageIndex = endPageIndex - _buttonCount + 1;
            }
            else
            {
                startPageIndex = PageIndex - temp1;
                endPageIndex = PageIndex + temp2;
            }
            var buttons = new List<int>();
            for (int i = startPageIndex; i <= endPageIndex; i++)
            {
                buttons.Add(i);
            }
            return buttons;
        }
    }
}

