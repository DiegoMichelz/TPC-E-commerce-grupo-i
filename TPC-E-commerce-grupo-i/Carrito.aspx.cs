using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_E_commerce_grupo_i.Models;

namespace TPC_E_commerce_grupo_i
{
    public partial class Carrito : System.Web.UI.Page
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
                Models.Carrito carrito = (Models.Carrito)Session["Carrito"];

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
            }
        }

        protected void repCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (Session["Carrito"] != null)
            {
                Models.Carrito carrito = (Models.Carrito)Session["Carrito"];
                int idArticulo = int.Parse(e.CommandArgument.ToString());

                // Buscamos el ítem actual para ver su cantidad
                var itemActual = carrito.Items.FirstOrDefault(x => x.Producto.Id == idArticulo);

                if (e.CommandName == "Eliminar")
                {
                    carrito.QuitarItem(idArticulo); // Usamos tu método QuitarItem
                }
                else if (e.CommandName == "Sumar" && itemActual != null)
                {
                    carrito.ModificarCantidad(idArticulo, itemActual.Cantidad + 1); // Usamos tu método ModificarCantidad
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
                Models.Carrito carrito = (Models.Carrito)Session["Carrito"];
                carrito.Limpiar(); // Usamos tu método Limpiar
                Session["Carrito"] = carrito;
                CargarCarrito();
            }
        }
    }
}