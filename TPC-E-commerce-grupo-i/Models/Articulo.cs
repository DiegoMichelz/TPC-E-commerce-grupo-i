using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPC_E_commerce_grupo_i.Models
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }          
        public Categoria Categoria { get; set; }
        public int Stock { get; set; }
        public List<string> ImagenesUrl { get; set; } = new List<string>();
        public decimal Precio { get; set; }
        public string ImagenPrincipal
        {
            get
            {
                if (ImagenesUrl != null && ImagenesUrl.Count > 0)
                    return ImagenesUrl[0];
                return "https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=500&q=80";
            }
        }
    }
}