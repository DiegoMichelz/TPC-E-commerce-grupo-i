using System;
using TPC_E_commerce_grupo_i.Models;

namespace TPC_E_commerce_grupo_i.Negocio
{
    public class UsuarioNegocio
    {
        // 1. Método para Validar Credenciales e Iniciar Sesión (Loguear)
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Buscamos coincidencia exacta de Email y Contraseña (parametrizado contra SQL Injection)
                datos.setConsulta("SELECT Id, Email, Pass, Nombre, Apellido, UrlImagenPerfil, Admin FROM USERS WHERE Email = @email AND Pass = @pass");
                datos.setParametro("@email", usuario.Email);
                datos.setParametro("@pass", usuario.Pass);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // Si encontró el registro, completamos los datos del objeto usuario
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Admin = (bool)datos.Lector["Admin"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["UrlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil = (string)datos.Lector["UrlImagenPerfil"];

                    return true; // Login correcto
                }

                return false; // Credenciales inválidas
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

        // Método para Insertar un Nuevo Usuario (Registrar)
       
        public int Registrar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO USERS (Email, Pass, Nombre, Apellido, Admin) OUTPUT INSERTED.Id VALUES (@email, @pass, @nombre, @apellido, @admin)");
                datos.setParametro("@email", nuevo.Email);
                datos.setParametro("@pass", nuevo.Pass);
                datos.setParametro("@nombre", string.IsNullOrEmpty(nuevo.Nombre) ? (object)DBNull.Value : nuevo.Nombre);
                datos.setParametro("@apellido", string.IsNullOrEmpty(nuevo.Apellido) ? (object)DBNull.Value : nuevo.Apellido);
                datos.setParametro("@admin", nuevo.Admin);

                // ⬇️ Usar ejecutarScalar() y convertir a int en lugar de ejecutarAccion()
                return Convert.ToInt32(datos.ejecutarScalar());
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