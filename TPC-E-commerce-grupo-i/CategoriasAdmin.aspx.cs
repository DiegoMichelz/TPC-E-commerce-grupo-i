using System;
using System.Web.UI;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class CategoriasAdmin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            int id = Convert.ToInt32(dgvCategorias.SelectedDataKey.Value);
            string descripcion = dgvCategorias.SelectedRow.Cells[1].Text;

            hfIdCategoria.Value = id.ToString();
            txtNuevaCategoria.Text = descripcion;
            btnGuardar.Text = "Actualizar";
            btnCancelar.Visible = true;
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