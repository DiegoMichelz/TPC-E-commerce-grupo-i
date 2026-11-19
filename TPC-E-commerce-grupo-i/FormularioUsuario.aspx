<%@ Page Title="Formulario de Usuario" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="FormularioUsuario.aspx.cs" Inherits="TPC_E_commerce_grupo_i.FormularioUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4 mb-5">

        <div class="row mb-3">
            <div class="col-12 d-flex justify-content-between align-items-center">
                <h2 class="mb-0">
                    <i class="bi bi-person-gear text-primary me-2"></i>
                    <asp:Label ID="lblTitulo" runat="server" Text="Nuevo Usuario"></asp:Label>
                </h2>
                <a href="UsuarioAdmin.aspx" class="btn btn-outline-secondary">
                    <i class="bi bi-arrow-left me-1"></i>Volver a Gestión de Usuarios
                </a>
            </div>
        </div>

        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-danger" role="alert">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </asp:Panel>

        <div class="card shadow-sm mb-4">
            <div class="card-header bg-dark text-white">
                <strong>Datos de Cuenta y Rol</strong>
            </div>
            <div class="card-body">
                <div class="row g-3">

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">Rol del Usuario *</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlRol_SelectedIndexChanged">
                            <asp:ListItem Text="Comprador / Cliente" Value="0" Selected="True" />
                            <asp:ListItem Text="Administrador" Value="1" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">Email *</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="100" placeholder="correo@ejemplo.com"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">
                            Contraseña <asp:Label ID="lblPassAsterisco" runat="server" Text="*"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" MaxLength="100" placeholder="Contraseña"></asp:TextBox>
                        <small class="text-muted">
                            <asp:Label ID="lblAyudaPass" runat="server" Text="Obligatoria al crear un usuario." Visible="true"></asp:Label>
                        </small>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">
                            Nombre <asp:Label ID="lblNombreAsterisco" runat="server" Text="*"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">
                            Apellido <asp:Label ID="lblApellidoAsterisco" runat="server" Text="*"></asp:Label>
                        </label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </div>

                    <asp:Panel ID="pnlTelefono" runat="server" CssClass="col-md-4">
                        <label class="form-label fw-semibold">Teléfono (Opcional)</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" MaxLength="30" placeholder="Ej: 1155667788"></asp:TextBox>
                    </asp:Panel>

                    <div class="col-md-8">
                        <label class="form-label fw-semibold">URL Imagen de Perfil (Opcional)</label>
                        <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" MaxLength="1000" placeholder="https://..."></asp:TextBox>
                    </div>

                    <div class="col-md-4 d-flex align-items-end">
                        <div class="form-check">
                            <asp:CheckBox ID="chkActivo" runat="server" CssClass="form-check-input" Checked="true" />
                            <label class="form-check-label fw-semibold" for="<%= chkActivo.ClientID %>">
                                Usuario Activo
                            </label>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <!-- Sección de Dirección de Envío para Compradores -->
        <asp:Panel ID="pnlDireccion" runat="server" CssClass="card shadow-sm mb-4">
            <div class="card-header bg-primary text-white">
                <strong>Dirección de Envío (Comprador)</strong>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfIdDireccion" runat="server" Value="0" />
                <div class="row g-3">

                    <div class="col-md-6">
                        <label class="form-label fw-semibold">Calle *</label>
                        <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label class="form-label fw-semibold">Número *</label>
                        <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label class="form-label fw-semibold">Piso</label>
                        <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label class="form-label fw-semibold">Depto</label>
                        <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <label class="form-label fw-semibold">Código Postal *</label>
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <label class="form-label fw-semibold">Localidad *</label>
                        <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </div>

                    <div class="col-md-5">
                        <label class="form-label fw-semibold">Provincia *</label>
                        <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </div>

                </div>
            </div>
        </asp:Panel>

        <div class="d-flex justify-content-end gap-2">
            <a href="UsuarioAdmin.aspx" class="btn btn-secondary">Cancelar</a>
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Usuario" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
        </div>

    </div>
</asp:Content>