using System;
using System.Collections.Generic;
using System.Linq;

namespace TPC_E_commerce_grupo_i.Models
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

        // Métodos auxiliares para gestionar el carrito en memoria/Session

        public void AgregarItem(Articulo articulo, int cantidad = 1)
        {
            var itemExistente = Items.FirstOrDefault(x => x.Producto.Id == articulo.Id);

            if (itemExistente != null)
            {
                itemExistente.Cantidad += cantidad;
            }
            else
            {
                Items.Add(new ItemCarrito
                {
                    Producto = articulo,
                    Cantidad = cantidad
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
                    item.Cantidad = nuevaCantidad;
                else
                    Items.Remove(item);
            }
        }

        public void Limpiar()
        {
            Items.Clear();
        }
    }
}