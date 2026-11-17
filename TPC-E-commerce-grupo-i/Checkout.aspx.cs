using System;
using System.Web.UI;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Carrito"] != null)
                {
                    // ✅ Casteo explícito forzando el uso de la clase del proyecto 'dominio'
                    dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];

                    if (carrito.Items != null && carrito.Items.Count > 0)
                    {
                        lblSubtotal.Text = carrito.Total.ToString("N2");
                        lblTotal.Text = carrito.Total.ToString("N2");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", false);
                        return;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx", false);
                    return;
                }

                if (Session["usuario"] != null)
                {
                    // ✅ Aplicamos el mismo casteo de namespace para el usuario
                    dominio.Usuario userLogueado = (dominio.Usuario)Session["usuario"];

                    pnlGuardarDireccion.Visible = true;

                    txtEmail.Text = userLogueado.Email;
                    txtEmail.ReadOnly = true;
                    txtNombre.Text = userLogueado.Nombre;
                    txtApellido.Text = userLogueado.Apellido;

                    DireccionNegocio dirNegocio = new DireccionNegocio();
                    dominio.Direccion dirGuardada = dirNegocio.ObtenerPorEmail(userLogueado.Email);

                    if (dirGuardada != null)
                    {
                        ViewState["IdDireccionGuardada"] = dirGuardada.Id;

                        pnlDireccionGuardada.Visible = true;
                        string piso = string.IsNullOrWhiteSpace(dirGuardada.Piso) ? "" : $" ({dirGuardada.Piso})";
                        lblResumenDireccion.Text = $"{dirGuardada.Calle}{piso} - {dirGuardada.Localidad}, {dirGuardada.Provincia} (CP {dirGuardada.CodigoPostal})";
                    }
                }
            }
        }




        protected void chkUsarGuardada_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["usuario"] == null) return;

            if (chkUsarGuardada.Checked)
            {
                Usuario userLogueado = (Usuario)Session["usuario"];
                DireccionNegocio dirNegocio = new DireccionNegocio();
                Direccion dirGuardada = dirNegocio.ObtenerPorEmail(userLogueado.Email);

                if (dirGuardada != null)
                {
                    txtDireccion.Text = dirGuardada.Calle;
                    txtPisoDepto.Text = dirGuardada.Piso;
                    txtLocalidad.Text = dirGuardada.Localidad;
                    txtProvincia.Text = dirGuardada.Provincia;
                    txtCP.Text = dirGuardada.CodigoPostal;

                    chkGuardarDireccion.Checked = false;
                }
            }
            else
            {
                txtDireccion.Text = "";
                txtPisoDepto.Text = "";
                txtLocalidad.Text = "";
                txtProvincia.Text = "";
                txtCP.Text = "";
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Se quita el prefijo 'Models.' para usar directamente la clase de 'dominio'
                Carrito carrito = (Carrito)Session["Carrito"];
                if (carrito == null) return;

                if (Session["usuario"] != null && chkGuardarDireccion.Checked)
                {
                    Usuario userLogueado = (Usuario)Session["usuario"];
                    DireccionNegocio dirNegocio = new DireccionNegocio();

                    int idCliente = dirNegocio.ObtenerOCrearCliente(userLogueado, txtNombre.Text.Trim(), txtApellido.Text.Trim());

                    Direccion dir = new Direccion();
                    dir.Id = ViewState["IdDireccionGuardada"] != null ? (int)ViewState["IdDireccionGuardada"] : 0;
                    dir.Calle = txtDireccion.Text.Trim();
                    dir.Numero = "S/N";
                    dir.Piso = txtPisoDepto.Text.Trim();
                    dir.Localidad = txtLocalidad.Text.Trim();
                    dir.Provincia = txtProvincia.Text.Trim();
                    dir.CodigoPostal = txtCP.Text.Trim();

                    dirNegocio.GuardarOActualizar(dir, idCliente);

                    if (string.IsNullOrWhiteSpace(userLogueado.Nombre) || string.IsNullOrWhiteSpace(userLogueado.Apellido))
                    {
                        userLogueado.Nombre = txtNombre.Text.Trim();
                        userLogueado.Apellido = txtApellido.Text.Trim();
                        UsuarioNegocio userNegocio = new UsuarioNegocio();
                        userNegocio.Actualizar(userLogueado);
                        Session["usuario"] = userLogueado;
                    }
                }

                Session.Remove("Carrito");
                Response.Redirect("Confirmacion.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}