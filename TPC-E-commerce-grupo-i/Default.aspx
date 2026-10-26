<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Franja de Envío Gratis Superior -->
    <div class="bg-primary text-white py-1 px-3 text-center small mb-3">
        <i class="bi bi-truck me-1"></i>Envíos gratis a partir de $50.000 | ¡Hasta 12 cuotas sin interés y garantía oficial garantizada!
   
    </div>

    <div class="container py-3">

        <!-- Breadcrumb / Barra de Estado -->
        <div class="bg-light p-2 rounded mb-4 shadow-sm d-flex flex-wrap align-items-center justify-content-between text-muted small">
            <div>
                <i class="bi bi-shop text-primary me-1"></i><strong class="text-dark">NovaStore Online</strong> / Catálogo Multirrubro / <span class="text-primary fw-semibold">Novedades y Tendencias</span>
            </div>
            <div>
                <span class="badge bg-secondary-subtle text-dark border">
                    <i class="bi bi-credit-card text-primary me-1"></i>Hasta 12 cuotas fijas en productos seleccionados
                </span>
            </div>
        </div>

        <!-- Hero Banner Promocional Multirrubro -->
        <div class="p-4 p-md-5 mb-5 bg-white border rounded-3 shadow-sm position-relative overflow-hidden">
            <div class="row align-items-center">
                <div class="col-lg-7 mb-4 mb-lg-0">
                    <span class="badge bg-primary-subtle text-primary mb-2 px-2 py-1 fw-bold">
                        <i class="bi bi-lightning-fill"></i>Lanzamientos • Tecnología & Hogar
                    </span>
                    <h1 class="display-6 fw-bold text-dark tracking-tight">Tecnología, gadgets y electro
                        <br>
                        <span class="text-primary">al mejor precio del mercado</span></h1>
                    <p class="lead text-muted fs-6 my-3">Encontrá las mejores marcas con envíos a todo el país, hasta 12 cuotas sin interés y garantía oficial certificada en cada compra.</p>

                    <div class="d-flex flex-wrap gap-2 mb-4">
                        <a href="#catalogo-seccion" class="btn btn-primary px-4 py-2">
                            <i class="bi bi-bag-fill me-1"></i>Comprar Ahora
                        </a>
                        <a href="#catalogo-seccion" class="btn btn-light border px-4 py-2">
                            <i class="bi bi-fire text-danger me-1"></i>Ver Novedades
                        </a>
                    </div>

                    <!-- Métricas rápidas -->
                    <div class="row g-2 text-center max-w-md">
                        <div class="col-4">
                            <div class="p-2 bg-light rounded border">
                                <h6 class="text-primary fw-bold mb-0">12 Cuotas</h6>
                                <small class="text-muted" style="font-size: 11px;">Sin Interés</small>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="p-2 bg-light rounded border">
                                <h6 class="text-primary fw-bold mb-0">+5.000</h6>
                                <small class="text-muted" style="font-size: 11px;">Productos Activos</small>
                            </div>
                        </div>
                        <div class="col-4">
                            <div class="p-2 bg-light rounded border">
                                <h6 class="text-primary fw-bold mb-0">Garantía</h6>
                                <small class="text-muted" style="font-size: 11px;">100% Oficial</small>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-5 text-center">
                    <div class="position-relative rounded overflow-hidden shadow bg-light aspect-ratio-4-3">
                        <img src="https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=800&q=80" alt="NovaStore Tech" class="img-fluid rounded w-100 object-fit-cover" style="max-height: 280px;" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Franja de Confianza 3 Columnas -->
        <div class="row g-3 mb-5">
            <div class="col-md-4">
                <div class="p-3 bg-white border rounded shadow-sm d-flex align-items-center gap-3 h-100">
                    <div class="fs-3 text-primary bg-primary-subtle p-3 rounded"><i class="bi bi-truck"></i></div>
                    <div>
                        <h6 class="fw-bold mb-1">Envíos a todo el país</h6>
                        <p class="text-muted small mb-0">Entrega rápida a domicilio y seguimiento satelital.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="p-3 bg-white border rounded shadow-sm d-flex align-items-center gap-3 h-100">
                    <div class="fs-3 text-secondary bg-secondary-subtle p-3 rounded"><i class="bi bi-credit-card"></i></div>
                    <div>
                        <h6 class="fw-bold mb-1">Pagos en hasta 12 cuotas</h6>
                        <p class="text-muted small mb-0">Tarjetas de crédito, débito, transferencia y Mercado Pago.</p>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="p-3 bg-white border rounded shadow-sm d-flex align-items-center gap-3 h-100">
                    <div class="fs-3 text-warning bg-warning-subtle p-3 rounded"><i class="bi bi-shield-check"></i></div>
                    <div>
                        <h6 class="fw-bold mb-1">Compra protegida oficial</h6>
                        <p class="text-muted small mb-0">Garantía directa con el fabricante y devolución simple.</p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Categorías Rápidas -->
        <div class="bg-white p-3 rounded border shadow-sm mb-4 d-flex flex-wrap align-items-center justify-content-between gap-2">
            <span class="fw-bold text-dark small"><i class="bi bi-grid-fill text-primary me-1"></i>Categorías Rápidas:</span>
            <div class="d-flex flex-wrap gap-2">
                <button type="button" class="btn btn-sm btn-primary rounded-pill px-3">Todos</button>
                <button type="button" class="btn btn-sm btn-light border rounded-pill px-3">Tecnología</button>
                <button type="button" class="btn btn-sm btn-light border rounded-pill px-3">Audio & Gadgets</button>
                <button type="button" class="btn btn-sm btn-light border rounded-pill px-3">Pequeños Electro</button>
                <button type="button" class="btn btn-sm btn-light border rounded-pill px-3">Hogar Inteligente</button>
            </div>
        </div>

        <!-- Main Content Area: Sidebar Filters + Product Grid -->
        <div class="row g-4" id="catalogo-seccion">

            <!-- Filtros Laterales (Columna 3) -->
            <aside class="col-lg-3">
                <div class="card border shadow-sm mb-3">
                    <div class="card-header bg-light d-flex justify-content-between align-items-center py-2">
                        <span class="fw-bold small text-dark"><i class="bi bi-sliders me-1 text-primary"></i>Filtros de Búsqueda</span>
                        <a href="#" class="small text-primary text-decoration-none">Limpiar</a>
                    </div>
                    <div class="card-body small">
                        <!-- Envío Gratis Switch -->
                        <div class="form-check form-switch bg-light p-2 rounded mb-3">
                            <input class="form-check-input" type="checkbox" id="envioGratisCheck" checked>
                            <label class="form-check-label fw-semibold" for="envioGratisCheck">Envío Gratis (+ $50k)</label>
                        </div>

                        <!-- Departamentos -->
                        <h6 class="text-uppercase fw-bold text-muted" style="font-size: 11px;">Departamentos</h6>
                        <div class="mb-3">
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox" id="cat1">
                                <label class="form-check-label" for="cat1">Tecnología & Informática (32)</label>
                            </div>
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox" id="cat2" checked>
                                <label class="form-check-label" for="cat2">Auriculares & Gadgets (28)</label>
                            </div>
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox" id="cat3">
                                <label class="form-check-label" for="cat3">Pequeños Electro (18)</label>
                            </div>
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" id="cat4">
                                <label class="form-check-label" for="cat4">Hogar Inteligente (12)</label>
                            </div>
                        </div>

                        <!-- Marcas -->
                        <h6 class="text-uppercase fw-bold text-muted" style="font-size: 11px;">Marcas Destacadas</h6>
                        <div class="mb-3">
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox" checked><label class="form-check-label">Samsung</label>
                            </div>
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox" checked><label class="form-check-label">Philips</label>
                            </div>
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox"><label class="form-check-label">Xiaomi</label>
                            </div>
                            <div class="form-check mb-1">
                                <input class="form-check-input" type="checkbox"><label class="form-check-label">Logitech</label>
                            </div>
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox"><label class="form-check-label">JBL</label>
                            </div>
                        </div>

                        <button type="button" class="btn btn-primary btn-sm w-100">Aplicar Filtros</button>
                    </div>
                </div>

                <!-- Ayuda / Soporte por Email -->
                <div class="p-3 bg-light border rounded shadow-sm">
                    <h6 class="fw-bold text-primary mb-1 small"><i class="bi bi-envelope-at me-1"></i>¿Tenés dudas o consultas?</h6>
                    <p class="text-muted mb-2" style="font-size: 12px;">Escribinos a nuestro correo de soporte y te respondemos a la brevedad.</p>
                    <a href="mailto:soporte@novastore.com" class="small fw-bold text-decoration-none">soporte@novastore.com <i class="bi bi-arrow-right"></i></a>
                </div>
            </aside>

            <!-- Grilla Principal de Productos (Columna 9) -->
            <section class="col-lg-9">

                <!-- Barra superior de ordenamiento -->
                <div class="bg-white p-3 rounded border shadow-sm mb-3 d-flex flex-column flex-sm-row justify-content-between align-items-center gap-2">
                    <span class="text-muted small"><i class="bi bi-grid text-secondary me-1"></i>Mostrando <strong>8</strong> de <strong>86</strong> productos</span>
                    <div class="d-flex align-items-center gap-2 w-100 w-sm-auto">
                        <label class="small text-muted text-nowrap">Ordenar por:</label>
                        <select class="form-select form-select-sm">
                            <option selected>Más populares</option>
                            <option>Menor precio</option>
                            <option>Mayor precio</option>
                        </select>
                    </div>
                </div>

                <!-- Grilla de Tarjetas -->
                <div class="row row-cols-1 row-cols-md-2 row-cols-xl-3 g-3">

                    <!-- Producto 1 -->
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0 position-relative">
                            <span class="position-absolute top-0 start-0 m-2 badge bg-primary">Envío Gratis</span>
                            <img src="https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=500&q=80" class="card-img-top bg-light object-fit-cover" style="height: 180px;" alt="...">
                            <div class="card-body d-flex flex-column">
                                <small class="text-muted text-uppercase" style="font-size: 10px;">Audio & Gadgets</small>
                                <h6 class="card-title fw-bold text-dark mt-1 text-truncate">Auriculares Inalámbricos Noise Cancelling</h6>
                                <p class="card-text text-muted small flex-grow-1" style="font-size: 12px;">Cancelación activa de ruido y 40hs de batería.</p>
                                <div class="mb-2">
                                    <small class="text-muted d-block" style="font-size: 11px;">6 cuotas fijas</small>
                                    <span class="fs-5 fw-bold text-primary">$58.900</span>
                                </div>
                                <a href="#" class="btn btn-outline-primary btn-sm w-100"><i class="bi bi-eye"></i>Ver detalle</a>
                            </div>
                        </div>
                    </div>

                    <!-- Producto 2 -->
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0 position-relative">
                            <span class="position-absolute top-0 start-0 m-2 badge bg-warning text-dark">12 Cuotas</span>
                            <img src="https://images.unsplash.com/photo-1570968915860-54d5c301fa9f?auto=format&fit=crop&w=500&q=80" class="card-img-top bg-light object-fit-cover" style="height: 180px;" alt="...">
                            <div class="card-body d-flex flex-column">
                                <small class="text-muted text-uppercase" style="font-size: 10px;">Pequeños Electro</small>
                                <h6 class="card-title fw-bold text-dark mt-1 text-truncate">Cafetera Espresso Automática</h6>
                                <p class="card-text text-muted small flex-grow-1" style="font-size: 12px;">Bomba italiana de 20 bares y espumador profesional.</p>
                                <div class="mb-2">
                                    <small class="text-muted d-block" style="font-size: 11px;">12 cuotas fijas</small>
                                    <span class="fs-5 fw-bold text-primary">$124.000</span>
                                </div>
                                <a href="#" class="btn btn-outline-primary btn-sm w-100"><i class="bi bi-eye"></i>Ver detalle</a>
                            </div>
                        </div>
                    </div>

                    <!-- Producto 3 -->
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0 position-relative">
                            <span class="position-absolute top-0 start-0 m-2 badge bg-danger">-15% OFF</span>
                            <img src="https://images.unsplash.com/photo-1556911220-e15b29be8c8f?auto=format&fit=crop&w=500&q=80" class="card-img-top bg-light object-fit-cover" style="height: 180px;" alt="...">
                            <div class="card-body d-flex flex-column">
                                <small class="text-muted text-uppercase" style="font-size: 10px;">Electro & Cocina</small>
                                <h6 class="card-title fw-bold text-dark mt-1 text-truncate">Freidora de Aire Digital 4.5L</h6>
                                <p class="card-text text-muted small flex-grow-1" style="font-size: 12px;">Cocción 360° y 8 programas preconfigurados.</p>
                                <div class="mb-2">
                                    <small class="text-muted d-block" style="font-size: 11px;">6 cuotas fijas</small>
                                    <span class="fs-5 fw-bold text-primary">$89.500</span>
                                </div>
                                <a href="#" class="btn btn-outline-primary btn-sm w-100"><i class="bi bi-eye"></i>Ver detalle</a>
                            </div>
                        </div>
                    </div>

                    <!-- Producto 4 -->
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0 position-relative">
                            <span class="position-absolute top-0 start-0 m-2 badge bg-success">Más Vendido</span>
                            <img src="https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=500&q=80" class="card-img-top bg-light object-fit-cover" style="height: 180px;" alt="...">
                            <div class="card-body d-flex flex-column">
                                <small class="text-muted text-uppercase" style="font-size: 10px;">Gadgets & Smart</small>
                                <h6 class="card-title fw-bold text-dark mt-1 text-truncate">Smartwatch Deportivo AMOLED</h6>
                                <p class="card-text text-muted small flex-grow-1" style="font-size: 12px;">Monitor cardíaco, SpO2 y GPS integrado.</p>
                                <div class="mb-2">
                                    <small class="text-muted d-block" style="font-size: 11px;">6 cuotas fijas</small>
                                    <span class="fs-5 fw-bold text-primary">$45.000</span>
                                </div>
                                <a href="#" class="btn btn-outline-primary btn-sm w-100"><i class="bi bi-eye"></i>Ver detalle</a>
                            </div>
                        </div>
                    </div>

                    <!-- Producto 5 -->
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0 position-relative">
                            <span class="position-absolute top-0 start-0 m-2 badge bg-secondary">Wireless</span>
                            <img src="https://images.unsplash.com/photo-1587829741301-dc798b83add3?auto=format&fit=crop&w=500&q=80" class="card-img-top bg-light object-fit-cover" style="height: 180px;" alt="...">
                            <div class="card-body d-flex flex-column">
                                <small class="text-muted text-uppercase" style="font-size: 10px;">Informática & Gaming</small>
                                <h6 class="card-title fw-bold text-dark mt-1 text-truncate">Teclado Mecánico RGB Compacto</h6>
                                <p class="card-text text-muted small flex-grow-1" style="font-size: 12px;">Switches Hot-Swap y Bluetooth 5.0.</p>
                                <div class="mb-2">
                                    <small class="text-muted d-block" style="font-size: 11px;">6 cuotas fijas</small>
                                    <span class="fs-5 fw-bold text-primary">$38.200</span>
                                </div>
                                <a href="#" class="btn btn-outline-primary btn-sm w-100"><i class="bi bi-eye"></i>Ver detalle</a>
                            </div>
                        </div>
                    </div>

                    <!-- Producto 6 -->
                    <div class="col">
                        <div class="card h-100 shadow-sm border-0 position-relative">
                            <span class="position-absolute top-0 start-0 m-2 badge bg-info text-dark">IPX7 Resist</span>
                            <img src="https://images.unsplash.com/photo-1608043152269-423dbba4e7e1?auto=format&fit=crop&w=500&q=80" class="card-img-top bg-light object-fit-cover" style="height: 180px;" alt="...">
                            <div class="card-body d-flex flex-column">
                                <small class="text-muted text-uppercase" style="font-size: 10px;">Audio Portátil</small>
                                <h6 class="card-title fw-bold text-dark mt-1 text-truncate">Parlante Portátil Bluetooth IPX7</h6>
                                <p class="card-text text-muted small flex-grow-1" style="font-size: 12px;">Sonido envolvente 360° y 18hs de batería.</p>
                                <div class="mb-2">
                                    <small class="text-muted d-block" style="font-size: 11px;">6 cuotas fijas</small>
                                    <span class="fs-5 fw-bold text-primary">$32.900</span>
                                </div>
                                <a href="#" class="btn btn-outline-primary btn-sm w-100"><i class="bi bi-eye"></i>Ver detalle</a>
                            </div>
                        </div>
                    </div>

                </div>

            </section>
        </div>

    </div>
</asp:Content>
