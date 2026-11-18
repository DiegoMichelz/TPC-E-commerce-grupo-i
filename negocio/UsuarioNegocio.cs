using System;
using System.Collections.Generic;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        // 1. Validar Credenciales (Loguear)
        public bool Loguear(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT Id, Email, Pass, Nombre, Apellido, UrlImagenPerfil, Admin, ISNULL(Activo, 1) AS Activo FROM USERS WHERE Email = @email AND Pass = @pass");
                datos.setParametro("@email", usuario.Email);
                datos.setParametro("@pass", usuario.Pass);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Admin = (bool)datos.Lector["Admin"];
                    usuario.Activo = (bool)datos.Lector["Activo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["Apellido"];

                    if (!(datos.Lector["UrlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil = (string)datos.Lector["UrlImagenPerfil"];

                    return true;
                }

                return false;
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

        // 2. Insertar Nuevo Usuario o Administrador
        public int Registrar(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("INSERT INTO USERS (Email, Pass, Nombre, Apellido, Admin, Activo) OUTPUT INSERTED.Id VALUES (@email, @pass, @nombre, @apellido, @admin, 1)");
                datos.setParametro("@email", nuevo.Email);
                datos.setParametro("@pass", nuevo.Pass);
                datos.setParametro("@nombre", string.IsNullOrEmpty(nuevo.Nombre) ? (object)DBNull.Value : nuevo.Nombre);
                datos.setParametro("@apellido", string.IsNullOrEmpty(nuevo.Apellido) ? (object)DBNull.Value : nuevo.Apellido);
                datos.setParametro("@admin", nuevo.Admin);

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

        // 3. Listar Usuarios según Filtro de Estado ("TODOS", "ACTIVOS", "INACTIVOS")
        public List<Usuario> Listar(string estadoFiltro = "TODOS")
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT Id, Email, Pass, Nombre, Apellido, UrlImagenPerfil, Admin, ISNULL(Activo, 1) AS Activo FROM USERS ";

                if (estadoFiltro == "ACTIVOS")
                    consulta += "WHERE ISNULL(Activo, 1) = 1 ";
                else if (estadoFiltro == "INACTIVOS")
                    consulta += "WHERE ISNULL(Activo, 1) = 0 ";

                consulta += "ORDER BY Nombre, Apellido";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Pass = (string)datos.Lector["Pass"];
                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value ? (string)datos.Lector["Nombre"] : "";
                    aux.Apellido = datos.Lector["Apellido"] != DBNull.Value ? (string)datos.Lector["Apellido"] : "";
                    aux.UrlImagenPerfil = datos.Lector["UrlImagenPerfil"] != DBNull.Value ? (string)datos.Lector["UrlImagenPerfil"] : "";
                    aux.Admin = (bool)datos.Lector["Admin"];
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

        // 4. Cambiar Estado (Baja Lógica / Reactivación)
        public void CambiarEstado(int idUsuario, bool nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE USERS SET Activo = @estado WHERE Id = @id");
                datos.setParametro("@estado", nuevoEstado);
                datos.setParametro("@id", idUsuario);
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

        // 5. Cambiar Rol de Administrador
        public void CambiarRol(int idUsuario, bool esAdmin)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE USERS SET Admin = @admin WHERE Id = @id");
                datos.setParametro("@admin", esAdmin);
                datos.setParametro("@id", idUsuario);
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

        // 6. Actualizar Datos del Perfil
        public void Actualizar(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE USERS SET Nombre = @nombre, Apellido = @apellido, UrlImagenPerfil = @img WHERE Id = @id");
                datos.setParametro("@nombre", string.IsNullOrEmpty(user.Nombre) ? (object)DBNull.Value : user.Nombre);
                datos.setParametro("@apellido", string.IsNullOrEmpty(user.Apellido) ? (object)DBNull.Value : user.Apellido);
                datos.setParametro("@img", string.IsNullOrEmpty(user.UrlImagenPerfil) ? (object)DBNull.Value : user.UrlImagenPerfil);
                datos.setParametro("@id", user.Id);

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