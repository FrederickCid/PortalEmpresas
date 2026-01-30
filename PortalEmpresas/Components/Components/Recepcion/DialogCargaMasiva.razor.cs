using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PortalEmpresas.Components.Components.Recepcion
{
    public partial class DialogCargaMasiva
    {
        [CascadingParameter]
        private IMudDialogInstance MudDialog { get; set; }
        string Respuesta = "";


        private void Cancel()
        {
            MudDialog.Close(DialogResult.Ok(false));
        }

        private void Confirmacion()
        {
            MudDialog.Close(DialogResult.Ok(true));
        }


        #region SnackBar
        private void snakBarCreation(string msj, string position, Severity style, int duration)
        {
            _snackBar.Configuration.VisibleStateDuration = duration;
            _snackBar.Configuration.PositionClass = position;
            _snackBar.Add(msj, style);
        }
        #endregion
    }
}

