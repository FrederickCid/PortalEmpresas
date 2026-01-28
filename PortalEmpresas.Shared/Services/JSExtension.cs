using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;


namespace PortalAG_V2.Services
{
    public static class JSExtension
    {
        #region Método de exportación a PDF

   
        #endregion

        #region Alertas

        public static async Task<object> MensajeAlertaRefrescar(this IJSRuntime js)
        {
            return await js.InvokeAsync<object>("MensajeAlertaRefrescar");
        }

        public static async Task<object> MensajeAlertaSimple(this IJSRuntime js, string titulo, string texto, string icono)
        {
            return await js.InvokeAsync<object>("MensajeAlertaSimple", titulo, texto, icono);
        }

        public static async Task<object> MensajeAlertaError(this IJSRuntime js, string titulo, string texto, string icono)
        {
            return await js.InvokeAsync<object>("MensajeAlertaError", titulo, texto, icono);
        }

        public static async Task<object> MensajeConfirmacion(this IJSRuntime js, string position, string icon, string title)
        {
            return await js.InvokeAsync<object>("MensajeConfirmacion", position, icon, title);
        }

        public static async Task<bool> MensajeVadidacion(this IJSRuntime js, string titulo, string texto, string icono)
        {
            return await js.InvokeAsync<bool>("MensajeVadidacion", titulo, texto, icono);
        }

        public static async Task<bool> MensajeAlertaValidacion(this IJSRuntime js, string title, string text, string icon)
        {
            return await js.InvokeAsync<bool>("MensajeAlertaValidacion", title, text, icon);
        }

        public static async Task<bool> MensajeAlertaEditar(this IJSRuntime js, string title, string text, string icon)
        {
            return await js.InvokeAsync<bool>("MensajeAlertaEditar", title, text, icon);
        }

        public static async Task<object> MensajeAlertaToast(this IJSRuntime js)
        {
            return await js.InvokeAsync<object>("MensajeAlertaToast");
        }

        #endregion

        #region Cookie's
        public static async Task<object> WriteCookie(this IJSRuntime js, string name, string value, int days)
        {
            return await js.InvokeAsync<object>("MyLib.WriteCookie", name, value, days);
        }
        public static async Task<string> DeleteCookie(this IJSRuntime js, string cname)
        {
            return await js.InvokeAsync<string>("MyLib.BorrarCookies", cname);
        }
        public static async Task<string> ReadCookie(this IJSRuntime js, string cname)
        {
            return await js.InvokeAsync<string>("MyLib.ReadCookie", cname);
        }
        public static async Task<string> ReloadWasm(this IJSRuntime js)
        {
            return await js.InvokeAsync<string>("MyLib.WasmReload");
        }

        public static async Task<object> GenerarPDFCotizacion(this IJSRuntime js)
        {
            return await js.InvokeAsync<object>("MyLib.GenerarPDFCotizacion");
        }
     
        public static async Task<object> MostrarPdfEnIframe(this IJSRuntime js, string Base64, string id)
        {
            return await js.InvokeAsync<object>("MyLib.MostrarPdfEnIframe", Base64, id);
        }
        public static async Task<object> SetFocus(this IJSRuntime js, string ElementId)
        {
            return await js.InvokeAsync<object>("MyLib.Focuselement", ElementId);
        }
        public static async Task<bool> IsFocused(this IJSRuntime js, string ElementId)
        {
            return await js.InvokeAsync<bool>("MyLib.IsFocused", ElementId);
        }
        public static async Task<object> SetValue(this IJSRuntime js, string ElementId, string value)
        {
            return await js.InvokeAsync<object>("MyLib.SetValue", ElementId, value);
        }
        public static async Task<object> DownloadFile(this IJSRuntime js,string filename, string bytesBase64)
        {
            return await js.InvokeAsync<object>("MyLib.DownloadFile", filename, bytesBase64);
        }
        #endregion

        #region ConsultasAPIJS
        //deje ejemplos en mantenedor articulos
        public static async Task<object> GetAsyncJS(this IJSRuntime js, string url, object? headers = null)
        {
            return await js.InvokeAsync<object>("MyLib.GetJsonFromUrl", url, headers);
        }
        //Con 30 min de timeout
        public static async Task<object> GetAsyncJSExtendTimeOut(this IJSRuntime js, string url, object? headers = null)
        {
            return await js.InvokeAsync<object>("MyLib.GetJsonFromUrlOtherTimeOut", url, headers);
        }

        public static async Task<object> PostAsyncJS(this IJSRuntime js, string url, object data, object? headers = null)
        {
            return await js.InvokeAsync<object>("MyLib.PostJsonToUrl", url, data, headers);
        }

        public static async Task<object> PostAsyncJSExtendTimeOut(this IJSRuntime js, string url, object data, object? headers = null)
        {
            return await js.InvokeAsync<object>("MyLib.PostJsonToUrlOtherTimeOut", url, data, headers);
        }
        #endregion

        #region Accions o animaciones JS
        public static async Task<object> ScrollBottom(this IJSRuntime js, ElementReference Element)
        {
            return await js.InvokeAsync<object>("MyLib.ScrollBottom", Element);
        }
        public static async Task<object> ScrollTop(this IJSRuntime js, ElementReference Element)
        {
            return await js.InvokeAsync<object>("MyLib.ScrollTop", Element);
        }

        public static async Task<object> Scroll(this IJSRuntime js, string id)
        {
            return await js.InvokeAsync<object>("MyLib.Scroll", id);
        }
        #endregion
    }
}
