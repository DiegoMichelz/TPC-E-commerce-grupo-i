<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-5">

        <!-- Título de Sección -->
        <div class="mb-4">
            <h1 class="h3 fw-bold text-dark mb-1"><i class="bi bi-cart3 text-primary me-2"></i>Carrito de Compras</h1>
            <p class="text-muted small">Revisá los productos seleccionados antes de avanzar con el pago y envío.</p>
        </div>

        <div class="row g-4">

            <!-- Columna Izquierda: Listado de Ítems del Carrito -->
            <div class="col-lg-8">
                <div class="card border shadow-sm rounded-3 overflow-hidden mb-4">
                    <div class="card-header bg-white py-3 border-bottom">
                        <h5 class="card-title h6 mb-0 fw-bold text-dark">Productos en tu carrito (2)</h5>
                    </div>
                    <div class="card-body p-0">

                        <!-- Ítem 1 del Carrito -->
                        <div class="p-3 border-bottom d-flex flex-column flex-md-row align-items-center justify-content-between gap-3">
                            <div class="d-flex align-items-center gap-3 w-100">
                                <img src="https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=150&q=80" alt="Auriculares" class="img-fluid rounded border bg-light" style="width: 80px; height: 80px; object-fit: contain;" />
                                <div>
                                    <h6 class="fw-bold mb-1 text-dark">Auriculares Inalámbricos Noise Cancelling Bluetooth Pro</h6>
                                    <small class="text-muted d-block mb-1">Código: #NS-8842</small>
                                    <span class="badge bg-success bg-opacity-10 text-success border border-success border-opacity-25 small">Envío Gratis</span>
                                </div>
                            </div>

                            <div class="d-flex align-items-center justify-content-between justify-content-md-end w-100 w-md-auto gap-4">
                                <!-- Selector de Cantidad -->
                                <div class="input-group input-group-sm" style="width: 110px;">
                                    <button class="btn btn-outline-secondary" type="button">-</button>
                                    <input type="text" class="form-control text-center bg-white" value="1" readonly>
                                    <button class="btn btn-outline-secondary" type="button">+</button>
                                </div>

                                <!-- Subtotal -->
                                <div class="text-end" style="min-width: 100px;">
                                    <span class="fw-bold text-dark fs-5">$58.900</span>
                                </div>

                                <!-- Botón Eliminar -->
                                <button type="button" class="btn btn-outline-danger btn-sm border-0" title="Eliminar producto">
                                    <i class="bi bi-trash fs-5"></i>
                                </button>
                            </div>
                        </div>

                        <!-- Ítem 2 del Carrito -->
                        <div class="p-3 d-flex flex-column flex-md-row align-items-center justify-content-between gap-3">
                            <div class="d-flex align-items-center gap-3 w-100">
                                <img src="https://images.unsplash.com/photo-1526738549149-8e07eca6c147?auto=format&fit=crop&w=150&q=80" alt="Cafetera" class="img-fluid rounded border bg-light" style="width: 80px; height: 80px; object-fit: contain;" />
                                <div>
                                    <h6 class="fw-bold mb-1 text-dark">Cafetera Express Automática Digital</h6>
                                    <small class="text-muted d-block mb-1">Código: #NS-1049</small>
                                    <span class="badge bg-success bg-opacity-10 text-success border border-success border-opacity-25 small">Envío Gratis</span>
                                </div>
                            </div>

                            <div class="d-flex align-items-center justify-content-between justify-content-md-end w-100 w-md-auto gap-4">
                                <!-- Selector de Cantidad -->
                                <div class="input-group input-group-sm" style="width: 110px;">
                                    <button class="btn btn-outline-secondary" type="button">-</button>
                                    <input type="text" class="form-control text-center bg-white" value="1" readonly>
                                    <button class="btn btn-outline-secondary" type="button">+</button>
                                </div>

                                <!-- Subtotal -->
                                <div class="text-end" style="min-width: 100px;">
                                    <span class="fw-bold text-dark fs-5">$185.000</span>
                                </div>

                                <!-- Botón Eliminar -->
                                <button type="button" class="btn btn-outline-danger btn-sm border-0" title="Eliminar producto">
                                    <i class="bi bi-trash fs-5"></i>
                                </button>
                            </div>
                        </div>

                    </div>
                </div>

                <!-- Acciones secundarias -->
                <div class="d-flex justify-content-between align-items-center">
                    <a href="Default.aspx#catalogo-seccion" class="btn btn-outline-primary btn-sm">
                        <i class="bi bi-arrow-left me-1"></i>Seguir Comprando
                    </a>
                    <button type="button" class="btn btn-outline-secondary btn-sm">
                        <i class="bi bi-trash3 me-1"></i>Vaciar Carrito
                   
                    </button>
                </div>
            </div>

            <!-- Columna Derecha: Resumen de Compra -->
            <div class="col-lg-4">
                <div class="card border shadow-sm rounded-3 p-4 bg-white sticky-top" style="top: 20px;">
                    <h5 class="card-title fw-bold text-dark mb-3">Resumen del Pedido</h5>

                    <div class="d-flex justify-content-between mb-2 text-muted small">
                        <span>Subtotal productos</span>
                        <span class="fw-semibold text-dark">$243.900</span>
                    </div>
                    <div class="d-flex justify-content-between mb-2 text-muted small">
                        <span>Envío a domicilio</span>
                        <span class="text-success fw-semibold">GRATIS</span>
                    </div>

                    <hr class="my-3">

                    <div class="d-flex justify-content-between mb-4">
                        <span class="fw-bold text-dark fs-5">Total Final</span>
                        <span class="fw-bold text-primary fs-4">$243.900</span>
                    </div>

                    <div class="d-grid gap-2">
                        <!-- Botón para ir al Checkout -->
                        <a href="Checkout.aspx" class="btn btn-primary btn-lg py-3 fw-bold shadow-sm">Iniciar Compra <i class="bi bi-arrow-right ms-2"></i>
                        </a>
                    </div>

                    <!-- Sellos de confianza -->
                    <div class="mt-4 pt-3 border-top text-center text-muted small">
                        <p class="mb-1"><i class="bi bi-lock-fill text-success me-1"></i>Compra 100% Segura y Protegida</p>
                        <p class="mb-0 text-secondary" style="font-size: 0.8rem;">Garantía oficial NovaStore 2027</p>
                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
