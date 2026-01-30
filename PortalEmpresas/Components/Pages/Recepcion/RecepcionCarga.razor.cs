using Backend.Api.PortalEmpresas.Shared.Models.Recepcion;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MudBlazor;
using PortalAG_V2.Services;
using PortalEmpresas.Components.Components.Recepcion;
using PortalEmpresas.Shared.Models.Recepcion;
using PortalEmpresas.Shared.Services.Recepcion;

namespace PortalEmpresas.Components.Pages.Recepcion
{
    public partial class RecepcionCarga
    {
        [Inject] IJSRuntime js { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] RecepcionService RecepcionService { get; set; }

        private IBrowserFile? ArchivoExcel;
        private string NombreArchivo = string.Empty;
        private bool ArchivoCargado = false;
        private bool DeshabilitarEnviar = true;
        private bool Procesando = false;

        private List<RecepcionEntradaExcelValidada> Listado = new();

        #region Carga archivo

        void UploadFiles(InputFileChangeEventArgs e)
        {
            ArchivoExcel = e.File;
            NombreArchivo = ArchivoExcel.Name;
            ArchivoCargado = true;
            DeshabilitarEnviar = true;
            Listado.Clear();
        }

        void EliminarArchivo()
        {
            ArchivoExcel = null;
            NombreArchivo = string.Empty;
            ArchivoCargado = false;
            DeshabilitarEnviar = true;
            Listado.Clear();
        }

        #endregion

        #region Procesar Excel

        async void ProcesarArchivo()
        {
            if (ArchivoExcel == null)
            {
                snakBarCreation("Debe seleccionar un archivo.", Defaults.Classes.Position.BottomStart, Severity.Warning, 3);
                return;
            }

            try
            {
                using var stream = ArchivoExcel.OpenReadStream(10_000_000);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);

                using var workbook = new XLWorkbook(ms);
                var worksheet = workbook.Worksheet(1);

                var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
                int linea = 1;

                Listado.Clear();

                foreach (var row in rows)
                {
                    var item = new RecepcionEntradaExcelValidada
                    {
                        Linea = linea++,
                        CodigoArticulo = row.Cell(2).GetString(),
                        NombreArticulo = row.Cell(3).GetString(),
                        Referencia = row.Cell(6).GetString()
                    };

                    if (!decimal.TryParse(row.Cell(4).GetString(), out var cantidad) || cantidad <= 0)
                        item.Error = "Cantidad inválida";
                    else
                        item.Cantidad = cantidad;

                    if (!int.TryParse(row.Cell(5).GetString(), out var bultos) || bultos < 0)
                        item.Error += string.IsNullOrEmpty(item.Error) ? "Bultos inválidos" : " | Bultos inválidos";
                    else
                        item.Bultos = bultos;

                    item.EsValido = string.IsNullOrEmpty(item.Error);

                    Listado.Add(item);
                }

                DeshabilitarEnviar = !Listado.Any(x => x.EsValido);

                snakBarCreation("Archivo procesado correctamente.", Defaults.Classes.Position.BottomStart, Severity.Success, 3);
            }
            catch (Exception ex)
            {
                snakBarCreation($"Error al procesar archivo: {ex.Message}",Defaults.Classes.Position.BottomStart, Severity.Error, 3);
            }
        }

        #endregion

        #region Enviar

        async void Enviar()
        {
            if (!Listado.Any(x => x.EsValido))
            {
                snakBarCreation("No hay registros válidos para enviar.", Defaults.Classes.Position.BottomStart, Severity.Warning, 3);
                return;
            }
            var parameters = new DialogParameters<DialogCargaMasiva> { };
            var options = new MudBlazor.DialogOptions() { CloseButton = false, BackdropClick = false, };
            var dialog = await DialogService.ShowAsync<DialogCargaMasiva>("Question", parameters, options);
            var result = await dialog.Result;
            if (result.Canceled)
                return;

            Procesando = true;
            DeshabilitarEnviar = true;

            foreach (var item in Listado.Where(x => x.EsValido))
            {
                var model = new CreateProductModel
                {
                    ArticulosExcel = new()
                    {
                        new ProductRecepcion
                        {
                            Nombre = item.NombreArticulo
                        }
                    }
                };

                var (response, error) = await RecepcionService.PostCreateProduct(model);

                if (response == null || !string.IsNullOrEmpty(error))
                {
                    item.Error = error ?? "Error desconocido";
                    item.EsValido = false;
                }
            }

            Procesando = false;
            snakBarCreation("Proceso finalizado.", Defaults.Classes.Position.BottomStart, Severity.Success, 3);
        }

        #endregion

        #region Utilidades

        void Limpiar()
        {
            ArchivoExcel = null;
            NombreArchivo = string.Empty;
            ArchivoCargado = false;
            DeshabilitarEnviar = true;
            Listado.Clear();
        }

        #endregion

        #region Excel ejemplo

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

            worksheet.Cell(1, 1).Value = "Linea";
            worksheet.Cell(1, 2).Value = "Codigo Artículo";
            worksheet.Cell(1, 3).Value = "Nombre Artículo";
            worksheet.Cell(1, 4).Value = "Cantidad";
            worksheet.Cell(1, 5).Value = "Bultos";
            worksheet.Cell(1, 6).Value = "Referencia";

            var header = worksheet.Range("A1:F1");
            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.LightGray;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        #endregion
        private void snakBarCreation(string msj, string position, Severity style, int duration)
        {
            _snackBar.Configuration.VisibleStateDuration = duration;
            _snackBar.Configuration.PositionClass = position;
            _snackBar.Add(msj, style);
        }
    }
}
