using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oyang.Blazor.Components.DataTable
{
    public class DataTableBase : ComponentBase
    {
        public event Func<string, bool?, Task> SortChangedEvent;
        public event Func<bool, Task> SelectedAllChangedEvent;

        public async Task NotifySortChanged(string sortField, bool? isAscending)
        {
            await SortChangedEvent?.Invoke(sortField, isAscending);
        }

        public async Task NotifySelectedAllChangedEvent(bool isSelected)
        {
            if (SelectedAllChangedEvent != null)
            {
                await SelectedAllChangedEvent?.Invoke(isSelected);
            }
        }
    }
}
