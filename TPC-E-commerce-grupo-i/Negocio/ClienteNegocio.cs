using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPC_E_commerce_grupo_i.Models;

namespace TPC_E_commerce_grupo_i.Negocio
{
    public class ClienteNegocio
    {
       
            private readonly AccesoDatos datos = new AccesoDatos(); // O la capa de persistencia/repositorio

            // 1. Obtener todos los clientes
            public List<Cliente> Listar()
            {
                List<Cliente> lista = new List<Cliente>();
                // Lógica para ejecutar la consulta SQL / Stored Procedure
                // y mapear cada fila a un objeto Cliente
                return lista;
            }

            // 2. Alta con validación de negocio
            public bool Agregar(Cliente nuevo)
            {
                // Regla de negocio: El email es obligatorio
                if (string.IsNullOrWhiteSpace(nuevo.Email))
                    return false;

                // Regla de negocio: No registrar emails duplicados
                if (BuscarPorEmail(nuevo.Email) != null)
                    return false;

                // Si pasa las validaciones, se envía a la base de datos
                // datos.EjecutarAccion(...);
                return true;
            }

            // 3. Consulta específica por Email
            public Cliente BuscarPorEmail(string email)
            {
                // Lógica de búsqueda
                return new Cliente();
            }

            // 4. Baja lógica
            public void EliminarLogico(int id)
            {
                // Lógica para UPDATE Clientes SET Activo = 0 WHERE Id = id
            }

        // 5. Consulta específica por ID
        public Cliente BuscarPorId(int id)
        {
            Cliente cliente = null;

            try
            {
                datos.setConsulta("SELECT Id, Nombre, Apellido, Email, Telefono, Activo FROM Clientes WHERE Id = @id AND Activo = 1");
                datos.setParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cliente = new Cliente();
                    cliente.Id = (int)datos.Lector["Id"];
                    cliente.Nombre = (string)datos.Lector["Nombre"];
                    cliente.Apellido = (string)datos.Lector["Apellido"];
                    cliente.Email = (string)datos.Lector["Email"];

                    // Verificación de nulo para campos opcionales en BD
                    if (!(datos.Lector["Telefono"] is DBNull))
                        cliente.Telefono = (string)datos.Lector["Telefono"];

                    cliente.Activo = (bool)datos.Lector["Activo"];
                }

                return cliente;
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
