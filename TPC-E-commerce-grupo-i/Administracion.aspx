<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12 d-flex flex-wrap justify-content-between align-items-center gap-2 mb-3">
                <h2 class="mb-0">Panel de Administración de Artículos</h2>
                <div class="d-flex gap-2">
                    <a href="MarcasAdmin.aspx" class="btn btn-outline-dark">
                        <i class="bi bi-tags me-1"></i>Gestionar Marcas
                    </a>
                    <a href="CategoriasAdmin.aspx" class="btn btn-outline-dark">
                        <i class="bi bi-grid me-1"></i>Gestionar Categorías
                    </a>
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo Artículo" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <!-- Panel de confirmación Bootstrap -->
                        <asp:Panel ID="pnlConfirmarBaja" runat="server" Visible="false" CssClass="alert alert-warning border-warning d-flex justify-content-between align-items-center mb-3">
                            <div>
                                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                                <strong>¿Está seguro de que desea dar de baja este artículo?</strong>
                            </div>
                            <div>
                                <asp:Button ID="btnConfirmarBaja" runat="server" Text="Sí, dar de baja" CssClass="btn btn-danger btn-sm me-2" OnClick="btnConfirmarBaja_Click" />
                                <asp:Button ID="btnCancelarBaja" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnCancelarBaja_Click" />
                            </div>
                        </asp:Panel>

                        <!-- Grilla dinámica de ASP.NET -->
                        <asp:GridView ID="dgvArticulos" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="false"
                            DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" OnRowCommand="dgvArticulos_RowCommand" OnPageIndexChanging="dgvArticulos_PageIndexChanging" AllowPaging="true" PageSize="5">
                            <Columns>
                                <asp:BoundField HeaderText="ID" DataField="Id" />
                                <asp:BoundField HeaderText="Código" DataField="Codigo" />
                                <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                                <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:C}" />
                                <asp:TemplateField HeaderText="Marca">
                                    <ItemTemplate>
                                        <%# Eval("Marca") != null ? Eval("Marca.Descripcion") : "Sin Marca" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Categoría">
                                    <ItemTemplate>
                                        <%# Eval("Categoria") != null ? Eval("Categoria.Descripcion") : "Sin Categoría" %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <a href='<%# "FormularioArticulo.aspx?id=" + Eval("Id") %>' class="btn btn-sm btn-warning me-1">
                                            ✏️ Modificar
                                        </a>
                                        <asp:LinkButton ID="btnEliminarGrilla" runat="server" 
                                            CssClass="btn btn-sm btn-danger" 
                                            CommandName="Eliminar" 
                                            CommandArgument='<%# Eval("Id") %>'>
                                            🗑️ Eliminar</asp:LinkButton>
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