using Newtonsoft.Json;
using PortalEmpresas.Shared.Models;
using PortalEmpresas.Shared.Models.Login;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace PortalEmpresas.Shared.Services.Login
{
    public class LoginData
    {
        private readonly MainServices services;

        public LoginData(MainServices services)
        {
            this.services = services;
        }

        public async Task<(ApiResponse<LoginResponse>, string)> LoginPost(string usuario, string password, string rutEmpresa)
        {
            var loginRequest = new
            {
                Rut = rutEmpresa,
                Username = usuario,
                Password = password

            };

            var response = await services.test.HttpClientInstance.PostAsJsonAsync("auth/login", loginRequest);

            if (!response.IsSuccessStatusCode) return (
            new ApiResponse<LoginResponse>
            {
                Success = false,
                Errors = new List<string> { "Error en la solicitud de login." }
            }, "Error");
            var responseApi = JsonConvert.DeserializeObject<ApiResponse<LoginResponse>>(await response.Content.ReadAsStringAsync());
            return (responseApi!, "Success");

        }


    }
}
