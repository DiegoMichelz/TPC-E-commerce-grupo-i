<%@ Page Title="Mi Perfil" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPC_E_commerce_grupo_i.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card border shadow-sm">
                    <div class="card-header bg-light py-3">
                        <h4 class="fw-bold mb-0 text-dark"><i class="bi bi-person-circle text-primary me-2"></i>Mi Perfil</h4>
                    </div>
                    <div class="card-body p-4">

                        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-success alert-dismissible fade show" role="alert">
                            <i class="bi bi-check-circle-fill me-2"></i><asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        </asp:Panel>

                        <div class="row g-4 align-items-center">
                            <!-- Columna Izquierda: Datos -->
                            <div class="col-md-7">
                                <div class="mb-3">
                                    <label class="form-label small fw-bold text-muted">Email (Cuenta)</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control bg-light" ReadOnly="true"></asp:TextBox>
                                </div>

                                <div class="mb-3">
                                    <label class="form-label small fw-bold text-muted">Nombre</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Placeholder="Tu nombre"></asp:TextBox>
                                </div>

                                <div class="mb-3">
                                    <label class="form-label small fw-bold text-muted">Apellido</label>
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Placeholder="Tu apellido"></asp:TextBox>
                                </div>
                            </div>

                            <!-- Columna Derecha: Avatar Fijo -->
                            <div class="col-md-5 text-center">
                                <div class="p-3 border rounded bg-light">
                                    <asp:Image ID="imgPerfil" runat="server" ImageUrl="https://cdn-icons-png.flaticon.com/512/149/149071.png" 
                                        CssClass="img-fluid rounded-circle border shadow-sm mb-2" Style="width: 150px; height: 150px; object-fit: cover;" />
                                    <div class="small text-muted fw-semibold">Avatar de Usuario</div>
                                </div>
                            </div>
                        </div>

                        <hr class="my-4" />

                        <div class="d-flex justify-content-between">
                            <a href="Default.aspx" class="btn btn-outline-secondary">Volver al Inicio</a>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary px-4 fw-bold" OnClick="btnGuardar_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>