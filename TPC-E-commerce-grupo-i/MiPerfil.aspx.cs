using System;
using System.Web.UI;
using dominio;
using negocio;

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

                DireccionNegocio dirNegocio = new DireccionNegocio();
                Direccion dir = dirNegocio.ObtenerPorEmail(user.Email);
                if (dir != null)
                {
                    ViewState["IdDireccionPerfil"] = dir.Id;
                    txtCalle.Text = dir.Calle;

                    string pisoDepto = dir.Piso;
                    if (!string.IsNullOrWhiteSpace(dir.Departamento))
                    {
                        pisoDepto = string.IsNullOrWhiteSpace(pisoDepto)
                            ? dir.Departamento
                            : pisoDepto + " " + dir.Departamento;
                    }
                    txtPisoDepto.Text = pisoDepto;

                    txtLocalidad.Text = dir.Localidad;
                    txtProvincia.Text = dir.Provincia;
                    txtCP.Text = dir.CodigoPostal;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMensaje.Visible = false;

                Usuario user = (Usuario)Session["usuario"];
                string nuevoNombre = txtNombre.Text.Trim();
                string nuevoApellido = txtApellido.Text.Trim();

                if (string.IsNullOrWhiteSpace(nuevoNombre) || string.IsNullOrWhiteSpace(nuevoApellido))
                {
                    pnlMensaje.CssClass = "alert alert-warning alert-dismissible fade show";
                    lblMensaje.Text = "Completá tu nombre y apellido.";
                    pnlMensaje.Visible = true;
                    return;
                }

                user.Nombre = nuevoNombre;
                user.Apellido = nuevoApellido;

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Actualizar(user);
                Session["usuario"] = user;

                bool tieneDatosDireccion = !string.IsNullOrWhiteSpace(txtCalle.Text) ||
                                         !string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                                         !string.IsNullOrWhiteSpace(txtProvincia.Text) ||
                                         !string.IsNullOrWhiteSpace(txtCP.Text);

                int idDirActual = ViewState["IdDireccionPerfil"] != null ? (int)ViewState["IdDireccionPerfil"] : 0;

                if (tieneDatosDireccion || idDirActual > 0)
                {
                    if (string.IsNullOrWhiteSpace(txtCalle.Text) ||
                        string.IsNullOrWhiteSpace(txtLocalidad.Text) ||
                        string.IsNullOrWhiteSpace(txtProvincia.Text) ||
                        string.IsNullOrWhiteSpace(txtCP.Text))
                    {
                        pnlMensaje.CssClass = "alert alert-warning alert-dismissible fade show";
                        lblMensaje.Text = "Se guardaron tus datos personales, pero completá Calle, Localidad, Provincia y Código Postal para guardar la dirección.";
                        pnlMensaje.Visible = true;
                        return;
                    }

                    DireccionNegocio dirNegocio = new DireccionNegocio();
                    int idCliente = dirNegocio.ObtenerOCrearCliente(user, user.Nombre, user.Apellido);

                    string pisoDeptoTexto = txtPisoDepto.Text.Trim();
                    string piso = pisoDeptoTexto;
                    string depto = "";

                    if (pisoDeptoTexto.Contains(" "))
                    {
                        string[] partes = pisoDeptoTexto.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                        if (partes.Length == 2)
                        {
                            piso = partes[0];
                            depto = partes[1];
                        }
                    }

                    Direccion dir = new Direccion
                    {
                        Id = idDirActual,
                        Calle = txtCalle.Text.Trim(),
                        Numero = "S/N",
                        Piso = piso,
                        Departamento = depto,
                        Localidad = txtLocalidad.Text.Trim(),
                        Provincia = txtProvincia.Text.Trim(),
                        CodigoPostal = txtCP.Text.Trim()
                    };

                    dirNegocio.GuardarOActualizar(dir, idCliente);

                    Direccion dirGuardada = dirNegocio.ObtenerPorEmail(user.Email);
                    if (dirGuardada != null)
                    {
                        ViewState["IdDireccionPerfil"] = dirGuardada.Id;
                    }
                }

                pnlMensaje.CssClass = "alert alert-success alert-dismissible fade show";
                lblMensaje.Text = "¡Tus datos y dirección se actualizaron correctamente!";
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