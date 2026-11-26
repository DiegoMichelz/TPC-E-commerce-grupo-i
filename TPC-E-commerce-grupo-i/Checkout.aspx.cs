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
                int idDireccionEnvio = ViewState["IdDireccionGuardada"] != null ? (int)ViewState["IdDireccionGuardada"] : 0;

                if (Session["usuario"] != null)
                {
                    Usuario userLogueado = (Usuario)Session["usuario"];
                    DireccionNegocio dirNegocio = new DireccionNegocio();

                    idCliente = dirNegocio.ObtenerOCrearCliente(userLogueado, txtNombre.Text.Trim(), txtApellido.Text.Trim());

                    if (idDireccionEnvio == 0)
                    {
                        Direccion dirExistente = dirNegocio.ObtenerPorEmail(userLogueado.Email);
                        if (dirExistente != null)
                        {
                            idDireccionEnvio = dirExistente.Id;
                        }
                    }

                    if (chkGuardarDireccion.Checked)
                    {
                        Direccion dir = new Direccion();
                        dir.Id = idDireccionEnvio;
                        dir.Calle = txtDireccion.Text.Trim();
                        dir.Numero = "S/N";
                        dir.Piso = txtPisoDepto.Text.Trim();
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
        // Método para recalcular el subtotal y total según el medio de pago elegido
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

                // 10% de descuento por transferencia sobre toda la operación (productos + envío)
                if (rbTransferencia.Checked)
                {
                    totalFinal = totalFinal * 0.90m;
                }

                lblTotal.Text = totalFinal.ToString("N2");
            }
        }

        // El evento que reclama el botón para actualizar la pantalla al instante
        protected void MedioPago_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTotales();
        }
    }
}