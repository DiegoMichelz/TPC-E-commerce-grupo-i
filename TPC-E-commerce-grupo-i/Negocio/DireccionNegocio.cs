using System;
using System.Collections.Generic;
using global::TPC_E_commerce_grupo_i.Models;

namespace TPC_E_commerce_grupo_i.Negocio
{
    public class DireccionNegocio
    {
        // 1. Obtener todas las direcciones asociadas a un cliente
        public List<Direccion> ListarPorCliente(int idCliente)
        {
            List<Direccion> lista = new List<Direccion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, IdCliente, Calle, Numero, Piso, Departamento, CodigoPostal, Localidad, Provincia, Alias, EsPredeterminada, Activo FROM Direcciones WHERE IdCliente = @idCliente AND Activo = 1");
                datos.setParametro("@idCliente", idCliente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion aux = new Direccion();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdCliente = (int)datos.Lector["IdCliente"];
                    aux.Calle = (string)datos.Lector["Calle"];
                    aux.Numero = (string)datos.Lector["Numero"];

                    if (!(datos.Lector["Piso"] is DBNull))
                        aux.Piso = (string)datos.Lector["Piso"];

                    if (!(datos.Lector["Departamento"] is DBNull))
                        aux.Departamento = (string)datos.Lector["Departamento"];

                    aux.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    aux.Localidad = (string)datos.Lector["Localidad"];
                    aux.Provincia = (string)datos.Lector["Provincia"];

                    if (!(datos.Lector["Alias"] is DBNull))
                        aux.Alias = (string)datos.Lector["Alias"];

                    aux.EsPredeterminada = (bool)datos.Lector["EsPredeterminada"];
                    aux.Activo = (bool)datos.Lector["Activo"];

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

        // 2. Obtener una dirección específica por su ID
        public Direccion BuscarPorId(int id)
        {
            Direccion direccion = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("SELECT Id, IdCliente, Calle, Numero, Piso, Departamento, CodigoPostal, Localidad, Provincia, Alias, EsPredeterminada, Activo FROM Direcciones WHERE Id = @id AND Activo = 1");
                datos.setParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    direccion = new Direccion();
                    direccion.Id = (int)datos.Lector["Id"];
                    direccion.IdCliente = (int)datos.Lector["IdCliente"];
                    direccion.Calle = (string)datos.Lector["Calle"];
                    direccion.Numero = (string)datos.Lector["Numero"];

                    if (!(datos.Lector["Piso"] is DBNull))
                        direccion.Piso = (string)datos.Lector["Piso"];

                    if (!(datos.Lector["Departamento"] is DBNull))
                        direccion.Departamento = (string)datos.Lector["Departamento"];

                    direccion.CodigoPostal = (string)datos.Lector["CodigoPostal"];
                    direccion.Localidad = (string)datos.Lector["Localidad"];
                    direccion.Provincia = (string)datos.Lector["Provincia"];

                    if (!(datos.Lector["Alias"] is DBNull))
                        direccion.Alias = (string)datos.Lector["Alias"];

                    direccion.EsPredeterminada = (bool)datos.Lector["EsPredeterminada"];
                    direccion.Activo = (bool)datos.Lector["Activo"];
                }

                return direccion;
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

        // 3. Agregar una nueva dirección
        public void Agregar(Direccion nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("INSERT INTO Direcciones (IdCliente, Calle, Numero, Piso, Departamento, CodigoPostal, Localidad, Provincia, Alias, EsPredeterminada, Activo) VALUES (@idCliente, @calle, @numero, @piso, @depto, @cp, @localidad, @provincia, @alias, @predeterminada, 1)");

                datos.setParametro("@idCliente", nueva.IdCliente);
                datos.setParametro("@calle", nueva.Calle);
                datos.setParametro("@numero", nueva.Numero);
                datos.setParametro("@piso", (object)nueva.Piso ?? DBNull.Value);
                datos.setParametro("@depto", (object)nueva.Departamento ?? DBNull.Value);
                datos.setParametro("@cp", nueva.CodigoPostal);
                datos.setParametro("@localidad", nueva.Localidad);
                datos.setParametro("@provincia", nueva.Provincia);
                datos.setParametro("@alias", (object)nueva.Alias ?? DBNull.Value);
                datos.setParametro("@predeterminada", nueva.EsPredeterminada);

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

        // 4. Modificar una dirección existente
        public void Modificar(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Direcciones SET Calle = @calle, Numero = @numero, Piso = @piso, Departamento = @depto, CodigoPostal = @cp, Localidad = @localidad, Provincia = @provincia, Alias = @alias, EsPredeterminada = @predeterminada WHERE Id = @id");

                datos.setParametro("@id", direccion.Id);
                datos.setParametro("@calle", direccion.Calle);
                datos.setParametro("@numero", direccion.Numero);
                datos.setParametro("@piso", (object)direccion.Piso ?? DBNull.Value);
                datos.setParametro("@depto", (object)direccion.Departamento ?? DBNull.Value);
                datos.setParametro("@cp", direccion.CodigoPostal);
                datos.setParametro("@localidad", direccion.Localidad);
                datos.setParametro("@provincia", direccion.Provincia);
                datos.setParametro("@alias", (object)direccion.Alias ?? DBNull.Value);
                datos.setParametro("@predeterminada", direccion.EsPredeterminada);

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

        // 5. Baja lógica de una dirección
        public void EliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setConsulta("UPDATE Direcciones SET Activo = 0 WHERE Id = @id");
                datos.setParametro("@id", id);
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
    }
}
