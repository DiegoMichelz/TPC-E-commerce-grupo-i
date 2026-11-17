using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dominio
{
    public class FormaPago
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; } = true;

        public override string ToString()
        {
            return Descripcion;
        }
    }
}