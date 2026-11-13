using System;
using System.Web.UI;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class MiPerfil : Page
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
                Usuario user = (Usuario)Session["usuario"];
                txtEmail.Text = user.Email;
                txtNombre.Text = user.Nombre;
                txtApellido.Text = user.Apellido;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario user = (Usuario)Session["usuario"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                user.Nombre = txtNombre.Text.Trim();
                user.Apellido = txtApellido.Text.Trim();

                negocio.Actualizar(user);

                Session["usuario"] = user;

                pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                lblMensaje.Text = "¡Tus datos se actualizaron correctamente!";
                pnlMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                pnlMensaje.CssClass = "alert alert-danger alert-dismissible fade show";
                lblMensaje.Text = "Error al guardar: " + ex.Message;
                pnlMensaje.Visible = true;
            }
        }
    }
}