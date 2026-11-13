using System;
using System.Web.UI;
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
            int id = Convert.ToInt32(dgvMarcas.SelectedDataKey.Value);
            string descripcion = dgvMarcas.SelectedRow.Cells[1].Text;

            hfIdMarca.Value = id.ToString();
            txtNuevaMarca.Text = descripcion;
            btnGuardar.Text = "Actualizar";
            btnCancelar.Visible = true;
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