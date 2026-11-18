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
                dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];
                if (carrito == null) return;

                int idCliente = 0;

                if (Session["usuario"] != null)
                {
                    Usuario userLogueado = (Usuario)Session["usuario"];
                    DireccionNegocio dirNegocio = new DireccionNegocio();

                    idCliente = dirNegocio.ObtenerOCrearCliente(userLogueado, txtNombre.Text.Trim(), txtApellido.Text.Trim());
                    if (chkGuardarDireccion.Checked)
                    { 
                        Direccion dir = new Direccion();
                        dir.Id = ViewState["IdDireccionGuardada"] != null ? (int)ViewState["IdDireccionGuardada"] : 0;
                        dir.Calle = txtDireccion.Text.Trim();
                        dir.Numero = "S/N";
                        dir.Piso = txtPisoDepto.Text.Trim();
                        dir.Localidad = txtLocalidad.Text.Trim();
                        dir.Provincia = txtProvincia.Text.Trim();
                        dir.CodigoPostal = txtCP.Text.Trim();

                        dirNegocio.GuardarOActualizar(dir, idCliente);
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
                // 1. OBTENEMOS EL TOTAL Y VEMOS QUÉ MEDIO DE PAGO ELIGIÓ
                decimal totalFinal = carrito.Total;
                int idFormaPago = 2; // Por defecto Tarjeta

                if (rbTransferencia.Checked)
                {
                    idFormaPago = 1; // ID 1 en tu SQL ('Efectivo / Transferencia')
                    totalFinal = totalFinal * 0.90m; // ¡Descuento del 10%!
                }
                else if (rbTarjeta.Checked)
                {
                    idFormaPago = 2; // ID 2 en tu SQL ('Tarjeta de Crédito / Débito')
                }

                // 2. ARMAMOS EL OBJETO PEDIDO PARA GUARDARLO EN LA BASE DE DATOS
                dominio.Pedido nuevoPedido = new dominio.Pedido();
                nuevoPedido.Cliente = new dominio.Cliente { Id = idCliente };
                nuevoPedido.Fecha = DateTime.Now;
                nuevoPedido.Estado = "Pendiente";
                nuevoPedido.Total = totalFinal; // Ya viene con el descuento aplicado si fue transferencia

                nuevoPedido.FormaPago = new dominio.FormaPago { Id = idFormaPago };
                nuevoPedido.FormaEntrega = new dominio.FormaEntrega { Id = 1 }; // Envío a domicilio por defecto

                // 3. CARGAMOS LOS ÍTEMS DEL CARRITO AL PEDIDO (Usando 'Elementos' como pide tu clase Pedido)
                foreach (var itemCarrito in carrito.Items)
                {
                    dominio.ItemPedido itemPedido = new dominio.ItemPedido();
                    itemPedido.Producto = itemCarrito.Producto; // Usando 'Producto' de tu ItemCarrito
                    itemPedido.Cantidad = itemCarrito.Cantidad;
                    itemPedido.PrecioUnitario = itemCarrito.Producto.Precio;

                    nuevoPedido.Elementos.Add(itemPedido);
                }

                // 4. GUARDAMOS EL PEDIDO MEDIANTE EL NEGOCIO
                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                pedidoNegocio.Agregar(nuevoPedido);

                // 5. VACIAR CARRITO Y REDIRIGIR
                Session.Remove("Carrito");
                Response.Redirect("Confirmacion.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Método para recalcular el subtotal y total según el medio de pago elegido
        private void ActualizarTotales()
        {
            if (Session["Carrito"] != null)
            {
                dominio.Carrito carrito = (dominio.Carrito)Session["Carrito"];
                decimal totalFinal = carrito.Total;

                // Si eligió transferencia, aplicamos el 10% de descuento
                if (rbTransferencia.Checked)
                {
                    totalFinal = totalFinal * 0.90m;
                }

                lblSubtotal.Text = carrito.Total.ToString("N2");
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