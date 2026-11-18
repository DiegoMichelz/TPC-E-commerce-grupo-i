<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="DetallePedidoAdmin.aspx.cs" Inherits="TPC_E_commerce_grupo_i.DetallePedidoAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Detalle del Pedido #<asp:Label ID="lblIdPedido" runat="server" Text="0" /></h2>
            <a href="PedidosAdmin.aspx" class="btn btn-outline-secondary">← Volver a Pedidos</a>
        </div>

        <div class="card shadow-sm p-4 mb-4 bg-white rounded-3">
            <div class="row">
                <div class="col-md-6">
                    <p class="mb-1"><strong>Cliente:</strong> <asp:Label ID="lblCliente" runat="server" /></p>
                    <p class="mb-1"><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server" /></p>
                    <p class="mb-1"><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" /></p>
                </div>
                <div class="col-md-6">
                    <p class="mb-1"><strong>Forma de Entrega:</strong> <asp:Label ID="lblEntrega" runat="server" /></p>
                    <p class="mb-1"><strong>Forma de Pago:</strong> <asp:Label ID="lblPago" runat="server" /></p>
                    <p class="mb-1"><strong>Estado Actual:</strong> <span class="badge bg-warning text-dark"><asp:Label ID="lblEstado" runat="server" /></span></p>
                </div>
            </div>
        </div>

        <h4 class="mb-3">Productos del Pedido</h4>
        <asp:GridView ID="gvDetalleItems" runat="server" CssClass="table table-striped table-hover align-middle" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Producto.Nombre" HeaderText="Artículo" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unitario" DataFormatString="{0:C2}" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <%# (Convert.ToDecimal(Eval("PrecioUnitario")) * Convert.ToInt32(Eval("Cantidad"))).ToString("C2") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="text-end mt-3">
            <h3>Total General: <span class="text-primary"><asp:Label ID="lblTotalGeneral" runat="server" Text="$0.00" /></span></h3>
        </div>
    </div>
</asp:Content>