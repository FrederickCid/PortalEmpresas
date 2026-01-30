using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Api.PortalEmpresas.Shared.Models.Recepcion
{
    public class CreateProductModel
    {
        public int IdEmpresa { get; set; }
        public int IdUsuario { get; set; }
        public List<ProductRecepcion> ArticulosExcel { get; set; }
  
    }

    public class ProductRecepcion
    {
        public string IdArticulo { get; set; }
        public string IdEmpresa { get; set; }
        public string Nombre { get; set; }
        public decimal Ean { get; set; }
        public int Sku { get; set; }
        public int NroParte { get; set; }
    }
}
