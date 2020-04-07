using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.JSInterop
{
    public static class JSRuntimeExtensions
    {
        public static void ModalShow(this IJSRuntime jsRuntime, string modalId)
        {
            jsRuntime.InvokeVoidAsync("site.modal", modalId, new { backdrop = "static", show = true });
        }
        public static void ModalHide(this IJSRuntime jsRuntime, string modalId)
        {
            jsRuntime.InvokeVoidAsync("site.modal", modalId, "hide");
        }
    }
}
