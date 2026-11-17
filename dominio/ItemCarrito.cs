using System;

namespace dominio
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