using System;
using System.Collections.Generic;
using System.Web.UI;
using dominio;
using negocio;

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
        protected global::System.Web.UI.WebControls.Panel pnlConfirmarBaja;
        protected global::System.Web.UI.WebControls.Label lblError;

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
                lblError.Visible = false;

                if (Validacion.EstaVacio(txtCodigo) || Validacion.EstaVacio(txtNombre))
                {
                    lblError.Text = "El código y el nombre del producto son obligatorios.";
                    lblError.Visible = true;
                    return;
                }

                if (!Validacion.EsPrecioValido(txtPrecio, out decimal precio))
                {
                    lblError.Text = "Ingresá un precio numérico válido mayor a 0.";
                    lblError.Visible = true;
                    return;
                }

                if (!Validacion.EsEnteroNoNegativo(txtStock, out int stock))
                {
                    lblError.Text = "El stock debe ser un número entero igual o mayor a 0.";
                    lblError.Visible = true;
                    return;
                }

                if (!Validacion.TieneSeleccion(ddlMarca) || !Validacion.TieneSeleccion(ddlCategoria))
                {
                    lblError.Text = "Seleccioná una marca y una categoría válidas.";
                    lblError.Visible = true;
                    return;
                }

                if (!Validacion.EsUrlOpcionalValida(txtImagenUrl))
                {
                    lblError.Text = "La URL de la imagen debe comenzar con http:// o https://.";
                    lblError.Visible = true;
                    return;
                }

                int idActual = int.Parse(hfIdArticulo.Value);
                string codigoIngresado = txtCodigo.Text.Trim();

                ArticuloNegocio negocio = new ArticuloNegocio();

                // Validar código duplicado (excluyendo el propio artículo si estamos editando)
                bool codigoDuplicado = negocio.Listar().Exists(x =>
                    x.Codigo != null &&
                    x.Codigo.Trim().Equals(codigoIngresado, StringComparison.OrdinalIgnoreCase) &&
                    x.Id != idActual);

                if (codigoDuplicado)
                {
                    lblError.Text = $"Ya existe otro artículo registrado con el código '{codigoIngresado}'.";
                    lblError.Visible = true;
                    return;
                }

                Articulo nuevo = new Articulo();
                nuevo.Id = idActual;
                nuevo.Codigo = codigoIngresado;
                nuevo.Nombre = txtNombre.Text.Trim();
                nuevo.Descripcion = txtDescripcion.Text.Trim();
                nuevo.Precio = precio;
                nuevo.Stock = stock;

                nuevo.Marca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };
                nuevo.Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                if (!string.IsNullOrWhiteSpace(txtImagenUrl.Text))
                {
                    nuevo.ImagenesUrl = new List<string> { txtImagenUrl.Text.Trim() };
                }

                if (nuevo.Id == 0)
                    negocio.Agregar(nuevo);
                else
                    negocio.Modificar(nuevo);

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
            pnlConfirmarBaja.Visible = true;
        }

        protected void btnConfirmarBaja_Click(object sender, EventArgs e)
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

        protected void btnCancelarBaja_Click(object sender, EventArgs e)
        {
            pnlConfirmarBaja.Visible = false;
        }
    }
}