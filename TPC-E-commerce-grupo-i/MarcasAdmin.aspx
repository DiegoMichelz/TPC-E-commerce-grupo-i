<%@ Page Title="Gestión de Marcas" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="MarcasAdmin.aspx.cs" Inherits="TPC_E_commerce_grupo_i.MarcasAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <h2 class="mb-0">Administración de Marcas</h2>
            <a href="Administracion.aspx" class="btn btn-outline-secondary">
                <i class="bi bi-arrow-left me-1"></i>Volver a Artículos
            </a>
        </div>

        <!-- Cartel de aviso / error Bootstrap -->
        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-danger alert-dismissible fade show" role="alert">
            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
        </asp:Panel>

        <!-- Panel de confirmación de baja Bootstrap -->
        <asp:Panel ID="pnlConfirmarBaja" runat="server" Visible="false" CssClass="alert alert-warning border-warning d-flex justify-content-between align-items-center mb-3">
            <div>
                <i class="bi bi-exclamation-triangle-fill me-2"></i>
                <strong>¿Está seguro de que desea eliminar esta marca?</strong>
            </div>
            <div>
                <asp:Button ID="btnConfirmarBaja" runat="server" Text="Sí, eliminar" CssClass="btn btn-danger btn-sm me-2" OnClick="btnConfirmarBaja_Click" />
                <asp:Button ID="btnCancelarBaja" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnCancelarBaja_Click" />
            </div>
        </asp:Panel>

        <div class="row mt-3 mb-3">
            <div class="col-md-6">
                <div class="input-group">
                    <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="form-control" Placeholder="Nombre de la marca"></asp:TextBox>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" Visible="false" />
                </div>
                <asp:HiddenField ID="hfIdMarca" runat="server" Value="0" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-8">
                <asp:GridView ID="dgvMarcas" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="false"
                    DataKeyNames="Id" OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged" OnRowCommand="dgvMarcas_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="Id" />
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-warning me-1" CommandName="Select">
                                    ✏️ Editar
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>'>
                                    🗑️ Eliminar
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>