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

                        // 1. Control visual y bloqueo según Stock disponible
                        if (seleccionado.Stock <= 0)
                        {
                            lblStockBadge.Text = "Sin stock disponible";
                            lblStockBadge.CssClass = "badge bg-danger bg-opacity-10 text-danger border border-danger border-opacity-25";

                            btnAgregarCarrito.Enabled = false;
                            btnAgregarCarrito.Text = "Sin Stock";
                            btnAgregarCarrito.CssClass = "btn btn-secondary btn-lg py-3 fw-bold shadow-sm w-100";

                            btnComprarAhora.Visible = false;
                        }
                        else if (seleccionado.Stock <= 5)
                        {
                            lblStockBadge.Text = $"¡Últimas {seleccionado.Stock} unidades disponibles!";
                            lblStockBadge.CssClass = "badge bg-warning bg-opacity-25 text-dark border border-warning";
                        }
                        else
                        {
                            lblStockBadge.Text = $"Stock disponible: {seleccionado.Stock} unidades";
                            lblStockBadge.CssClass = "badge bg-success bg-opacity-10 text-success border border-success border-opacity-25";
                        }

                        // 2. Mapear Marca y Categoría
                        string nombreMarca = seleccionado.Marca != null ? seleccionado.Marca.Descripcion : "Sin Marca";
                        string nombreCategoria = seleccionado.Categoria != null ? seleccionado.Categoria.Descripcion : "Sin Categoría";
                        lblMarcaCategoria.Text = $"{nombreMarca} / {nombreCategoria}";

                        // 3. Mapear Descripción Ampliada
                        lblDetalleTecnico.Text = !string.IsNullOrWhiteSpace(seleccionado.Descripcion)
                            ? seleccionado.Descripcion
                            : "No hay especificaciones adicionales registradas para este producto.";

                        // 4. Mapear Imágenes
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
                dominio.Articulo seleccionado = negocio.Listar().Find(x => x.Id == id);

                if (seleccionado != null)
                {
                    // Validación de seguridad en servidor por si no hay stock
                    if (seleccionado.Stock <= 0)
                    {
                        return;
                    }

                    dominio.Carrito carrito = Session["Carrito"] != null
                        ? (dominio.Carrito)Session["Carrito"]
                        : new dominio.Carrito();

                    // Opcional: evitar agregar más unidades al carrito de las que existen en stock
                    var itemExistente = carrito.Items.Find(x => x.Producto.Id == seleccionado.Id);
                    int cantidadEnCarrito = itemExistente != null ? itemExistente.Cantidad : 0;

                    if (cantidadEnCarrito + 1 > seleccionado.Stock)
                    {
                        lblStockBadge.Text = $"Máximo disponible alcanzado ({seleccionado.Stock} unid.)";
                        lblStockBadge.CssClass = "badge bg-warning bg-opacity-25 text-dark border border-warning";
                        return;
                    }

                    carrito.AgregarItem(seleccionado, 1);
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