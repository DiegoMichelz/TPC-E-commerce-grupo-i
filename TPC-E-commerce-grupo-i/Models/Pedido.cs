using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_E_commerce_grupo_i.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public FormaPago FormaPago { get; set; }
        public Direccion DireccionEnvio { get; set; }
        public string Estado { get; set; } // "Pendiente", "Pagado", "En preparación", "Enviado", "Entregado", "Cancelado"
        public decimal Total { get; set; }
        public List<ItemPedido> Elementos { get; set; }

        public Pedido()
        {
            Elementos = new List<ItemPedido>();
            Fecha = DateTime.Now;
        }
    }
}