using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_E_commerce_grupo_i
{
    public partial class MasterE_commerce : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] != null)
                {
                    dominio.Usuario user = (dominio.Usuario)Session["usuario"];

                    if (user.Admin)
                    {
                        // Si es ADMIN: Ocultamos menú de cliente y mostramos menú de administración
                        pnlMenuCliente.Visible = false;
                        pnlMenuAdmin.Visible = true;
                    }
                    else
                    {
                        // Si es CLIENTE COMÚN: Mostramos mis compras y ocultamos panel admin
                        pnlMenuCliente.Visible = true;
                        pnlMenuAdmin.Visible = false;
                    }
                }
                else
                {
                    // Si NO está logueado: Ocultamos ambos
                    pnlMenuCliente.Visible = false;
                    pnlMenuAdmin.Visible = false;
                }
            }
        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiamos la sesión y mandamos al usuario al inicio
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx", false);
        }
    }
}