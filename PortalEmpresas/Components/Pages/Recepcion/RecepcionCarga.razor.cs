using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PortalEmpresas.Shared.Models.Recepcion;
using PortalEmpresas.Shared.Services.Recepcion;

namespace PortalEmpresas.Components.Pages.Recepcion
{
    public partial class RecepcionCarga
    {
        // SOLO placeholders (no lógica aún)
        private string NombreArchivo = string.Empty;
        private bool ArchivoCargado = false;
        private bool DeshabilitarEnviar = true;

        private List<RecepcionEntradaExcelValidada> Listado = new();

        void UploadFiles() { }
        void ProcesarArchivo() { }
        void DescargarEjemplo() { }
        void EliminarArchivo() { }
        void Enviar() { }
        void Limpiar() { }
    }
}
