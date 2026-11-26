using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Opcional: Si ya hay un usuario en sesión, lo mandamos directo al inicio o al carrito
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    Response.Redirect("Default.aspx", false);
                }
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            // Validamos que no estén vacíos antes de ir a la base de datos
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblMensajeError.Text = "Por favor, completá ambos campos para ingresar.";
                lblMensajeError.Visible = true;
                return;
            }

            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                usuario.Email = txtEmail.Text.Trim();
                usuario.Pass = txtPassword.Text.Trim();

                // Llamamos al método de login
                if (negocio.Loguear(usuario))
                {
                    // Guardamos el usuario logueado en la Session del sitio
                    Session["usuario"] = usuario;

                    // Redirigimos según el rol
                    if (usuario.Admin)
                    {
                        Response.Redirect("Administracion.aspx", false);
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", false);
                    }
                }
                else
                {
                    lblMensajeError.Text = "Email o contraseña incorrectos.";
                    lblMensajeError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Muestra el mensaje de error (incluyendo si la cuenta está inactiva/bloqueada)
                lblMensajeError.Text = ex.Message;
                lblMensajeError.Visible = true;
            }
        }

        protected void btnInvitado_Click(object sender, EventArgs e)
        {
            // Limpiamos cualquier sesión previa por las dudas
            Session.Remove("usuario");

            // Lo mandamos directo al catálogo (o al carrito) para que compre como invitado
            Response.Redirect("Checkout.aspx", false);
        }
    }
}