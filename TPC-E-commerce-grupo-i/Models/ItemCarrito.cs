using System;

namespace TPC_E_commerce_grupo_i.Models
{
    public class ItemCarrito
    {
        public Articulo Producto { get; set; }
        public int Cantidad { get; set; }

        public decimal Subtotal
        {
            get
            {
                if (Producto != null)
                    return Producto.Precio * Cantidad;
                return 0;
            }
        }
    }
}