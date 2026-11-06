using System;
using System.Collections.Generic;

namespace TPC_E_commerce_grupo_i.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public bool Activo { get; set; }

        // Agregamos la lista de direcciones asociadas al cliente
        public List<Direccion> Direcciones { get; set; } = new List<Direccion>();
    }
}