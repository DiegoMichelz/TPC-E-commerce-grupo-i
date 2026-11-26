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
                    dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];

                    if (carrito.Items != null && carrito.Items.Count > 0)
                    {
                        ActualizarTotales();
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
                    dominio.Usuario userLogueado = (dominio.Usuario)Session["usuario"];

                    pnlGuardarDireccion.Visible = true;
                    chkGuardarDireccion.Checked = true;

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
                        chkUsarGuardada.Checked = true;

                        string pisoDepto = FormatearPisoDepto(dirGuardada);
                        string pisoResumen = string.IsNullOrWhiteSpace(pisoDepto) ? "" : $" ({pisoDepto})";
                        lblResumenDireccion.Text = $"{dirGuardada.Calle}{pisoResumen} - {dirGuardada.Localidad}, {dirGuardada.Provincia} (CP {dirGuardada.CodigoPostal})";

                        CargarDireccionEnCampos(dirGuardada);
                    }
                    else
                    {
                        ViewState["IdDireccionGuardada"] = 0;
                    }
                }
            }
        }

        private string FormatearPisoDepto(Direccion dir)
        {
            if (dir == null) return "";
            if (!string.IsNullOrWhiteSpace(dir.Piso) && !string.IsNullOrWhiteSpace(dir.Departamento))
                return $"{dir.Piso} {dir.Departamento}".Trim();
            if (!string.IsNullOrWhiteSpace(dir.Piso))
                return dir.Piso.Trim();
            if (!string.IsNullOrWhiteSpace(dir.Departamento))
                return dir.Departamento.Trim();
            return "";
        }

        private void CargarDireccionEnCampos(Direccion dir)
        {
            txtDireccion.Text = dir.Calle;
            txtPisoDepto.Text = FormatearPisoDepto(dir);
            txtLocalidad.Text = dir.Localidad;
            txtProvincia.Text = dir.Provincia;
            txtCP.Text = dir.CodigoPostal;
        }

        private void LimpiarCamposDireccion()
        {
            txtDireccion.Text = "";
            txtPisoDepto.Text = "";
            txtLocalidad.Text = "";
            txtProvincia.Text = "";
            txtCP.Text = "";
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
                    ViewState["IdDireccionGuardada"] = dirGuardada.Id;
                    CargarDireccionEnCampos(dirGuardada);
                    chkGuardarDireccion.Checked = true;
                }
            }
            else
            {
                // Si destilda la guardada, habilita ingresar una nueva dirección sin sobreescribir la anterior
                ViewState["IdDireccionGuardada"] = 0;
                LimpiarCamposDireccion();
                chkGuardarDireccion.Checked = true;
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                dominio.Carrito carrito = Session["Carrito"] as dominio.Carrito;
                if (carrito == null || carrito.Items == null || carrito.Items.Count == 0)
                {
                    Response.Redirect("Default.aspx", false);
                    return;
                }

                // 1. Validar datos obligatorios de contacto y envío (Piso/Depto queda opcional)
                if (!Validacion.EsEmailValido(txtEmail))
                {
                    lblError.Text = "Ingresá un correo electrónico válido para enviar la factura.";
                    lblError.Visible = true;
                    return;
                }

                if (Validacion.EstaVacio(txtNombre) || Validacion.EstaVacio(txtApellido) ||
                    Validacion.EstaVacio(txtDireccion) || Validacion.EstaVacio(txtLocalidad) ||
                    Validacion.EstaVacio(txtProvincia) || Validacion.EstaVacio(txtCP))
                {
                    lblError.Text = "Completá todos los datos obligatorios de envío (Nombre, Apellido, Dirección, Localidad, Provincia y Código Postal).";
                    lblError.Visible = true;
                    return;
                }

                // 2. Validar stock disponible actualizado en base de datos antes de grabar
                ArticuloNegocio artNegocio = new ArticuloNegocio();
                var catalogoActual = artNegocio.Listar();

                foreach (var item in carrito.Items)
                {
                    var artDB = catalogoActual.Find(x => x.Id == item.Producto.Id);
                    if (artDB == null || artDB.Stock < item.Cantidad)
                    {
                        int disponible = artDB != null ? artDB.Stock : 0;
                        lblError.Text = $"No hay stock suficiente para '{item.Producto.Nombre}'. Disponible actual: {disponible}.";
                        lblError.Visible = true;
                        return;
                    }
                }

                int idCliente = 0;
                int idDireccionGuardada = ViewState["IdDireccionGuardada"] != null ? (int)ViewState["IdDireccionGuardada"] : 0;
                int idDireccionEnvio = idDireccionGuardada;

                if (Session["usuario"] != null)
                {
                    Usuario userLogueado = (Usuario)Session["usuario"];
                    DireccionNegocio dirNegocio = new DireccionNegocio();

                    idCliente = dirNegocio.ObtenerOCrearCliente(userLogueado, txtNombre.Text.Trim(), txtApellido.Text.Trim());

                    if (chkGuardarDireccion.Checked)
                    {
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

                        Direccion dir = new Direccion();
                        dir.Id = idDireccionGuardada; // > 0 actualiza la dirección guardada; 0 crea una nueva dirección
                        dir.Calle = txtDireccion.Text.Trim();
                        dir.Numero = "S/N";
                        dir.Piso = piso;
                        dir.Departamento = depto;
                        dir.Localidad = txtLocalidad.Text.Trim();
                        dir.Provincia = txtProvincia.Text.Trim();
                        dir.CodigoPostal = txtCP.Text.Trim();

                        dirNegocio.GuardarOActualizar(dir, idCliente);

                        Direccion dirActualizada = dirNegocio.ObtenerPorEmail(userLogueado.Email);
                        if (dirActualizada != null)
                        {
                            idDireccionEnvio = dirActualizada.Id;
                            ViewState["IdDireccionGuardada"] = idDireccionEnvio;
                        }
                    }
                    else if (idDireccionEnvio == 0)
                    {
                        Direccion dirExistente = dirNegocio.ObtenerPorEmail(userLogueado.Email);
                        if (dirExistente != null)
                        {
                            idDireccionEnvio = dirExistente.Id;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(userLogueado.Nombre) || string.IsNullOrWhiteSpace(userLogueado.Apellido))
                    {
                        userLogueado.Nombre = txtNombre.Text.Trim();
                        userLogueado.Apellido = txtApellido.Text.Trim();
                        UsuarioNegocio userNegocio = new UsuarioNegocio();
                        userNegocio.Actualizar(userLogueado);
                        Session["usuario"] = userLogueado;
                    }
                }

                // 3. Total y medio de pago
                decimal totalFinal = carrito.TotalConEnvio;
                int idFormaPago = rbTransferencia.Checked ? 1 : 2;
                if (rbTransferencia.Checked)
                {
                    totalFinal = totalFinal * 0.90m;
                }

                // 4. Armar y guardar pedido
                dominio.Pedido nuevoPedido = new dominio.Pedido();
                nuevoPedido.Cliente = new dominio.Cliente { Id = idCliente };
                nuevoPedido.Fecha = DateTime.Now;
                nuevoPedido.Estado = "Pendiente";
                nuevoPedido.Total = totalFinal;
                nuevoPedido.FormaPago = new dominio.FormaPago { Id = idFormaPago };
                nuevoPedido.FormaEntrega = new dominio.FormaEntrega { Id = 2 };

                if (idDireccionEnvio > 0)
                {
                    nuevoPedido.DireccionEnvio = new dominio.Direccion { Id = idDireccionEnvio };
                }

                foreach (var itemCarrito in carrito.Items)
                {
                    dominio.ItemPedido itemPedido = new dominio.ItemPedido();
                    itemPedido.Producto = itemCarrito.Producto;
                    itemPedido.Cantidad = itemCarrito.Cantidad;
                    itemPedido.PrecioUnitario = itemCarrito.Producto.Precio;
                    nuevoPedido.Elementos.Add(itemPedido);
                }

                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                pedidoNegocio.Agregar(nuevoPedido);

                Session.Remove("Carrito");
                Response.Redirect("Confirmacion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
            }
        }

        private void ActualizarTotales()
        {
            if (Session["Carrito"] != null)
            {
                dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];

                lblSubtotal.Text = carrito.Total.ToString("N2");

                if (carrito.CostoEnvio == 0m)
                {
                    lblEnvioCheckout.Text = "GRATIS";
                    lblEnvioCheckout.CssClass = "text-success fw-semibold";
                }
                else
                {
                    lblEnvioCheckout.Text = "$" + carrito.CostoEnvio.ToString("N2");
                    lblEnvioCheckout.CssClass = "fw-semibold text-dark";
                }

                decimal totalFinal = carrito.TotalConEnvio;

                if (rbTransferencia.Checked)
                {
                    totalFinal = totalFinal * 0.90m;
                }

                lblTotal.Text = totalFinal.ToString("N2");
            }
        }

        protected void MedioPago_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTotales();
        }
    }
}