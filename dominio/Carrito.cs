using System;
using System.Collections.Generic;
using System.Linq;

namespace dominio
{
    public class Carrito
    {
        public List<ItemCarrito> Items { get; set; } = new List<ItemCarrito>();

        public decimal Total
        {
            get
            {
                return Items.Sum(x => x.Subtotal);
            }
        }

        public int CantidadTotalItems
        {
            get
            {
                return Items.Sum(x => x.Cantidad);
            }
        }

        public void AgregarItem(Articulo articulo, int cantidad = 1)
        {
            if (articulo == null || articulo.Stock <= 0)
                return;

            var itemExistente = Items.FirstOrDefault(x => x.Producto.Id == articulo.Id);

            if (itemExistente != null)
            {
                if (itemExistente.Cantidad + cantidad <= articulo.Stock)
                {
                    itemExistente.Cantidad += cantidad;
                }
                else
                {
                    itemExistente.Cantidad = articulo.Stock;
                }
            }
            else
            {
                int cantidadInicial = cantidad <= articulo.Stock ? cantidad : articulo.Stock;
                Items.Add(new ItemCarrito
                {
                    Producto = articulo,
                    Cantidad = cantidadInicial
                });
            }
        }

        public void QuitarItem(int idArticulo)
        {
            var item = Items.FirstOrDefault(x => x.Producto.Id == idArticulo);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void ModificarCantidad(int idArticulo, int nuevaCantidad)
        {
            var item = Items.FirstOrDefault(x => x.Producto.Id == idArticulo);
            if (item != null)
            {
                if (nuevaCantidad > 0)
                {
                    // No permite superar el stock disponible del producto
                    item.Cantidad = nuevaCantidad <= item.Producto.Stock ? nuevaCantidad : item.Producto.Stock;
                }
                else
                {
                    Items.Remove(item);
                }
            }
        }

        public void Limpiar()
        {
            Items.Clear();
        }

        public decimal CostoEnvio
        {
            get
            {
                if (Items.Count == 0) return 0m;
                return Total >= 50000m ? 0m : 12500m;
            }
        }

        public decimal TotalConEnvio
        {
            get
            {
                return Total + CostoEnvio;
            }
        }
    }
}