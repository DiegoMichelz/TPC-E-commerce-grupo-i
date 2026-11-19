<%@ Page Title="Gestión de Usuarios" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="UsuariosAdmin.aspx.cs" Inherits="TPC_E_commerce_grupo_i.UsuariosAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <!-- Enlaces del Panel de Administración -->
        <div class="row mb-3">
            <div class="col-12 d-flex flex-wrap justify-content-between align-items-center gap-2">
                <h2 class="mb-0"><i class="bi bi-people-fill text-primary me-2"></i>Gestión de Usuarios</h2>
                <div class="d-flex flex-wrap gap-2">
                    <a href="Administracion.aspx" class="btn btn-outline-dark"><i class="bi bi-box me-1"></i>Gestionar Artículos</a>
                    <a href="PedidosAdmin.aspx" class="btn btn-outline-dark"><i class="bi bi-box-seam me-1"></i>Gestionar Pedidos</a>
                    <a href="MarcasAdmin.aspx" class="btn btn-outline-dark"><i class="bi bi-tags me-1"></i>Gestionar Marcas</a>
                    <a href="CategoriasAdmin.aspx" class="btn btn-outline-dark"><i class="bi bi-grid me-1"></i>Gestionar Categorías</a>
                    <a href="UsuarioAdmin.aspx" class="btn btn-dark"><i class="bi bi-people me-1"></i>Gestionar Usuarios</a>
                </div>
            </div>
        </div>

        <!-- Panel de Filtro por Estado y Alta de Usuario -->
        <div class="card shadow-sm mb-3">
            <div class="card-body py-2 d-flex flex-wrap align-items-center justify-content-between gap-2">
                <div class="d-flex align-items-center gap-2">
                    <label class="fw-semibold small mb-0">Filtrar por Estado:</label>
                    <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-select form-select-sm w-auto" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                        <asp:ListItem Text="Solo Activos" Value="ACTIVOS" Selected="True" />
                        <asp:ListItem Text="Solo Inactivos / Bloqueados" Value="INACTIVOS" />
                        <asp:ListItem Text="Todos los Usuarios" Value="TODOS" />
                    </asp:DropDownList>
                </div>

                <div>
                    <a href="FormularioUsuario.aspx" class="btn btn-sm btn-primary">
                        <i class="bi bi-person-plus me-1"></i>Nuevo usuario
                    </a>
                </div>
            </div>
        </div>

        <!-- Panel de Detalle del Usuario Seleccionado -->
        <asp:Panel ID="pnlDetalleUsuario" runat="server" Visible="false" CssClass="card shadow-sm mb-3 border-info">
            <div class="card-header bg-info text-dark d-flex justify-content-between align-items-center">
                <strong><i class="bi bi-person-lines-fill me-1"></i>Detalle del Usuario</strong>
                <asp:LinkButton ID="btnCerrarDetalle" runat="server" CssClass="btn btn-sm btn-outline-dark" OnClick="btnCerrarDetalle_Click">
                    Cerrar ✕
                </asp:LinkButton>
            </div>
            <div class="card-body">
                <div class="row g-2">
                    <div class="col-md-4"><strong>Nombre y Apellido:</strong> <asp:Label ID="lblDetNombre" runat="server" /></div>
                    <div class="col-md-4"><strong>Email:</strong> <asp:Label ID="lblDetEmail" runat="server" /></div>
                    <div class="col-md-4"><strong>Rol / Estado:</strong> <asp:Label ID="lblDetRolEstado" runat="server" /></div>
                    <div class="col-md-4"><strong>Teléfono:</strong> <asp:Label ID="lblDetTelefono" runat="server" /></div>
                    <div class="col-md-8"><strong>Dirección:</strong> <asp:Label ID="lblDetDireccion" runat="server" /></div>
                </div>
            </div>
        </asp:Panel>

        <!-- Grilla de Usuarios -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <asp:GridView ID="dgvUsuarios" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="false"
                            DataKeyNames="Id" OnRowCommand="dgvUsuarios_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="Id" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
                                <asp:BoundField HeaderText="Email" DataField="Email" />
                                
                                <asp:TemplateField HeaderText="Rol">
                                    <ItemTemplate>
                                        <%# (bool)Eval("Admin") ? "<span class='badge bg-primary'>Administrador</span>" : "<span class='badge bg-secondary'>Comprador</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <%# (bool)Eval("Activo") ? "<span class='badge bg-success'>Activo</span>" : "<span class='badge bg-danger'>Inactivo / Bloqueado</span>" %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <!-- Ver Detalle -->
                                        <asp:LinkButton ID="btnVerDetalle" runat="server"
                                            CssClass="btn btn-sm btn-info text-dark me-1"
                                            CommandName="VerDetalle" CommandArgument='<%# Eval("Id") %>'>
                                            👁️ Detalle
                                        </asp:LinkButton>

                                        <!-- Modificar Usuario -->
                                        <asp:LinkButton ID="btnModificar" runat="server"
                                            CssClass="btn btn-sm btn-warning me-1"
                                            CommandName="Modificar" CommandArgument='<%# Eval("Id") %>'>
                                            ✏️ Modificar
                                        </asp:LinkButton>

                                        <!-- Promoción / Reversión de Rol Admin -->
                                        <asp:LinkButton ID="btnToggleAdmin" runat="server" 
                                            CssClass='<%# (bool)Eval("Admin") ? "btn btn-sm btn-outline-secondary me-1" : "btn btn-sm btn-outline-primary me-1" %>'
                                            CommandName="ToggleAdmin" CommandArgument='<%# Eval("Id") %>'>
                                            <%# (bool)Eval("Admin") ? "Quitar Admin" : "Hacer Admin" %>
                                        </asp:LinkButton>

                                        <!-- Inactivar / Bloquear (si está activo) -->
                                        <asp:LinkButton ID="btnInactivar" runat="server" 
                                            CssClass="btn btn-sm btn-danger me-1" 
                                            CommandName="Inactivar" CommandArgument='<%# Eval("Id") %>'
                                            Visible='<%# (bool)Eval("Activo") %>'>
                                            🚫 Bloquear
                                        </asp:LinkButton>

                                        <!-- Reactivar (si está inactivo) -->
                                        <asp:LinkButton ID="btnReactivar" runat="server" 
                                            CssClass="btn btn-sm btn-success" 
                                            CommandName="Reactivar" CommandArgument='<%# Eval("Id") %>'
                                            Visible='<%# !(bool)Eval("Activo") %>'>
                                            ♻️ Reactivar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>