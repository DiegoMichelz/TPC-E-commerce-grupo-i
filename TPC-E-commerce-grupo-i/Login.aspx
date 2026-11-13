<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-5">
            <div class="card shadow">
                <div class="card-header bg-primary text-white text-center">
                    <h4>Iniciar Sesión</h4>
                </div>
                <div class="card-body">
                    <!-- 🟢 NUEVO: MENSAJE DE ERROR VISIBLE -->
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="alert alert-danger d-block mb-3" Visible="false"></asp:Label>
                    <!-- Campo de Email -->
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Correo Electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@ejemplo.com"></asp:TextBox>
                    </div>

                    <!-- Campo de Contraseña -->
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="********"></asp:TextBox>
                    </div>

                    <!-- Botón de Ingresar -->
                    <div class="d-grid gap-2">
                        <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-primary" OnClick="btnIngresar_Click" />
                        
                        <!-- Opción de Invitado / Sin Loguear -->
                        <asp:Button ID="btnInvitado" runat="server" Text="Continuar como Invitado" CssClass="btn btn-outline-secondary" CausesValidation="false" OnClick="btnInvitado_Click" />
                    </div>
                </div>
                <div class="card-footer text-center text-muted">
                    <small>¿No tenés cuenta? <a href="Registro.aspx">Registrate acá</a></small>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
