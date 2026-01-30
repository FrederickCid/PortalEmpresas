using Backend.Api.PortalEmpresas.Shared.Models.Recepcion;
using Newtonsoft.Json;
using PortalEmpresas.Shared.Models;
using PortalEmpresas.Shared.Models.Login;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace PortalEmpresas.Shared.Services.Recepcion
{
    public class RecepcionService
    {
        private readonly MainServices services;
        readonly string url = "/recepcion/create-product";
        public RecepcionService(MainServices services)
        {
            this.services = services;
        }

        public async Task<(ApiResponse<CreateProductModel>?, string)> PostCreateProduct(CreateProductModel model)
        {

            var response = await services.test.HttpClientInstance.PostAsJsonAsync(url, model);

            if (!response.IsSuccessStatusCode) return (
            new ApiResponse<CreateProductModel>
            {
                Success = false,
                Errors = new List<string> { "Error en la solicitud de login." }
            }, "Error");
            var responseApi = JsonConvert.DeserializeObject<ApiResponse<CreateProductModel>>(await response.Content.ReadAsStringAsync());
            if (!responseApi.Errors.Any(x => x.ToUpper() != "succes".ToUpper())) return (
               new ApiResponse<CreateProductModel>
               {
                   Success = false,
                   Errors = responseApi.Errors
               },  "Error");
            return (responseApi!, "Success");
        }
    }
}
