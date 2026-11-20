using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

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

                        // 1. Mapear Marca y Categoría en el label correspondiente de la tabla de especificaciones
                        string nombreMarca = seleccionado.Marca != null ? seleccionado.Marca.Descripcion : "Sin Marca";
                        string nombreCategoria = seleccionado.Categoria != null ? seleccionado.Categoria.Descripcion : "Sin Categoría";
                        lblMarcaCategoria.Text = $"{nombreMarca} / {nombreCategoria}";

                        // 2. Mapear Descripción Ampliada en la tabla de especificaciones
                        lblDetalleTecnico.Text = !string.IsNullOrWhiteSpace(seleccionado.Descripcion)
                            ? seleccionado.Descripcion
                            : "No hay especificaciones adicionales registradas para este producto.";

                        // 3. Mapear Imágenes
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

                // Usamos la capa de negocio
                ArticuloNegocio negocio = new ArticuloNegocio();

                // 1. Instancia del objeto Articulo pertenecientes al dominio
                dominio.Articulo seleccionado = negocio.Listar().Find(x => x.Id == id);

                if (seleccionado != null)
                {
                    // 2. Obtener o instanciar el Carrito de la Sesión garantizando el tipo dominio.Carrito
                    dominio.Carrito carrito = Session["Carrito"] != null
                        ? (dominio.Carrito)Session["Carrito"]
                        : new dominio.Carrito();

                    // 3. Invocamos AgregarItem pasando la instancia del tipo dominio.Articulo
                    carrito.AgregarItem(seleccionado, 1);

                    // 4. Guardar en Session y redirigir
                    Session["Carrito"] = carrito;
                    Response.Redirect("Carrito.aspx", false);
                }
            }
        }
        protected void btnComprarAhora_Click(object sender, EventArgs e)
        {
            btnAgregarCarrito_Click(sender, e);
            Response.Redirect("Checkout.aspx", false);
        }
    }
}