using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_E_commerce_grupo_i.Models
{
    public class HistorialEstadoPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public string EstadoAnterior { get; set; }
        public string EstadoNuevo { get; set; }
        public DateTime FechaCambio { get; set; }
        public string Observaciones { get; set; }

        public HistorialEstadoPedido()
        {
            FechaCambio = DateTime.Now;
        }
    }
}