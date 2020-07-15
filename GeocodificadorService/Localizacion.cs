using System;
using System.Collections.Generic;
using System.Text;

namespace GeocodificadorService
{
    public class Localizacion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }

    }
}
