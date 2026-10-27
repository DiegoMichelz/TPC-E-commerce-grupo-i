using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_E_commerce_grupo_i.Models
{
    public class FormaEntrega
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public bool Estado { get; set; } = true;

        public override string ToString()
        {
            return Descripcion;
        }
    }
}