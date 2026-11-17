using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;
using negocio;

namespace negocio
{
    public class FormaEntregaNegocio
    {
        public List<FormaEntrega> Listar()
        {
            List<FormaEntrega> lista = new List<FormaEntrega>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, Descripcion, Costo, Estado FROM FormasEntrega WHERE Estado = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaEntrega aux = new FormaEntrega();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Costo = (decimal)datos.Lector["Costo"];
                    aux.Estado = (bool)datos.Lector["Estado"];
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
    }
}