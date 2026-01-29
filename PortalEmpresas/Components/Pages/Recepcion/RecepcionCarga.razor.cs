using ClosedXML.Excel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using PortalAG_V2.Services;
using PortalEmpresas.Shared.Models.Recepcion;
using PortalEmpresas.Shared.Services.Recepcion;

namespace PortalEmpresas.Components.Pages.Recepcion
{
    public partial class RecepcionCarga
    {
        [Inject] IJSRuntime js { get; set; }       
        // SOLO placeholders (no lógica aún)
        private string NombreArchivo = string.Empty;
        private bool ArchivoCargado = false;
        private bool DeshabilitarEnviar = true;

        private List<RecepcionEntradaExcelValidada> Listado = new();

        void UploadFiles() { }
        void ProcesarArchivo() { }
        private async Task DescargarEjemplo() 
        {
            byte[] excelBytes = GenerarExcelEjemplo();

            string base64 = Convert.ToBase64String(excelBytes);

            await js.DownloadFile("Excel Ejemplo.xlsx", base64);
        }
        private byte[] GenerarExcelEjemplo()
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Entradas");
            var i = 1;
            // Encabezados
            worksheet.Cell(i, 1).Value = "Linea";
            worksheet.Cell(i, 2).Value = "Codigo Artículo";
            worksheet.Cell(i, 3).Value = "Nombre Artículo";
            worksheet.Cell(i, 4).Value = "Cantidad";
            worksheet.Cell(i, 5).Value = "Bultos";
            worksheet.Cell(i, 6).Value = "Referencia";

            // Estilo encabezados
            var headerRange = worksheet.Range("A1:F1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        void EliminarArchivo() { }
        void Enviar() { }
        void Limpiar() { }
    }
}
