using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_E_commerce_grupo_i.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public Articulo Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }
    }
}