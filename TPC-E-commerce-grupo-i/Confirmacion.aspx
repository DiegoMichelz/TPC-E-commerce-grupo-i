<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Confirmacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center py-5">
        <div class="card border shadow-sm p-5 mx-auto" style="max-width: 600px;">
            <i class="bi bi-check-circle-fill text-success display-1 mb-3"></i>
            <h1 class="h3 fw-bold text-dark">¡Compra Confirmada con Éxito!</h1>
            <p class="text-muted">Tu pedido se ha procesado correctamente y el carrito ha sido vaciado.</p>
            <div class="mt-4">
                <a href="Default.aspx" class="btn btn-primary px-4 fw-bold">Volver al Catálogo</a>
            </div>
        </div>
    </div>
</asp:Content>
