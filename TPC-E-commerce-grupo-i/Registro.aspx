<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-6">
            <div class="card shadow">
                <div class="card-header bg-success text-white text-center">
                    <h4>Crear Cuenta Nueva</h4>
                </div>
                <div class="card-body">
                    <!-- Campo Email -->
                    <div class="mb-3">
                        <label for="txtEmail" class="form-label">Correo Electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="nombre@ejemplo.com" required="true"></asp:TextBox>
                    </div>

                    <!-- Campo Contraseña -->
                    <div class="mb-3">
                        <label for="txtPassword" class="form-label">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="********" required="true"></asp:TextBox>
                    </div>

                    <!-- Campo Nombre -->
                    <div class="mb-3">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Tu nombre"></asp:TextBox>
                    </div>

                    <!-- Campo Apellido -->
                    <div class="mb-3">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Tu apellido"></asp:TextBox>
                    </div>

                    <!-- Botón Registrarse -->
                    <div class="d-grid gap-2">
                        <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" CssClass="btn btn-success" OnClick="btnRegistrarse_Click" />
                    </div>
                </div>
                <div class="card-footer text-center text-muted">
                    <small>¿Ya tenés una cuenta? <a href="Login.aspx">Iniciá sesión acá</a></small>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
