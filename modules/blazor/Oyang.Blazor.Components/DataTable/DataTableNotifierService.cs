using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Oyang.Blazor.Components.DataTable
{
    public class DataTableNotifierService
    {
        public event Func<Task> PaginationChangedEvent;

        public async Task NotifyPaginationChanged()
        {
            await PaginationChangedEvent?.Invoke();
        }

    }
}
