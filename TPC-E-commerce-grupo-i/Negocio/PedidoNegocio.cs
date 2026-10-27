using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPC_E_commerce_grupo_i.Models;

namespace TPC_E_commerce_grupo_i.Negocio
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
    }
}