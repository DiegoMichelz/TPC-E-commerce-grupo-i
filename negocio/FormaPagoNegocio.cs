using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dominio;
using negocio;


namespace negocio
{
    public class FormaPagoNegocio
    {
        public List<FormaPago> Listar()
        {
            List<FormaPago> lista = new List<FormaPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, Descripcion, Estado FROM FormasPago WHERE Estado = 1");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaPago aux = new FormaPago();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
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