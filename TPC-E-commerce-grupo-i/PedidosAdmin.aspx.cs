using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class PedidosAdmin : Page
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
                CargarPedidos();
            }
        }

        private void CargarPedidos()
        {
            PedidoNegocio negocio = new PedidoNegocio();
            gvPedidosAdmin.DataSource = negocio.ListarTodos();
            gvPedidosAdmin.DataBind();

            // Seteamos el valor actual del DropDownList en cada fila según la base de datos
            foreach (GridViewRow row in gvPedidosAdmin.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl = (DropDownList)row.FindControl("ddlEstado");
                    HiddenField hdn = (HiddenField)row.FindControl("hdnEstadoActual");
                    if (ddl != null && hdn != null)
                    {
                        ddl.SelectedValue = hdn.Value;
                    }
                }
            }
        }

        protected void gvPedidosAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ActualizarEstado")
            {
                int idPedido = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                DropDownList ddlEstado = (DropDownList)row.FindControl("ddlEstado");

                if (ddlEstado != null)
                {
                    string nuevoEstado = ddlEstado.SelectedValue;
                    PedidoNegocio negocio = new PedidoNegocio();
                    negocio.ModificarEstado(idPedido, nuevoEstado);
                    CargarPedidos(); // Refrescamos la grilla
                }
            }
        }
    }
}