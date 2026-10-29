<%@ Page Title="" Language="C#" MasterPageFile="~/MasterE-commerce.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="TPC_E_commerce_grupo_i.Administracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-12 d-flex justify-content-between align-items-center mb-3">
                <h2>Panel de Administración de Artículos</h2>
                <!-- Botón para cuando armen el alta de productos -->
                <a href="#" class="btn btn-primary">Agregar Nuevo Artículo</a>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <!-- Tabla vacía de ejemplo con Bootstrap -->
                        <table class="table table-striped table-hover">
                            <thead class="table-dark">
                                <tr>
                                    <th>ID</th>
                                    <th>Código</th>
                                    <th>Nombre</th>
                                    <th>Precio</th>
                                    <th>Acciones</th>
                                </tr>
                            </thead>
                            <tbody>
                                <!-- Fila de ejemplo estática para que no quede vacío -->
                                <tr>
                                    <td>1</td>
                                    <td>ART001</td>
                                    <td>Auriculares Inalámbricos</td>
                                    <td>$35,000.00</td>
                                    <td>
                                        <a href="#" class="btn btn-sm btn-warning">Modificar</a>
                                        <a href="#" class="btn btn-sm btn-danger">Eliminar</a>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
