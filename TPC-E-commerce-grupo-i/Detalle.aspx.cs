using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idStr = Request.QueryString["id"];
                if (idStr != null)
                {
                    int id = int.Parse(idStr);
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    List<Articulo> lista = negocio.Listar();

                    // Buscamos el artículo específico
                    Articulo seleccionado = lista.Find(x => x.Id == id);

                    if (seleccionado != null)
                    {
                        // 1. Asignamos la información de texto a los Labels
                        lblCodigo.Text = seleccionado.Codigo;
                        lblNombre.Text = seleccionado.Nombre;
                        lblPrecio.Text = seleccionado.Precio.ToString("N2");
                        lblDescripcion.Text = seleccionado.Descripcion;

                        // 2. Cargamos las imágenes (si tiene)
                        if (seleccionado.ImagenesUrl != null && seleccionado.ImagenesUrl.Count > 0)
                        {
                            imgPrincipal.ImageUrl = seleccionado.ImagenesUrl[0];
                            RepeaterImagenes.DataSource = seleccionado.ImagenesUrl;
                            RepeaterImagenes.DataBind();
                        }
                    }
                }
            }
        }
    }
}