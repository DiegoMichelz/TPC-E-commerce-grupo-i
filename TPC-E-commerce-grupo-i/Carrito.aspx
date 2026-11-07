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
                        <h5 class="card-title h6 mb-0 fw-bold text-dark">Productos en tu carrito</h5>
                    </div>
                    <div class="card-body p-0">

                        <!-- REPEATER DINÁMICO PARA LOS ÍTEMS -->
                        <asp:Repeater ID="repCarrito" runat="server" OnItemCommand="repCarrito_ItemCommand">
                            <ItemTemplate>
                                <div class="p-3 border-bottom d-flex flex-column flex-md-row align-items-center justify-content-between gap-3">
                                    <div class="d-flex align-items-center gap-3 w-100">
                                        <!-- Imagen del Producto (Asumiendo que el ítem tiene acceso al Artículo y su Imagen) -->
                                        <img src='<%# Eval("Producto.ImagenesUrl[0]") %>' alt="Producto" class="img-fluid rounded border bg-light" style="width: 80px; height: 80px; object-fit: contain;" />
                                        <div>
                                            <h6 class="fw-bold mb-1 text-dark"><%# Eval("Producto.Nombre") %></h6>
                                            <small class="text-muted d-block mb-1">Código: <%# Eval("Producto.Codigo") %></small>
                                            <span class="badge bg-success bg-opacity-10 text-success border border-success border-opacity-25 small">Envío Gratis</span>
                                        </div>
                                    </div>

                                    <div class="d-flex align-items-center justify-content-between justify-content-md-end w-100 w-md-auto gap-4">
                                        <!-- Selector de Cantidad -->
                                        <div class="input-group input-group-sm" style="width: 110px;">
                                            <asp:Button runat="server" Text="-" CssClass="btn btn-outline-secondary" CommandName="Restar" CommandArgument='<%# Eval("Producto.Id") %>' />
                                            <asp:TextBox runat="server" Text='<%# Eval("Cantidad") %>' CssClass="form-control text-center bg-white" ReadOnly="true" />
                                            <asp:Button runat="server" Text="+" CssClass="btn btn-outline-secondary" CommandName="Sumar" CommandArgument='<%# Eval("Producto.Id") %>' />
                                        </div>

                                        <!-- Subtotal por ítem -->
                                        <div class="text-end" style="min-width: 100px;">
                                            <span class="fw-bold text-dark fs-5">$<%# Eval("Subtotal") %></span>
                                        </div>

                                        <!-- Botón Eliminar -->
                                        <asp:LinkButton runat="server" CssClass="btn btn-outline-danger btn-sm border-0" CommandName="Eliminar" CommandArgument='<%# Eval("Producto.Id") %>' ToolTip="Eliminar producto">
                                            <i class="bi bi-trash fs-5"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>

                <!-- Acciones secundarias -->
                <div class="d-flex justify-content-between align-items-center">
                    <a href="Default.aspx#catalogo-seccion" class="btn btn-outline-primary btn-sm">
                        <i class="bi bi-arrow-left me-1"></i>Seguir Comprando
                    </a>
                    <asp:Button ID="btnVaciar" runat="server" CssClass="btn btn-outline-secondary btn-sm" Text="Vaciar Carrito" OnClick="btnVaciar_Click" />
                </div>
            </div>

            <!-- Columna Derecha: Resumen de Compra -->
            <div class="col-lg-4">
                <div class="card border shadow-sm rounded-3 p-4 bg-white sticky-top" style="top: 20px;">
                    <h5 class="card-title fw-bold text-dark mb-3">Resumen del Pedido</h5>

                    <div class="d-flex justify-content-between mb-2 text-muted small">
                        <span>Subtotal productos</span>
                        <span class="fw-semibold text-dark">$<asp:Label ID="lblTotalGeneral" runat="server" Text="0.00" /></span>
                    </div>
                    <div class="d-flex justify-content-between mb-2 text-muted small">
                        <span>Envío a domicilio</span>
                        <span class="text-success fw-semibold">GRATIS</span>
                    </div>

                    <hr class="my-3">

                    <div class="d-flex justify-content-between mb-4">
                        <span class="fw-bold text-dark fs-5">Total Final</span>
                        <span class="fw-bold text-primary fs-4">$<asp:Label ID="lblTotalGeneralDos" runat="server" Text="0.00" /></span>
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
