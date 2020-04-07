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

        public event Action ModalOkEvent;

        public void ModalOk()
        {
            ModalOkEvent?.Invoke();
        }
    }
}
