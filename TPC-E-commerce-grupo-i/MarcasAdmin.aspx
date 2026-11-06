<%@ Page Title="Gestión de Marcas" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="MarcasAdmin.aspx.cs" Inherits="TPC_E_commerce_grupo_i.MarcasAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Administración de Marcas</h2>
        
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
                <asp:GridView ID="dgvMarcas" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                    DataKeyNames="Id" OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="Id" />
                        <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
                        <asp:CommandField ShowSelectButton="true" SelectText="✏️ Editar" ControlStyle-CssClass="btn btn-sm btn-warning" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>