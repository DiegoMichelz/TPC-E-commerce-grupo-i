using System;

namespace TPC_E_commerce_grupo_i.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string CodigoPostal { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Alias { get; set; }
        public bool EsPredeterminada { get; set; }
        public bool Activo { get; set; } = true;
    }
}