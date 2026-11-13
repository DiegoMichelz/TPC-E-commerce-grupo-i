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

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Borramos la sesión del usuario actual
            Session.Remove("usuario");

            // Redirigimos al inicio
            Response.Redirect("Default.aspx", false);
        }
    }


}