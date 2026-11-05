<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12 d-flex justify-content-between align-items-center mb-3">
                <h2>Panel de Administración de Artículos</h2>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nuevo Artículo" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <!-- Grilla dinámica de ASP.NET -->
                        <asp:GridView ID="dgvArticulos" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false"
                            DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" OnPageIndexChanging="dgvArticulos_PageIndexChanging" AllowPaging="true" PageSize="5">
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

                                <asp:CommandField ShowSelectButton="true" SelectText="✏️ Modificar" ControlStyle-CssClass="btn btn-sm btn-warning" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
