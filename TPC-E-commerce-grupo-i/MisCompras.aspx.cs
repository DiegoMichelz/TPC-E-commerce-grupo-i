using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class MisCompras : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        private void CargarCompras()
        {
            try
            {
                dominio.Usuario userLogueado = (dominio.Usuario)Session["usuario"];

                // 1. Obtenemos el ID del cliente asociado a este usuario
                DireccionNegocio dirNegocio = new DireccionNegocio();
                int idCliente = dirNegocio.ObtenerOCrearCliente(userLogueado, userLogueado.Nombre ?? "", userLogueado.Apellido ?? "");

                // 2. Listamos los pedidos usando el ID de Cliente correcto
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> lista = negocio.Listar(idCliente);

                gvMisCompras.DataSource = lista;
                gvMisCompras.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}