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
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;

                // Llamamos al método de login
                if (negocio.Loguear(usuario))
                {
                    // Guardamos el usuario logueado en la Session del sitio
                    Session["usuario"] = usuario;

                    // Redirigimos según el rol (si es admin a la administración, si es cliente al catálogo o carrito)
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
                    // Si falla, podemos mostrar un mensaje de error (podés sumar un Label en el .aspx para esto)
                    //Session.Add("error", "Email o contraseña incorrectos.");
                    //Response.Redirect("Error.aspx", false);
                    lblMensajeError.Text = "Email o contraseña incorrectos.";
                    lblMensajeError.Visible = true; 
                }
            }
            catch (Exception ex)
            {
                //Session.Add("error", ex.ToString());
                //Response.Redirect("Error.aspx", false);
                // Si ocurre cualquier excepción en la base de datos:
                lblMensajeError.Text = "Usuario o contraseña incorrectos.";
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