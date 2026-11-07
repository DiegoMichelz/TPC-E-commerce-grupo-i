<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">

        <!-- Título de Sección -->
        <div class="mb-4">
            <h1 class="h3 fw-bold text-dark mb-1"><i class="bi bi-shield-lock text-primary me-2"></i>Finalizar Compra</h1>
            <p class="text-muted small">Completá tus datos de envío y pago para confirmar el pedido en NovaStore.</p>
        </div>

        <div class="row g-4">

            <!-- Columna Izquierda: Formularios de Datos -->
            <div class="col-lg-8">

                <!-- 1. Datos de Contacto y Envío -->
                <div class="card border shadow-sm rounded-3 p-4 mb-4 bg-white">
                    <h5 class="fw-bold text-dark mb-3"><i class="bi bi-geo-alt text-primary me-2"></i>1. Dirección de Envío</h5>

                    <div class="row g-3">
                        <div class="col-md-6">
                            <label class="form-label small text-muted">Nombre</label>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Juan" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label small text-muted">Apellido</label>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej: Pérez" />
                        </div>
                        <div class="col-md-8">
                            <label class="form-label small text-muted">Calle y Número</label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" placeholder="Ej: Av. Rivadavia 1420" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label small text-muted">Piso / Depto (Opcional)</label>
                            <asp:TextBox ID="txtPisoDepto" runat="server" CssClass="form-control" placeholder="Ej: 4 B" />
                        </div>
                        <div class="col-md-5">
                            <label class="form-label small text-muted">Localidad / Ciudad</label>
                            <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" placeholder="Ej: Tres Arroyos" />
                        </div>
                        <div class="col-md-4">
                            <label class="form-label small text-muted">Provincia</label>
                            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" placeholder="Ej: Buenos Aires" />
                        </div>
                        <div class="col-md-3">
                            <label class="form-label small text-muted">Código Postal</label>
                            <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" placeholder="Ej: 7500" />
                        </div>
                    </div>
                </div>

                <!-- 2. Medio de Pago -->
                <div class="card border shadow-sm rounded-3 p-4 mb-4 bg-white">
                    <h5 class="fw-bold text-dark mb-3"><i class="bi bi-credit-card text-primary me-2"></i>2. Seleccioná tu Medio de Pago</h5>

                    <div class="vstack gap-3">
                        <div class="form-check border p-3 rounded-3">
                            <asp:RadioButton ID="rbTarjeta" runat="server" GroupName="MedioPago" Checked="true" CssClass="form-check-input" />
                            <label class="form-check-label fw-semibold text-dark ms-2" for="MainContent_rbTarjeta">
                                Tarjeta de Crédito o Débito
                                <span class="d-block text-muted small fw-normal">Hasta 6 cuotas sin interés con bancos seleccionados.</span>
                            </label>
                        </div>
                        <div class="form-check border p-3 rounded-3">
                            <asp:RadioButton ID="rbTransferencia" runat="server" GroupName="MedioPago" CssClass="form-check-input" />
                            <label class="form-check-label fw-semibold text-dark ms-2" for="MainContent_rbTransferencia">
                                Transferencia Bancaria (Alias / CBU)
                                <span class="d-block text-success small fw-semibold">¡10% de descuento abonando por este medio!</span>
                            </label>
                        </div>
                        <div class="form-check border p-3 rounded-3">
                            <asp:RadioButton ID="rbEfectivo" runat="server" GroupName="MedioPago" CssClass="form-check-input" />
                            <label class="form-check-label fw-semibold text-dark ms-2" for="MainContent_rbEfectivo">
                                Pago Fácil / RapiPago
                                <span class="d-block text-muted small fw-normal">Imprimís el ticket y abonás en cualquier sucursal habilitada.</span>
                            </label>
                        </div>
                    </div>
                </div>

                <!-- Botón Volver al Carrito -->
                <div>
                    <a href="Carrito.aspx" class="btn btn-outline-secondary btn-sm">
                        <i class="bi bi-arrow-left me-1"></i>Volver al Carrito
                    </a>
                </div>

            </div>

            <!-- Columna Derecha: Resumen Total y Confirmación -->
            <div class="col-lg-4">
                <div class="card border shadow-sm rounded-3 p-4 bg-white sticky-top" style="top: 20px;">
                    <h5 class="card-title fw-bold text-dark mb-3">Resumen de la Orden</h5>

                    <div class="d-flex justify-content-between mb-2 text-muted small">
                        <span>Productos</span>
                        <span class="fw-semibold text-dark">$<asp:Label ID="lblSubtotal" runat="server" Text="0.00" /></span>
                    </div>
                    <div class="d-flex justify-content-between mb-2 text-muted small">
                        <span>Envío a domicilio</span>
                        <span class="text-success fw-semibold">GRATIS</span>
                    </div>

                    <hr class="my-3">

                    <div class="d-flex justify-content-between mb-4">
                        <span class="fw-bold text-dark fs-5">Total a Pagar</span>
                        <span class="fw-bold text-primary fs-4">$<asp:Label ID="lblTotal" runat="server" Text="0.00" /></span>
                    </div>

                    <div class="d-grid gap-2">
                        <!-- Botón Final para Confirmar Pedido -->
                        <asp:Button ID="btnConfirmar" runat="server" CssClass="btn btn-success btn-lg py-3 fw-bold shadow-sm" Text="Confirmar y Pagar" OnClick="btnConfirmar_Click" />
                    </div>

                    <div class="mt-4 pt-3 border-top text-center text-muted small">
                        <p class="mb-1"><i class="bi bi-shield-fill-check text-primary me-1"></i>Transacción encriptada SSL</p>
                        <p class="mb-0 text-secondary" style="font-size: 0.8rem;">Tus datos viajan seguros</p>
                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
