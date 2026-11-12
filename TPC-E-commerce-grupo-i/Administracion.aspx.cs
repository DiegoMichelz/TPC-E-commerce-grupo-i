using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class Administracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                List<Articulo> lista = negocio.Listar();

                // Guardamos en Session para la administración
                Session["listaArticulosAdmin"] = lista;

                dgvArticulos.DataSource = lista;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.SelectedDataKey != null)
            {
                string id = dgvArticulos.SelectedDataKey.Value.ToString();
                Response.Redirect("FormularioArticulo.aspx?id=" + id, false);
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioArticulo.aspx", false);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            CargarGrilla();
        }

        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int idArticulo = Convert.ToInt32(e.CommandArgument);

                    ArticuloNegocio negocio = new ArticuloNegocio();
                    negocio.EliminarLogico(idArticulo);

                    // Recargar la grilla invocando a tu método de carga habitual
                    CargarGrilla(); // o el método que uses para enlazar dgvArticulos.DataBind()
                }
                catch (Exception ex)
                {
                    Session.Add("error", ex.ToString());
                    Response.Redirect("Error.aspx", false);
                }
            }
        }
    }
}