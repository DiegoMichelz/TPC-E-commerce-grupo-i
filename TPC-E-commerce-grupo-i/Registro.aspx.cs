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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Opcional: si ya está logueado, lo mandamos al inicio
            if (!IsPostBack && Session["usuario"] != null)
            {
                Response.Redirect("Default.aspx", false);
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                if (!Validacion.EsEmailValido(txtEmail))
                {
                    lblError.Text = "Ingresá un correo electrónico válido.";
                    lblError.Visible = true;
                    return;
                }

                if (Validacion.EstaVacio(txtPassword) || txtPassword.Text.Trim().Length < 4)
                {
                    lblError.Text = "La contraseña debe tener al menos 4 caracteres.";
                    lblError.Visible = true;
                    return;
                }

                if (Validacion.EstaVacio(txtNombre) || Validacion.EstaVacio(txtApellido))
                {
                    lblError.Text = "Nombre y apellido son obligatorios.";
                    lblError.Visible = true;
                    return;
                }

                string emailLimpio = txtEmail.Text.Trim();
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Validar que el correo no esté registrado previamente
                bool emailExistente = negocio.Listar().Exists(x =>
                    x.Email != null &&
                    x.Email.Trim().Equals(emailLimpio, StringComparison.OrdinalIgnoreCase));

                if (emailExistente)
                {
                    lblError.Text = "Ya existe una cuenta registrada con ese correo electrónico.";
                    lblError.Visible = true;
                    return;
                }

                Usuario nuevo = new Usuario();
                nuevo.Email = emailLimpio;
                nuevo.Pass = txtPassword.Text;
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Apellido = txtApellido.Text.Trim();
                nuevo.Admin = false;
                nuevo.Activo = true;

                int idGenerado = negocio.Registrar(nuevo);
                nuevo.Id = idGenerado;

                Session["usuario"] = nuevo;
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}