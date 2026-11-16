using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class MarcasAdmin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarcas.DataSource = negocio.Listar();
            dgvMarcas.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNuevaMarca.Text))
                return;

            MarcaNegocio negocio = new MarcaNegocio();
            int id = int.Parse(hfIdMarca.Value);

            if (id == 0)
            {
                Marca nueva = new Marca { Descripcion = txtNuevaMarca.Text.Trim() };
                negocio.Agregar(nueva);
            }
            else
            {
                Marca aux = new Marca { Id = id, Descripcion = txtNuevaMarca.Text.Trim() };
                negocio.Modificar(aux);
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;
            pnlConfirmarBaja.Visible = false;

            int id = Convert.ToInt32(dgvMarcas.SelectedDataKey.Value);
            string descripcion = HttpUtility.HtmlDecode(dgvMarcas.SelectedRow.Cells[1].Text);

            hfIdMarca.Value = id.ToString();
            txtNuevaMarca.Text = descripcion;
            btnGuardar.Text = "Actualizar";
            btnCancelar.Visible = true;
        }

        protected void dgvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                pnlMensaje.Visible = false;
                ViewState["IdMarcaEliminar"] = Convert.ToInt32(e.CommandArgument);
                pnlConfirmarBaja.Visible = true;
            }
        }

        protected void btnConfirmarBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdMarcaEliminar"] != null)
                {
                    int idMarca = (int)ViewState["IdMarcaEliminar"];
                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.Eliminar(idMarca);

                    ViewState["IdMarcaEliminar"] = null;
                    pnlConfirmarBaja.Visible = false;
                    LimpiarFormulario();
                    CargarGrilla();
                }
            }
            catch (SqlException ex)
            {
                pnlConfirmarBaja.Visible = false;
                pnlMensaje.Visible = true;
                if (ex.Number == 547)
                    lblMensaje.Text = "No se puede eliminar esta marca porque tiene artículos asociados.";
                else
                    lblMensaje.Text = "Ocurrió un error al intentar eliminar la marca.";
            }
        }

        protected void btnCancelarBaja_Click(object sender, EventArgs e)
        {
            ViewState["IdMarcaEliminar"] = null;
            pnlConfirmarBaja.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfIdMarca.Value = "0";
            txtNuevaMarca.Text = "";
            btnGuardar.Text = "Guardar";
            btnCancelar.Visible = false;
        }
    }
}