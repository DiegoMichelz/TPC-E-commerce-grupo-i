using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class UsuariosAdmin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación de Seguridad: Solo Administradores
            Usuario user = Session["usuario"] as Usuario;
            if (user == null || !user.Admin)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            string filtro = ddlFiltroEstado.SelectedValue;
            dgvUsuarios.DataSource = negocio.Listar(filtro);
            dgvUsuarios.DataBind();
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int idUsuario = Convert.ToInt32(e.CommandArgument);
            UsuarioNegocio negocio = new UsuarioNegocio();

            if (e.CommandName == "Inactivar")
            {
                negocio.CambiarEstado(idUsuario, false);
                CargarGrilla();
            }
            else if (e.CommandName == "Reactivar")
            {
                negocio.CambiarEstado(idUsuario, true);
                CargarGrilla();
            }
            else if (e.CommandName == "ToggleAdmin")
            {
                List<Usuario> lista = negocio.Listar("TODOS");
                Usuario seleccionado = lista.Find(u => u.Id == idUsuario);
                if (seleccionado != null)
                {
                    negocio.CambiarRol(idUsuario, !seleccionado.Admin);
                    CargarGrilla();
                }
            }
        }
    }
}