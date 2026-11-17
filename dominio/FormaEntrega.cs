using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
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