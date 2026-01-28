using System;
using System.Collections.Generic;
using System.Text;

namespace PortalEmpresas.Shared.Models.Recepcion
{
    public class RecepcionEntradaExcelValidada
    {
        // Número de fila en el Excel
        public int Linea { get; set; }

        // Datos del artículo
        public string CodigoArticulo { get; set; } = string.Empty;
        public string NombreArticulo { get; set; } = string.Empty;

        // Datos de recepción
        public decimal Cantidad { get; set; }
        public decimal Bultos { get; set; }
        public string? Referencia { get; set; }

        // Estado de validación
        public bool EsValido { get; set; }
        public string Error { get; set; } = string.Empty;
    }
}
