<%@ Page Title="Gestión de Artículo" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="TPC_E_commerce_grupo_i.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <div class="card shadow-sm border rounded-3">
                    <div class="card-header bg-primary text-white py-3">
                        <h4 class="mb-0 fw-bold">
                            <asp:Label ID="lblTitulo" runat="server" Text="Agregar Nuevo Artículo" />
                        </h4>
                    </div>
                    <div class="card-body p-4">
                        <asp:HiddenField ID="hfIdArticulo" runat="server" Value="0" />

                        <div class="row g-3">
                            <div class="col-md-4">
                                <label class="form-label small fw-bold text-muted">Código</label>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" placeholder="Ej: ART021" Required="true" />
                            </div>
                            <div class="col-md-8">
                                <label class="form-label small fw-bold text-muted">Nombre del Producto</label>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej: Smart TV 55 Pulgadas" Required="true" />
                            </div>

                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">Precio ($)</label>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="0.01" Required="true" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">Stock Disponible</label>
                                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" placeholder="0" TextMode="Number" Required="true" />
                            </div>

                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">Marca</label>
                                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" />
                            </div>
                            <div class="col-md-6">
                                <label class="form-label small fw-bold text-muted">Categoría</label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" />
                            </div>

                            <!-- URL Imagen Principal con Previsualización -->
                            <div class="col-12">
                                <label class="form-label small fw-bold text-muted">URL Imagen Principal</label>
                                <div class="row g-3 align-items-center">
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtImagenUrl" runat="server" CssClass="form-control" 
                                            placeholder="https://ejemplo.com/imagen.jpg" 
                                            oninput="previsualizarImagen(this.value);" 
                                            onchange="previsualizarImagen(this.value);" 
                                            onkeyup="previsualizarImagen(this.value);" 
                                            ClientIDMode="Static" />
                                    </div>
                                    <div class="col-md-4 text-center">
                                        <div class="p-2 border rounded bg-light" style="min-height: 120px; display: flex; align-items: center; justify-content: center;">
                                            <img id="imgPreview" src="https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=500&q=80" 
                                                alt="Vista previa" class="img-fluid rounded" style="max-height: 110px; object-fit: contain;" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-12">
                                <label class="form-label small fw-bold text-muted">Descripción Ampliada</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Ingrese las características del producto..." />
                            </div>
                        </div>

                        <!-- Botones de Acción -->
                        <div class="d-flex justify-content-between align-items-center mt-4 pt-3 border-top">
                            <div>
                                <a href="Administracion.aspx" class="btn btn-outline-secondary me-2">
                                    <i class="bi bi-arrow-left me-1"></i>Cancelar
                                </a>
                                <asp:Button ID="btnEliminar" runat="server" Text="🗑️ Eliminar Artículo" 
                                    CssClass="btn btn-outline-danger fw-bold" OnClick="btnEliminar_Click" Visible="false" 
                                    OnClientClick="return confirm('¿Está seguro de que desea dar de baja este artículo?');" />
                            </div>
                            <div>
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Artículo" CssClass="btn btn-success px-4 fw-bold" OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- JavaScript para Previsualización de Imagen -->
    <script type="text/javascript">
        function previsualizarImagen(url) {
            var img = document.getElementById('imgPreview');
            if (url && url.trim() !== '') {
                img.src = url;
            } else {
                img.src = 'https://images.unsplash.com/photo-1505740420928-5e560c06d30e?auto=format&fit=crop&w=500&q=80';
            }
        }

        window.addEventListener('DOMContentLoaded', function () {
            var txt = document.getElementById('txtImagenUrl');
            if (txt) {
                previsualizarImagen(txt.value);
            }
        });
    </script>
</asp:Content>