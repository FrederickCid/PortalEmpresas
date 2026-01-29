using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PortalEmpresas.Shared.Models.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "RUT empresa es requerido")]
        public string RutEmpresa { get; set; } = "";

        [Required(ErrorMessage = "Email es requerido")]
        [EmailAddress(ErrorMessage = "Email debe ser válido")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Contraseña es requerida")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Contraseña debe tener entre 6 y 50 caracteres")]
        public string Password { get; set; } = "";

        public bool Recordar { get; set; }
    }
}
