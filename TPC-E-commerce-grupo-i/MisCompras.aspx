<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="TPC_E_commerce_grupo_i.MisCompras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-4">
        <h2 class="mb-4">Historial de Mis Compras</h2>

        <asp:GridView ID="gvMisCompras" runat="server" CssClass="table table-striped table-hover table-bordered align-middle" AutoGenerateColumns="False" EmptyDataText="Aún no realizaste ninguna compra.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Nº Pedido" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:BoundField DataField="FormaEntrega.Descripcion" HeaderText="Entrega" />
                <asp:BoundField DataField="FormaPago.Descripcion" HeaderText="Forma de Pago" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C2}" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
