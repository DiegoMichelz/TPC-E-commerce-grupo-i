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
            pnlDetalleUsuario.Visible = false;
            pnlConfirmarBloqueo.Visible = false;
            CargarGrilla();
        }

        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument == null) return;

            int idUsuario = Convert.ToInt32(e.CommandArgument);

            Usuario logueado = Session["usuario"] as Usuario;
            if (logueado != null && logueado.Id == idUsuario && (e.CommandName == "Inactivar" || e.CommandName == "ToggleAdmin"))
            {
                return; // Evita que el admin se bloquee o se quite permisos a sí mismo
            }

            if (e.CommandName == "VerDetalle")
            {
                pnlConfirmarBloqueo.Visible = false;
                MostrarDetalleUsuario(idUsuario);
                return;
            }

            if (e.CommandName == "Modificar")
            {
                Response.Redirect("FormularioUsuario.aspx?id=" + idUsuario, false);
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();

            if (e.CommandName == "Inactivar")
            {
                pnlDetalleUsuario.Visible = false;
                ViewState["IdUsuarioBloquear"] = idUsuario;

                Usuario seleccionado = negocio.Listar("TODOS").Find(u => u.Id == idUsuario);
                lblUsuarioBloquear.Text = seleccionado != null ? seleccionado.Email : ("ID #" + idUsuario);

                pnlConfirmarBloqueo.Visible = true;
            }
            else if (e.CommandName == "Reactivar")
            {
                pnlConfirmarBloqueo.Visible = false;
                negocio.CambiarEstado(idUsuario, true);
                CargarGrilla();
            }
            else if (e.CommandName == "ToggleAdmin")
            {
                pnlConfirmarBloqueo.Visible = false;
                List<Usuario> lista = negocio.Listar("TODOS");
                Usuario seleccionado = lista.Find(u => u.Id == idUsuario);
                if (seleccionado != null)
                {
                    negocio.CambiarRol(idUsuario, !seleccionado.Admin);
                    CargarGrilla();
                }
            }
        }

        protected void btnConfirmarBloqueo_Click(object sender, EventArgs e)
        {
            if (ViewState["IdUsuarioBloquear"] != null)
            {
                int idUsuario = (int)ViewState["IdUsuarioBloquear"];
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.CambiarEstado(idUsuario, false);

                ViewState["IdUsuarioBloquear"] = null;
                pnlConfirmarBloqueo.Visible = false;
                CargarGrilla();
            }
        }

        protected void btnCancelarBloqueo_Click(object sender, EventArgs e)
        {
            ViewState["IdUsuarioBloquear"] = null;
            pnlConfirmarBloqueo.Visible = false;
        }

        private void MostrarDetalleUsuario(int idUsuario)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario seleccionado = usuarioNegocio.Listar("TODOS").Find(u => u.Id == idUsuario);
            if (seleccionado == null) return;

            string nombreCompleto = $"{seleccionado.Nombre} {seleccionado.Apellido}".Trim();
            lblDetNombre.Text = string.IsNullOrWhiteSpace(nombreCompleto) ? "(Sin nombre)" : nombreCompleto;
            lblDetEmail.Text = seleccionado.Email;
            lblDetRolEstado.Text = (seleccionado.Admin ? "Administrador" : "Comprador") + " / " + (seleccionado.Activo ? "Activo" : "Inactivo");

            ClienteNegocio clienteNegocio = new ClienteNegocio();
            Cliente cli = clienteNegocio.BuscarPorEmail(seleccionado.Email);
            lblDetTelefono.Text = (cli != null && !string.IsNullOrWhiteSpace(cli.Telefono)) ? cli.Telefono : "No registrado";

            DireccionNegocio direccionNegocio = new DireccionNegocio();
            Direccion dir = direccionNegocio.ObtenerPorEmail(seleccionado.Email);
            if (dir != null)
            {
                string pisoDepto = "";
                if (!string.IsNullOrWhiteSpace(dir.Piso)) pisoDepto += $" Piso {dir.Piso}";
                if (!string.IsNullOrWhiteSpace(dir.Departamento)) pisoDepto += $" Depto {dir.Departamento}";

                lblDetDireccion.Text = $"{dir.Calle} {dir.Numero}{pisoDepto} - CP {dir.CodigoPostal}, {dir.Localidad}, {dir.Provincia}";
            }
            else
            {
                lblDetDireccion.Text = seleccionado.Admin ? "No aplica (Administrador)" : "Sin dirección registrada";
            }

            pnlDetalleUsuario.Visible = true;
        }

        protected void btnCerrarDetalle_Click(object sender, EventArgs e)
        {
            pnlDetalleUsuario.Visible = false;
        }
    }
}