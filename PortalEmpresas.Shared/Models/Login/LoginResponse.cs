using System;
using System.Collections.Generic;
using System.Text;

namespace PortalEmpresas.Shared.Models.Login
{
    public class LoginResponse
    {

        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime expiraEn { get; set; }
    }


}
