using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oyang.Identity.BlazorApp.Service
{
    public class NotifierService
    {
        public event Action ModalCancelEvent;

        public void ModalCancel()
        {
            ModalCancelEvent?.Invoke();
        }

        public event Func<bool> ModalOkEvent;

        public bool ModalOk()
        {
             return ModalOkEvent?.Invoke() ?? true;
        }

        public event Action PaginationChangeEvent;

        public void OnPaginationChange()
        {
            PaginationChangeEvent?.Invoke();
        }
    }
}
