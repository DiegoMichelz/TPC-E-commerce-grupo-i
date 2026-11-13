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
            Usuario nuevo = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();

            try
            {
                nuevo.Email = txtEmail.Text;
                nuevo.Pass = txtPassword.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Apellido = txtApellido.Text;
                nuevo.Admin = false; // Por defecto se registra como cliente común

                // Usamos el método de Nahuel que hace el INSERT y devuelve el ID generado
                int idGenerado = negocio.Registrar(nuevo);
                nuevo.Id = idGenerado;

                // De regalo, lo dejamos logueado automáticamente en la sesión
                Session["usuario"] = nuevo;

                // Lo mandamos al catálogo o a una pantalla de éxito
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