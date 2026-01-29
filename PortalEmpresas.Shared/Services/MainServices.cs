using System;
using System.Collections.Generic;
using System.Text;

namespace PortalEmpresas.Shared.Services
{
    public class MainServices
    {
        #region Instancias de Clientes

        public ClientFactory test { get; set; } = new ClientFactory("https://localhost:44316/");
   

        #endregion

        private static MainServices instance;
        public static MainServices GetInstance()
        {
            if (instance == null)
            {
                return new MainServices();
            }

            return instance;
        }

        public MainServices()
        {
            instance = this;
        }


    }
}
