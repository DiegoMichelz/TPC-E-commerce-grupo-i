using System;
using System.Collections.Generic;
using System.Web.UI;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        protected global::System.Web.UI.WebControls.Label lblTitulo;
        protected global::System.Web.UI.WebControls.HiddenField hfIdArticulo;
        protected global::System.Web.UI.WebControls.TextBox txtCodigo;
        protected global::System.Web.UI.WebControls.TextBox txtNombre;
        protected global::System.Web.UI.WebControls.TextBox txtPrecio;
        protected global::System.Web.UI.WebControls.TextBox txtStock;
        protected global::System.Web.UI.WebControls.DropDownList ddlMarca;
        protected global::System.Web.UI.WebControls.DropDownList ddlCategoria;
        protected global::System.Web.UI.WebControls.TextBox txtImagenUrl;
        protected global::System.Web.UI.WebControls.TextBox txtDescripcion;
        protected global::System.Web.UI.WebControls.Button btnGuardar;
        protected global::System.Web.UI.WebControls.Button btnEliminar;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDesplegables();

                // Si viene un ID por URL, cargamos para MODIFICAR
                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr))
                {
                    int id = int.Parse(idStr);
                    CargarArticuloParaEditar(id);
                }
            }
        }

        private void CargarDesplegables()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio catNegocio = new CategoriaNegocio();

            ddlMarca.DataSource = marcaNegocio.Listar();
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

            ddlCategoria.DataSource = catNegocio.Listar();
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
        }

        private void CargarArticuloParaEditar(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> lista = negocio.Listar();
            Articulo seleccionado = lista.Find(x => x.Id == id);

            if (seleccionado != null)
            {
                lblTitulo.Text = "Modificar Artículo #" + seleccionado.Id;
                hfIdArticulo.Value = seleccionado.Id.ToString();
                txtCodigo.Text = seleccionado.Codigo;
                txtNombre.Text = seleccionado.Nombre;
                txtPrecio.Text = seleccionado.Precio.ToString("0.00").Replace(',', '.');
                txtStock.Text = seleccionado.Stock.ToString();
                txtDescripcion.Text = seleccionado.Descripcion;

                if (seleccionado.Marca != null)
                    ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();

                if (seleccionado.Categoria != null)
                    ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();

                if (seleccionado.ImagenesUrl != null && seleccionado.ImagenesUrl.Count > 0)
                    txtImagenUrl.Text = seleccionado.ImagenesUrl[0];
                btnEliminar.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                nuevo.Id = int.Parse(hfIdArticulo.Value);
                nuevo.Codigo = txtCodigo.Text.Trim();
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Descripcion = txtDescripcion.Text.Trim();
                nuevo.Precio = decimal.Parse(txtPrecio.Text.Replace('.', ','));
                nuevo.Stock = int.Parse(txtStock.Text);

                nuevo.Marca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };
                nuevo.Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                if (!string.IsNullOrWhiteSpace(txtImagenUrl.Text))
                {
                    nuevo.ImagenesUrl = new List<string> { txtImagenUrl.Text.Trim() };
                }

                ArticuloNegocio negocio = new ArticuloNegocio();

                if (nuevo.Id == 0)
                {
                    negocio.Agregar(nuevo);
                }
                else
                {
                    negocio.Modificar(nuevo);
                }

                Response.Redirect("Administracion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(hfIdArticulo.Value);
                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.EliminarLogico(id);

                Response.Redirect("Administracion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}