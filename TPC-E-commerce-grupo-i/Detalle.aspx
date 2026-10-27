<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Detalle.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">

        <!-- Breadcrumb / Ruta de Navegación -->
        <nav aria-label="breadcrumb" class="mb-4">
            <ol class="breadcrumb bg-light p-2 rounded small shadow-sm">
                <li class="breadcrumb-item"><a href="Default.aspx" class="text-decoration-none">Inicio</a></li>
                <li class="breadcrumb-item"><a href="Default.aspx#catalogo-seccion" class="text-decoration-none">Catálogo</a></li>
                <li class="breadcrumb-item"><a href="#" class="text-decoration-none">Audio & Gadgets</a></li>
                <li class="breadcrumb-item active text-primary fw-semibold" aria-current="page">Auriculares Inalámbricos</li>
            </ol>
        </nav>

        <!-- Sección Principal del Producto -->
        <div class="row g-4 bg-white p-4 p-md-5 rounded-3 border shadow-sm mb-5">

            <!-- Columna Izquierda: Imagen del Producto -->
            <div class="col-lg-6">
                <div class="position-relative bg-light rounded-3 overflow-hidden border mb-3 text-center p-3">
                    <span class="position-absolute top-0 start-0 m-3 badge bg-primary">Envío Gratis</span>
                    <img src="https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=800&q=80" alt="Auriculares Inalámbricos" class="img-fluid rounded" style="max-height: 380px; object-fit: contain;" />
                </div>
                <!-- Miniaturas de galería (Simuladas) -->
                <div class="row g-2">
                    <div class="col-3">
                        <div class="border rounded p-1 bg-light border-primary" style="cursor: pointer;">
                            <img src="https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=200&q=80" class="img-fluid rounded" alt="...">
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="border rounded p-1 bg-light" style="cursor: pointer;">
                            <img src="https://images.unsplash.com/photo-1484704849700-f032a568e944?auto=format&fit=crop&w=200&q=80" class="img-fluid rounded" alt="...">
                        </div>
                    </div>
                </div>
            </div>

            <!-- Columna Derecha: Información y Compra -->
            <div class="col-lg-6 d-flex flex-column justify-content-between">
                <div>
                    <div class="d-flex align-items-center justify-content-between mb-2">
                        <span class="text-muted text-uppercase small fw-bold" style="letter-spacing: 0.05em;">Código: #NS-8842</span>
                        <span class="badge bg-success bg-opacity-10 text-success border border-success border-opacity-25">Stock Disponible</span>
                    </div>

                    <h1 class="h3 fw-bold text-dark mb-2">Auriculares Inalámbricos Noise Cancelling Bluetooth Pro</h1>

                    <!-- Calificación -->
                    <div class="d-flex align-items-center gap-2 mb-3">
                        <div class="text-warning small">
                            <i class="bi bi-star-fill"></i>
                            <i class="bi bi-star-fill"></i>
                            <i class="bi bi-star-fill"></i>
                            <i class="bi bi-star-fill"></i>
                            <i class="bi bi-star-fill"></i>
                        </div>
                        <span class="small text-muted">(128 opiniones de compradores)</span>
                    </div>

                    <!-- Precio y Financiación -->
                    <div class="bg-light p-3 rounded-3 border mb-4">
                        <small class="text-muted d-block mb-1">Precio oficial contado / débito</small>
                        <div class="d-flex align-items-baseline gap-3">
                            <span class="fs-2 fw-bold text-primary">$58.900</span>
                            <span class="text-success small fw-semibold"><i class="bi bi-arrow-down-circle"></i>10% OFF pagando con transferencia</span>
                        </div>
                        <hr class="my-2">
                        <div class="small text-muted">
                            <i class="bi bi-credit-card text-primary me-1"></i>Hasta <strong>6 cuotas fijas</strong> de $11.500 sin interés con tarjetas bancarias seleccionadas.
                       
                        </div>
                    </div>

                    <!-- Descripción breve -->
                    <p class="text-muted small mb-4">
                        Experimentá un sonido envolvente de alta fidelidad con cancelación activa de ruido (ANC). Diseñados para máxima comodidad durante todo el día, con almohadillas de memoria viscoelástica y hasta 40 horas de reproducción continua con una sola carga.
                   
                    </p>
                </div>

                <!-- Botones de Acción -->
                <div>
                    <div class="d-grid gap-2 mb-3">
                        <button type="button" class="btn btn-primary btn-lg py-3 fw-bold shadow-sm">
                            <i class="bi bi-cart-plus me-2"></i>Agregar al Carrito
                       
                        </button>
                        <button type="button" class="btn btn-outline-dark btn-lg py-3 fw-semibold">
                            <i class="bi bi-lightning-fill text-warning me-2"></i><span class="text-dark">Comprar Ahora</span>
                        </button>
                    </div>

                    <!-- Garantías rápidas -->
                    <div class="row g-2 pt-3 border-top text-muted small text-center">
                        <div class="col-4">
                            <i class="bi bi-shield-check text-primary fs-5 d-block mb-1"></i>Garantía 12 Meses
                       
                        </div>
                        <div class="col-4">
                            <i class="bi bi-arrow-counterclockwise text-primary fs-5 d-block mb-1"></i>Devolución Gratis
                       
                        </div>
                        <div class="col-4">
                            <i class="bi bi-truck text-primary fs-5 d-block mb-1"></i>Envíos a todo el país
                       
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <!-- Especificaciones Técnicas Detalladas -->
        <div class="bg-white p-4 rounded-3 border shadow-sm mb-5">
            <h4 class="fw-bold h5 mb-3 text-dark"><i class="bi bi-file-earmark-text text-primary me-2"></i>Especificaciones Técnicas</h4>
            <div class="table-responsive">
                <table class="table table-striped table-bordered small mb-0">
                    <tbody>
                        <tr>
                            <th class="w-25 text-muted">Marca / Modelo</th>
                            <td>NovaStore Tech / NC-Pro 2027</td>
                        </tr>
                        <tr>
                            <th class="text-muted">Conectividad</th>
                            <td>Bluetooth 5.3 / Jack 3.5mm / USB-C</td>
                        </tr>
                        <tr>
                            <th class="text-muted">Autonomía de Batería</th>
                            <td>Hasta 40 horas (con ANC desactivado) / 30 horas (con ANC activo)</td>
                        </tr>
                        <tr>
                            <th class="text-muted">Tiempo de Carga</th>
                            <td>Carga rápida: 10 minutos para 3 horas de reproducción</td>
                        </tr>
                        <tr>
                            <th class="text-muted">Contenido en Caja</th>
                            <td>Auriculares, estuche rígido de transporte, cable USB-C de carga y manual oficial.</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

    </div>
</asp:Content>
