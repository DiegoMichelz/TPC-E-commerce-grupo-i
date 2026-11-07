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
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validamos que exista un carrito con productos en la sesión
                if (Session["Carrito"] != null)
                {
                    Models.Carrito carrito = (Models.Carrito)Session["Carrito"];
                    if (carrito.Items != null && carrito.Items.Count > 0)
                    {
                        // Mostramos los montos reales actualizados
                        lblSubtotal.Text = carrito.Total.ToString("N2");
                        lblTotal.Text = carrito.Total.ToString("N2");
                    }
                    else
                    {
                        // Si el carrito está vacío, volvemos al catálogo
                        Response.Redirect("Default.aspx", false);
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                Models.Carrito carrito = (Models.Carrito)Session["Carrito"];
                if (carrito == null) return;

                // Aquí podés armar tu objeto Pedido y persistirlo con PedidoNegocio
                // Ejemplo de estructura genérica:
                /*
                Pedido nuevoPedido = new Pedido();
                nuevoPedido.Fecha = DateTime.Now;
                nuevoPedido.Total = carrito.Total;
                nuevoPedido.DireccionEnvio = txtDireccion.Text + ", " + txtLocalidad.Text;
                
                // Guardamos en la base de datos
                PedidoNegocio negocio = new PedidoNegocio();
                int idPedido = negocio.Agregar(nuevoPedido, carrito.Items);
                */

                // 1. Vaciamos el carrito de la sesión tras la compra exitosa
                Session.Remove("Carrito");

                // 2. Redirigimos a una pantalla de éxito (o al inicio con aviso)
                Response.Redirect("Default.aspx?compra=exitosa", false);
            }
            catch (Exception ex)
            {
                // Manejo de errores básico en caso de que falle la base de datos
                throw ex;
            }
        }
    }
}