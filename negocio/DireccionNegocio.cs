using System;
using dominio;
using negocio;

namespace negocio
{
    public class DireccionNegocio
    {
        public int ObtenerOCrearCliente(Usuario user, string nombreForm, string apellidoForm)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("SELECT Id FROM Clientes WHERE Email = @email");
                datos.setParametro("@email", user.Email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["Id"];
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            AccesoDatos datosInsert = new AccesoDatos();
            try
            {
                string nombre = !string.IsNullOrWhiteSpace(nombreForm) ? nombreForm : (!string.IsNullOrWhiteSpace(user.Nombre) ? user.Nombre : "Cliente");
                string apellido = !string.IsNullOrWhiteSpace(apellidoForm) ? apellidoForm : (!string.IsNullOrWhiteSpace(user.Apellido) ? user.Apellido : "Web");

                datosInsert.setConsulta("INSERT INTO Clientes (Nombre, Apellido, Email, Contrasena, Activo) OUTPUT INSERTED.Id VALUES (@nombre, @apellido, @email, @pass, 1)");
                datosInsert.setParametro("@nombre", nombre);
                datosInsert.setParametro("@apellido", apellido);
                datosInsert.setParametro("@email", user.Email);
                datosInsert.setParametro("@pass", string.IsNullOrEmpty(user.Pass) ? "1234" : user.Pass);

                return Convert.ToInt32(datosInsert.ejecutarScalar());
            }
            finally
            {
                datosInsert.cerrarConexion();
            }
        }

        public Direccion ObtenerPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta(@"SELECT TOP 1 d.Id, d.Calle, d.Numero, d.Piso, d.Departamento, d.CodigoPostal, d.Localidad, d.Provincia 
                                    FROM Direcciones d 
                                    INNER JOIN Clientes c ON d.IdCliente = c.Id 
                                    WHERE c.Email = @email AND d.Activo = 1 
                                    ORDER BY d.EsPredeterminada DESC, d.Id DESC");
                datos.setParametro("@email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Direccion dir = new Direccion();
                    dir.Id = (int)datos.Lector["Id"];
                    dir.Calle = datos.Lector["Calle"].ToString();
                    dir.Numero = datos.Lector["Numero"] is DBNull ? "" : datos.Lector["Numero"].ToString();
                    dir.Piso = datos.Lector["Piso"] is DBNull ? "" : datos.Lector["Piso"].ToString();
                    dir.Departamento = datos.Lector["Departamento"] is DBNull ? "" : datos.Lector["Departamento"].ToString();
                    dir.CodigoPostal = datos.Lector["CodigoPostal"].ToString();
                    dir.Localidad = datos.Lector["Localidad"].ToString();
                    dir.Provincia = datos.Lector["Provincia"].ToString();
                    return dir;
                }

                return null;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void GuardarOActualizar(Direccion dir, int idCliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (dir.Id > 0)
                {
                    datos.setConsulta(@"UPDATE Direcciones 
                                        SET Calle = @calle, Numero = @numero, Piso = @piso, Departamento = @departamento, 
                                            CodigoPostal = @cp, Localidad = @localidad, Provincia = @provincia 
                                        WHERE Id = @id");
                    datos.setParametro("@id", dir.Id);
                }
                else
                {
                    datos.setConsulta(@"INSERT INTO Direcciones (IdCliente, Calle, Numero, Piso, Departamento, CodigoPostal, Localidad, Provincia, EsPredeterminada, Activo) 
                                        VALUES (@idCliente, @calle, @numero, @piso, @departamento, @cp, @localidad, @provincia, 1, 1)");
                    datos.setParametro("@idCliente", idCliente);
                }

                datos.setParametro("@calle", dir.Calle);
                datos.setParametro("@numero", string.IsNullOrEmpty(dir.Numero) ? "S/N" : dir.Numero);
                datos.setParametro("@piso", string.IsNullOrEmpty(dir.Piso) ? (object)DBNull.Value : dir.Piso);
                datos.setParametro("@departamento", string.IsNullOrEmpty(dir.Departamento) ? (object)DBNull.Value : dir.Departamento);
                datos.setParametro("@cp", dir.CodigoPostal);
                datos.setParametro("@localidad", dir.Localidad);
                datos.setParametro("@provincia", dir.Provincia);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}