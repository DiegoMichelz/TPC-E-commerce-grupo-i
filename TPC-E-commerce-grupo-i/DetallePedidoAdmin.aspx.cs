using System;
using System.Web.UI;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class DetallePedidoAdmin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null || !((Usuario)Session["usuario"]).Admin)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idPedido = Convert.ToInt32(Request.QueryString["id"]);
                    CargarDetallePedido(idPedido);
                }
                else
                {
                    Response.Redirect("PedidosAdmin.aspx", false);
                }
            }
        }

        private void CargarDetallePedido(int idPedido)
        {
            PedidoNegocio negocio = new PedidoNegocio();
            Pedido pedido = negocio.ObtenerPedidoConDetalle(idPedido); // Aseguráte de tener este método que traiga los ítems

            if (pedido != null)
            {
                lblIdPedido.Text = pedido.Id.ToString();
                lblCliente.Text = pedido.Cliente.Nombre + " " + pedido.Cliente.Apellido;
                lblEmail.Text = pedido.Cliente.Email;
                lblFecha.Text = pedido.Fecha.ToString("dd/MM/yyyy HH:mm");
                lblEntrega.Text = pedido.FormaEntrega.Descripcion;
                lblPago.Text = pedido.FormaPago.Descripcion;
                lblEstado.Text = pedido.Estado;
                lblTotalGeneral.Text = pedido.Total.ToString("C2");

                gvDetalleItems.DataSource = pedido.Elementos; // Usando 'Elementos' como definiste en tu clase Pedido
                gvDetalleItems.DataBind();
            }
        }
    }
}