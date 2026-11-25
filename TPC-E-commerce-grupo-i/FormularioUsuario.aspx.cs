using System;
using System.Collections.Generic;
using System.Web.UI;
using dominio;
using negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class FormularioUsuario : Page
    {
        private int IdUsuarioEditar
        {
            get
            {
                if (ViewState["IdUsuarioEditar"] != null)
                    return (int)ViewState["IdUsuarioEditar"];
                return 0;
            }
            set { ViewState["IdUsuarioEditar"] = value; }
        }

        private string PassActual
        {
            get { return ViewState["PassActual"] as string ?? ""; }
            set { ViewState["PassActual"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario adminActual = Session["usuario"] as Usuario;
            if (adminActual == null || !adminActual.Admin)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                int idParam;
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idParam) && idParam > 0)
                {
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    Usuario userDestino = usuarioNegocio.Listar("TODOS").Find(u => u.Id == idParam);
                    if (userDestino == null)
                    {
                        Response.Redirect("UsuarioAdmin.aspx", false);
                        return;
                    }

                    // Si el destino es Admin Maestro y quien está logueado NO es Admin Maestro -> fuera
                    if (userDestino.EsAdminMaestro && !adminActual.EsAdminMaestro)
                    {
                        Response.Redirect("UsuarioAdmin.aspx", false);
                        return;
                    }

                    // Si quien está logueado NO es Admin Maestro y el destino es otro Admin -> fuera
                    if (!adminActual.EsAdminMaestro && userDestino.Admin)
                    {
                        Response.Redirect("UsuarioAdmin.aspx", false);
                        return;
                    }
                    IdUsuarioEditar = idParam;
                    lblTitulo.Text = "Modificar Usuario #" + idParam;
                    lblPassAsterisco.Visible = false;
                    lblAyudaPass.Text = "Dejá en blanco para mantener la contraseña actual.";
                    CargarUsuario(idParam);
                }
                else
                {
                    IdUsuarioEditar = 0;
                    lblTitulo.Text = "Nuevo Usuario";
                    lblPassAsterisco.Visible = true;
                    lblAyudaPass.Text = "Obligatoria al crear un usuario.";
                }

                ActualizarVisibilidadComprador();
            }
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVisibilidadComprador();
        }

        private void ActualizarVisibilidadComprador()
        {
            bool esAdmin = ddlRol.SelectedValue == "1";
            pnlDireccion.Visible = !esAdmin;
            pnlTelefono.Visible = !esAdmin;

            // En tabla Clientes, Nombre y Apellido son NOT NULL; en USERS son NULL.
            // Si es comprador, marcamos Nombre y Apellido como obligatorios en el formulario.
            lblNombreAsterisco.Visible = !esAdmin;
            lblApellidoAsterisco.Visible = !esAdmin;
        }

        private void CargarUsuario(int idUsuario)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            List<Usuario> lista = usuarioNegocio.Listar("TODOS");
            Usuario user = lista.Find(u => u.Id == idUsuario);

            if (user == null)
            {
                Response.Redirect("UsuarioAdmin.aspx", false);
                return;
            }

            txtEmail.Text = user.Email;
            PassActual = user.Pass;
            txtNombre.Text = user.Nombre;
            txtApellido.Text = user.Apellido;
            txtUrlImagen.Text = user.UrlImagenPerfil;
            ddlRol.SelectedValue = user.Admin ? "1" : "0";
            chkActivo.Checked = user.Activo;

            if (!user.Admin)
            {
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                Cliente cli = clienteNegocio.BuscarPorEmail(user.Email);
                if (cli != null && !string.IsNullOrEmpty(cli.Telefono))
                {
                    txtTelefono.Text = cli.Telefono;
                }

                DireccionNegocio direccionNegocio = new DireccionNegocio();
                Direccion dir = direccionNegocio.ObtenerPorEmail(user.Email);
                if (dir != null)
                {
                    hfIdDireccion.Value = dir.Id.ToString();
                    txtCalle.Text = dir.Calle;
                    txtNumero.Text = dir.Numero;
                    txtPiso.Text = dir.Piso;
                    txtDepartamento.Text = dir.Departamento;
                    txtCodigoPostal.Text = dir.CodigoPostal;
                    txtLocalidad.Text = dir.Localidad;
                    txtProvincia.Text = dir.Provincia;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;

            string email = txtEmail.Text.Trim();
            string passIngresada = txtPass.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string urlImagen = txtUrlImagen.Text.Trim();
            bool esAdmin = ddlRol.SelectedValue == "1";
            bool activo = chkActivo.Checked;

            // 1. Validaciones con Validacion.cs
            if (!Validacion.EsEmailValido(txtEmail))
            {
                MostrarError("Ingresá un correo electrónico válido.");
                return;
            }

            if (IdUsuarioEditar == 0 && (Validacion.EstaVacio(txtPass) || passIngresada.Length < 4))
            {
                MostrarError("La contraseña es obligatoria y debe tener al menos 4 caracteres.");
                return;
            }

            if (IdUsuarioEditar > 0 && !Validacion.EstaVacio(txtPass) && passIngresada.Length < 4)
            {
                MostrarError("La nueva contraseña debe tener al menos 4 caracteres.");
                return;
            }

            if (!Validacion.EsUrlOpcionalValida(txtUrlImagen))
            {
                MostrarError("La URL de imagen de perfil debe comenzar con http:// o https://.");
                return;
            }

            // 2. Evitar que el admin logueado se desactive o se quite permisos a sí mismo
            Usuario adminLogueado = Session["usuario"] as Usuario;
            if (adminLogueado != null && adminLogueado.Id == IdUsuarioEditar && (!esAdmin || !activo))
            {
                MostrarError("No podés desactivar tu propia cuenta ni quitarte el rol de Administrador.");
                return;
            }
            if (IdUsuarioEditar > 0 && adminLogueado != null)
            {
                UsuarioNegocio usuarioNegocioValidacion = new UsuarioNegocio();
                Usuario destinoActual = usuarioNegocioValidacion.Listar("TODOS").Find(u => u.Id == IdUsuarioEditar);

                if (destinoActual != null)
                {
                    if (destinoActual.EsAdminMaestro && !adminLogueado.EsAdminMaestro)
                    {
                        MostrarError("Solo el Admin Maestro puede modificar esta cuenta.");
                        return;
                    }

                    if (!adminLogueado.EsAdminMaestro && destinoActual.Admin)
                    {
                        MostrarError("No tenés permisos para modificar a otro administrador.");
                        return;
                    }

                    if (destinoActual.EsAdminMaestro && (!esAdmin || !activo))
                    {
                        MostrarError("La cuenta del Admin Maestro no puede desactivarse ni perder su rol.");
                        return;
                    }
                }
            }
            if (!esAdmin)
            {
                if (Validacion.EstaVacio(txtNombre) || Validacion.EstaVacio(txtApellido))
                {
                    MostrarError("Para un comprador/cliente, Nombre y Apellido son obligatorios.");
                    return;
                }

                if (Validacion.EstaVacio(txtCalle) ||
                    Validacion.EstaVacio(txtNumero) ||
                    Validacion.EstaVacio(txtCodigoPostal) ||
                    Validacion.EstaVacio(txtLocalidad) ||
                    Validacion.EstaVacio(txtProvincia))
                {
                    MostrarError("Completá los campos obligatorios de dirección (Calle, Número, Código Postal, Localidad y Provincia).");
                    return;
                }
            }

            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                string passFinal = !string.IsNullOrWhiteSpace(passIngresada) ? passIngresada : PassActual;

                // 3. Validar email duplicado tanto en Alta como en Edición
                Usuario existente = usuarioNegocio.Listar("TODOS").Find(u =>
                    u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Id != IdUsuarioEditar);
                if (existente != null)
                {
                    MostrarError("Ya existe otro usuario registrado con ese correo electrónico.");
                    return;
                }

                if (IdUsuarioEditar == 0)
                {
                    Usuario nuevo = new Usuario
                    {
                        Email = email,
                        Pass = passFinal,
                        Nombre = nombre,
                        Apellido = apellido,
                        UrlImagenPerfil = urlImagen,
                        Admin = esAdmin,
                        Activo = activo
                    };

                    int idGenerado = usuarioNegocio.Registrar(nuevo);
                    if (!activo && idGenerado > 0)
                    {
                        usuarioNegocio.CambiarEstado(idGenerado, false);
                    }
                    nuevo.Id = idGenerado;

                    if (!esAdmin)
                    {
                        GuardarClienteYDireccion(nuevo, nombre, apellido, telefono, activo);
                    }
                }
                else
                {
                    Usuario editado = new Usuario
                    {
                        Id = IdUsuarioEditar,
                        Email = email,
                        Pass = passFinal,
                        Nombre = nombre,
                        Apellido = apellido,
                        UrlImagenPerfil = urlImagen,
                        Admin = esAdmin,
                        Activo = activo
                    };

                    usuarioNegocio.Actualizar(editado);
                    usuarioNegocio.CambiarRol(IdUsuarioEditar, esAdmin);
                    usuarioNegocio.CambiarEstado(IdUsuarioEditar, activo);

                    // Actualizar Email y Pass en USERS por si fueron modificados desde Admin
                    AccesoDatos datosUser = new AccesoDatos();
                    try
                    {
                        datosUser.setConsulta("UPDATE USERS SET Email = @email, Pass = @pass WHERE Id = @id");
                        datosUser.setParametro("@email", email);
                        datosUser.setParametro("@pass", passFinal);
                        datosUser.setParametro("@id", IdUsuarioEditar);
                        datosUser.ejecutarAccion();
                    }
                    finally
                    {
                        datosUser.cerrarConexion();
                    }

                    if (!esAdmin)
                    {
                        GuardarClienteYDireccion(editado, nombre, apellido, telefono, activo);
                    }
                }
                if (adminLogueado != null && adminLogueado.Id == IdUsuarioEditar)
                {
                    Usuario actualizado = usuarioNegocio.Listar("TODOS").Find(u => u.Id == IdUsuarioEditar);
                    if (actualizado != null)
                    {
                        Session["usuario"] = actualizado;
                    }
                }
                Response.Redirect("UsuarioAdmin.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarError("Error al guardar el usuario: " + ex.Message);
            }
        }

        private void GuardarClienteYDireccion(Usuario user, string nombre, string apellido, string telefono, bool activo)
        {
            DireccionNegocio direccionNegocio = new DireccionNegocio();
            int idCliente = direccionNegocio.ObtenerOCrearCliente(user, nombre, apellido);

            AccesoDatos datosCliente = new AccesoDatos();
            try
            {
                datosCliente.setConsulta("UPDATE Clientes SET Nombre = @nombre, Apellido = @apellido, Telefono = @telefono, Activo = @activo WHERE Id = @id");
                datosCliente.setParametro("@nombre", nombre);
                datosCliente.setParametro("@apellido", apellido);
                datosCliente.setParametro("@telefono", string.IsNullOrWhiteSpace(telefono) ? (object)DBNull.Value : telefono);
                datosCliente.setParametro("@activo", activo);
                datosCliente.setParametro("@id", idCliente);
                datosCliente.ejecutarAccion();
            }
            finally
            {
                datosCliente.cerrarConexion();
            }

            int idDir;
            int.TryParse(hfIdDireccion.Value, out idDir);

            Direccion dir = new Direccion
            {
                Id = idDir,
                Calle = txtCalle.Text.Trim(),
                Numero = txtNumero.Text.Trim(),
                Piso = txtPiso.Text.Trim(),
                Departamento = txtDepartamento.Text.Trim(),
                CodigoPostal = txtCodigoPostal.Text.Trim(),
                Localidad = txtLocalidad.Text.Trim(),
                Provincia = txtProvincia.Text.Trim(),
                Activo = true
            };

            direccionNegocio.GuardarOActualizar(dir, idCliente);
        }

        private void MostrarError(string mensaje)
        {
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }
    }
}