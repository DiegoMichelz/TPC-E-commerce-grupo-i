using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio; // Importante: Referencia directa a la biblioteca de clases de Dominio

namespace TPC_E_commerce_grupo_i
{
    public partial class Carrito : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        private void CargarCarrito()
        {
            if (Session["Carrito"] != null)
            {
                // ✅ Casteo directo a la clase Carrito del proyecto 'dominio'
                dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];

                repCarrito.DataSource = carrito.Items;
                repCarrito.DataBind();

                lblTotalGeneral.Text = carrito.Total.ToString("N2");
                lblTotalGeneralDos.Text = carrito.Total.ToString("N2");
            }
            else
            {
                repCarrito.DataSource = null;
                repCarrito.DataBind();
                lblTotalGeneral.Text = "0.00";
                lblTotalGeneralDos.Text = "0.00";
            }
        }

        protected void repCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["Carrito"] != null)
            {
                // ✅ Casteo directo sin usar el prefijo 'Models.'
                dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];
                int idArticulo = int.Parse(e.CommandArgument.ToString());

                // Buscamos el ítem actual en la lista del dominio
                var itemActual = carrito.Items.FirstOrDefault(x => x.Producto.Id == idArticulo);

                if (e.CommandName == "Eliminar")
                {
                    carrito.QuitarItem(idArticulo);
                }
                else if (e.CommandName == "Sumar" && itemActual != null)
                {
                    carrito.ModificarCantidad(idArticulo, itemActual.Cantidad + 1);
                }
                else if (e.CommandName == "Restar" && itemActual != null)
                {
                    carrito.ModificarCantidad(idArticulo, itemActual.Cantidad - 1);
                }

                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }

        protected void btnVaciar_Click(object sender, EventArgs e)
        {
            if (Session["Carrito"] != null)
            {
                dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];
                carrito.Limpiar();
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }
    }
}