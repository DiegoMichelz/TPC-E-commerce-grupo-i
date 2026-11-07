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

                    Articulo seleccionado = lista.Find(x => x.Id == id);

                    if (seleccionado != null)
                    {
                        lblCodigo.Text = seleccionado.Codigo;
                        lblNombre.Text = seleccionado.Nombre;
                        lblPrecio.Text = seleccionado.Precio.ToString("N2");
                        lblDescripcion.Text = seleccionado.Descripcion;

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

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            string idStr = Request.QueryString["id"];
            if (idStr != null)
            {
                int id = int.Parse(idStr);
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.Listar();

                // Instancia del objeto Articulo
                Articulo seleccionado = lista.Find(x => x.Id == id);

                if (seleccionado != null)
                {
                    // Obtener o instanciar el Carrito desde la Sesión
                    TPC_E_commerce_grupo_i.Models.Carrito carrito = Session["Carrito"] != null
                    ? (TPC_E_commerce_grupo_i.Models.Carrito)Session["Carrito"]
                    : new TPC_E_commerce_grupo_i.Models.Carrito();

                    // Invocar AgregarItem pasando la instancia de Articulo
                    carrito.AgregarItem(seleccionado, 1);

                    // Guardar en Session y redirigir
                    Session["Carrito"] = carrito;
                    Response.Redirect("Carrito.aspx", false);
                }
            }
        }
    }
}