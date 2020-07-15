using System;
using System.Collections.Generic;

namespace Api_Geo.Models
{
    public partial class Pedido
    {
        public int Id { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
 
    }
}
