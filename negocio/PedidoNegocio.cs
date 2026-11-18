using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;
using negocio;

namespace negocio
{
    public class PedidoNegocio
    {
        public int Agregar(Pedido nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta(@"INSERT INTO Pedidos (IdCliente, Fecha, IdFormaEntrega, IdFormaPago, IdDireccionEnvio, Estado, Total) 
                                    VALUES (@idCliente, @fecha, @idEntrega, @idPago, @idDireccion, @estado, @total); 
                                    SELECT SCOPE_IDENTITY();");

                datos.setParametro("@idCliente", nuevo.Cliente.Id);
                datos.setParametro("@fecha", nuevo.Fecha);
                datos.setParametro("@idEntrega", nuevo.FormaEntrega.Id);
                datos.setParametro("@idPago", nuevo.FormaPago.Id);

                if (nuevo.DireccionEnvio != null)
                    datos.setParametro("@idDireccion", nuevo.DireccionEnvio.Id);
                else
                    datos.setParametro("@idDireccion", DBNull.Value);

                datos.setParametro("@estado", nuevo.Estado);
                datos.setParametro("@total", nuevo.Total);

                int idPedidoGenerado = Convert.ToInt32(datos.ejecutarScalar());
                datos.cerrarConexion();

                foreach (ItemPedido item in nuevo.Elementos)
                {
                    AccesoDatos datosDetalle = new AccesoDatos();
                    datosDetalle.setConsulta("INSERT INTO ItemsPedido (IdPedido, IdArticulo, Cantidad, PrecioUnitario) VALUES (@idPedido, @idArticulo, @cantidad, @precio)");
                    datosDetalle.setParametro("@idPedido", idPedidoGenerado);
                    datosDetalle.setParametro("@idArticulo", item.Producto.Id);
                    datosDetalle.setParametro("@cantidad", item.Cantidad);
                    datosDetalle.setParametro("@precio", item.PrecioUnitario);
                    datosDetalle.ejecutarAccion();
                    datosDetalle.cerrarConexion();
                }

                return idPedidoGenerado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Pedido> Listar(int idCliente = 0)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"
                    SELECT 
                        P.Id, P.Fecha, P.Estado, P.Total, P.IdCliente,
                        C.Nombre AS ClienteNombre, C.Apellido AS ClienteApellido, C.Email AS ClienteEmail,
                        FE.Id AS IdFormaEntrega, FE.Descripcion AS FormaEntrega,
                        FP.Id AS IdFormaPago, FP.Descripcion AS FormaPago,
                        P.IdDireccionEnvio
                    FROM Pedidos P
                    INNER JOIN Clientes C ON P.IdCliente = C.Id
                    INNER JOIN FormasEntrega FE ON P.IdFormaEntrega = FE.Id
                    INNER JOIN FormasPago FP ON P.IdFormaPago = FP.Id";

                if (idCliente != 0)
                {
                    consulta += " WHERE P.IdCliente = @idCliente";
                    datos.setConsulta(consulta);
                    datos.setParametro("@idCliente", idCliente);
                }
                else
                {
                    datos.setConsulta(consulta);
                }

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Estado = (string)datos.Lector["Estado"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    // Mapeo del cliente
                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["IdCliente"];
                    aux.Cliente.Nombre = (string)datos.Lector["ClienteNombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["ClienteApellido"];
                    aux.Cliente.Email = (string)datos.Lector["ClienteEmail"];

                    // Mapeo de la forma de entrega
                    aux.FormaEntrega = new FormaEntrega();
                    aux.FormaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    aux.FormaEntrega.Descripcion = (string)datos.Lector["FormaEntrega"];

                    // Mapeo de la forma de pago
                    aux.FormaPago = new FormaPago();
                    aux.FormaPago.Id = (int)datos.Lector["IdFormaPago"];
                    aux.FormaPago.Descripcion = (string)datos.Lector["FormaPago"];

                    lista.Add(aux);
                }

                return lista;
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void ModificarEstado(int idPedido, string nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Pedidos SET Estado = @estado WHERE Id = @id");
                datos.setParametro("@estado", nuevoEstado);
                datos.setParametro("@id", idPedido);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Pedido> ListarTodos()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"SELECT P.Id, P.Fecha, P.IdCliente, C.Nombre, C.Apellido, C.Email, 
                            P.IdFormaEntrega, FE.Descripcion AS DescEntrega, 
                            P.IdFormaPago, FP.Descripcion AS DescPago, 
                            P.Estado, P.Total 
                            FROM Pedidos P 
                            INNER JOIN Clientes C ON P.IdCliente = C.Id 
                            INNER JOIN FormasEntrega FE ON P.IdFormaEntrega = FE.Id 
                            INNER JOIN FormasPago FP ON P.IdFormaPago = FP.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];

                    aux.Cliente = new Cliente();
                    aux.Cliente.Id = (int)datos.Lector["IdCliente"];
                    aux.Cliente.Nombre = (string)datos.Lector["Nombre"];
                    aux.Cliente.Apellido = (string)datos.Lector["Apellido"];
                    aux.Cliente.Email = (string)datos.Lector["Email"];

                    aux.FormaEntrega = new FormaEntrega();
                    aux.FormaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    aux.FormaEntrega.Descripcion = (string)datos.Lector["DescEntrega"];

                    aux.FormaPago = new FormaPago();
                    aux.FormaPago.Id = (int)datos.Lector["IdFormaPago"];
                    aux.FormaPago.Descripcion = (string)datos.Lector["DescPago"];

                    aux.Estado = (string)datos.Lector["Estado"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public Pedido ObtenerPedidoConDetalle(int idPedido)
        {
            Pedido pedido = null;
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // 1. Traemos la cabecera del pedido, cliente y descripciones
                datos.setConsulta(@"SELECT P.Id, P.Fecha, P.IdCliente, C.Nombre, C.Apellido, C.Email, 
                            P.IdFormaEntrega, FE.Descripcion AS DescEntrega, 
                            P.IdFormaPago, FP.Descripcion AS DescPago, 
                            P.Estado, P.Total 
                            FROM Pedidos P 
                            INNER JOIN Clientes C ON P.IdCliente = C.Id 
                            INNER JOIN FormasEntrega FE ON P.IdFormaEntrega = FE.Id 
                            INNER JOIN FormasPago FP ON P.IdFormaPago = FP.Id 
                            WHERE P.Id = @id");
                datos.setParametro("@id", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    pedido = new Pedido();
                    pedido.Id = (int)datos.Lector["Id"];
                    pedido.Fecha = (DateTime)datos.Lector["Fecha"];

                    pedido.Cliente = new Cliente();
                    pedido.Cliente.Id = (int)datos.Lector["IdCliente"];
                    pedido.Cliente.Nombre = (string)datos.Lector["Nombre"];
                    pedido.Cliente.Apellido = (string)datos.Lector["Apellido"];
                    pedido.Cliente.Email = (string)datos.Lector["Email"];

                    pedido.FormaEntrega = new FormaEntrega();
                    pedido.FormaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    pedido.FormaEntrega.Descripcion = (string)datos.Lector["DescEntrega"];

                    pedido.FormaPago = new FormaPago();
                    pedido.FormaPago.Id = (int)datos.Lector["IdFormaPago"];
                    pedido.FormaPago.Descripcion = (string)datos.Lector["DescPago"];

                    pedido.Estado = (string)datos.Lector["Estado"];
                    pedido.Total = (decimal)datos.Lector["Total"];
                }
                datos.cerrarConexion();

                // 2. Si encontramos el pedido, traemos sus ítems/elementos asociados
                if (pedido != null)
                {
                    datos = new AccesoDatos();
                    datos.setConsulta(@"SELECT IP.Id, IP.Cantidad, IP.PrecioUnitario, 
                                A.Id AS IdArticulo, A.Codigo, A.Nombre, A.Descripcion, A.Precio 
                                FROM ItemsPedido IP 
                                INNER JOIN Articulos A ON IP.IdArticulo = A.Id 
                                WHERE IP.IdPedido = @idPedido");
                    datos.setParametro("@idPedido", idPedido);
                    datos.ejecutarLectura();

                    pedido.Elementos = new List<ItemPedido>();
                    while (datos.Lector.Read())
                    {
                        ItemPedido item = new ItemPedido();
                        
                        item.Cantidad = (int)datos.Lector["Cantidad"];
                        item.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];

                        item.Producto = new Articulo();
                        item.Producto.Id = (int)datos.Lector["IdArticulo"];
                        item.Producto.Codigo = (string)datos.Lector["Codigo"];
                        item.Producto.Nombre = (string)datos.Lector["Nombre"];
                        item.Producto.Descripcion = datos.Lector["Descripcion"] != DBNull.Value ? (string)datos.Lector["Descripcion"] : "";
                        item.Producto.Precio = (decimal)datos.Lector["Precio"];

                        pedido.Elementos.Add(item);
                    }
                }

                return pedido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}