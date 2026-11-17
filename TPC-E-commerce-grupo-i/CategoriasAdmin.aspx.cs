using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class CategoriasAdmin : Page
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
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.Listar();
            dgvCategorias.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;

            if (string.IsNullOrWhiteSpace(txtNuevaCategoria.Text))
                return;

            CategoriaNegocio negocio = new CategoriaNegocio();
            int id = int.Parse(hfIdCategoria.Value);

            if (id == 0)
            {
                Categoria nueva = new Categoria { Descripcion = txtNuevaCategoria.Text.Trim() };
                negocio.Agregar(nueva);
            }
            else
            {
                Categoria aux = new Categoria { Id = id, Descripcion = txtNuevaCategoria.Text.Trim() };
                negocio.Modificar(aux);
            }

            LimpiarFormulario();
            CargarGrilla();
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;
            pnlConfirmarBaja.Visible = false;

            int id = Convert.ToInt32(dgvCategorias.SelectedDataKey.Value);
            string descripcion = HttpUtility.HtmlDecode(dgvCategorias.SelectedRow.Cells[1].Text);

            hfIdCategoria.Value = id.ToString();
            txtNuevaCategoria.Text = descripcion;
            btnGuardar.Text = "Actualizar";
            btnCancelar.Visible = true;
        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                pnlMensaje.Visible = false;
                ViewState["IdCategoriaEliminar"] = Convert.ToInt32(e.CommandArgument);
                pnlConfirmarBaja.Visible = true;
            }
        }

        protected void btnConfirmarBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdCategoriaEliminar"] != null)
                {
                    int idCategoria = (int)ViewState["IdCategoriaEliminar"];
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.Eliminar(idCategoria);

                    ViewState["IdCategoriaEliminar"] = null;
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
                    lblMensaje.Text = "No se puede eliminar esta categoría porque tiene artículos asociados.";
                else
                    lblMensaje.Text = "Ocurrió un error al intentar eliminar la categoría.";
            }
        }

        protected void btnCancelarBaja_Click(object sender, EventArgs e)
        {
            ViewState["IdCategoriaEliminar"] = null;
            pnlConfirmarBaja.Visible = false;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfIdCategoria.Value = "0";
            txtNuevaCategoria.Text = "";
            btnGuardar.Text = "Guardar";
            btnCancelar.Visible = false;
        }
    }
}