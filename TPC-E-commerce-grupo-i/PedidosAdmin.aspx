<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="PedidosAdmin.aspx.cs" Inherits="TPC_E_commerce_grupo_i.PedidosAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <!-- Barra de navegación interna -->
        <div class="row">
            <div class="col-12 d-flex flex-wrap justify-content-between align-items-center gap-2 mb-3">
                <h2 class="mb-0">Panel de Administración de Pedidos</h2>
                <div class="d-flex flex-wrap gap-2">
                    <a href="Administracion.aspx" class="btn btn-outline-dark"><i class="bi bi-box me-1"></i>Gestionar Artículos</a>
                    <a href="PedidosAdmin.aspx" class="btn btn-dark"><i class="bi bi-box-seam me-1"></i>Gestionar Pedidos</a>
                    <a href="MarcasAdmin.aspx" class="btn btn-outline-dark"><i class="bi bi-tags me-1"></i>Gestionar Marcas</a>
                    <a href="CategoriasAdmin.aspx" class="btn btn-outline-dark"><i class="bi bi-grid me-1"></i>Gestionar Categorías</a>
                </div>
            </div>
        </div>

        <!-- Grilla de Pedidos Interactiva -->
        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <asp:GridView ID="gvPedidosAdmin" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="False" 
                            DataKeyNames="Id" OnRowCommand="gvPedidosAdmin_RowCommand" EmptyDataText="No hay pedidos registrados.">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                <asp:TemplateField HeaderText="Cliente">
                                    <ItemTemplate>
                                        <%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %>
                                        <br /><small class="text-muted"><%# Eval("Cliente.Email") %></small>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="FormaEntrega.Descripcion" HeaderText="Entrega" />
                                <asp:BoundField DataField="FormaPago.Descripcion" HeaderText="Pago" />
                                
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select form-select-sm d-inline-block w-auto">
                                            <asp:ListItem Text="Pendiente" Value="Pendiente" />
                                            <asp:ListItem Text="En preparación" Value="En preparación" />
                                            <asp:ListItem Text="En camino" Value="En camino" />
                                            <asp:ListItem Text="Entregado" Value="Entregado" />
                                            <asp:ListItem Text="Cancelado" Value="Cancelado" />
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnEstadoActual" runat="server" Value='<%# Eval("Estado") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />

                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnActualizar" runat="server" Text="Guardar" CssClass="btn btn-sm btn-success me-1" CommandName="ActualizarEstado" CommandArgument='<%# Eval("Id") %>' />
                                        <a href='<%# "DetallePedidoAdmin.aspx?id=" + Eval("Id") %>' class="btn btn-sm btn-primary">Ver Detalle</a>
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