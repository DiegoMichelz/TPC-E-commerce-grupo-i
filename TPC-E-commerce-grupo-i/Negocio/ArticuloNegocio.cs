using System;
using System.Collections.Generic;
using TPC_E_commerce_grupo_i.Models;

namespace TPC_E_commerce_grupo_i.Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = @"
            SELECT 
                A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio,
                M.Id AS IdMarca, M.Descripcion AS Marca,
                C.Id AS IdCategoria, C.Descripcion AS Categoria
            FROM Articulos A
            LEFT JOIN Marcas M ON A.IdMarca = M.Id
            LEFT JOIN Categorias C ON A.IdCategoria = C.Id
            WHERE A.Activo = 1
            ORDER BY A.Id ASC";

                datos.setConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = !(datos.Lector["Descripcion"] is DBNull) ? (string)datos.Lector["Descripcion"] : "";
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    // Cargar Marca
                    if (!(datos.Lector["IdMarca"] is DBNull))
                    {
                        aux.Marca = new Marca
                        {
                            Id = (int)datos.Lector["IdMarca"],
                            Descripcion = (string)datos.Lector["Marca"]
                        };
                    }

                    // Cargar Categoria
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Descripcion = (string)datos.Lector["Categoria"]
                        };
                    }

                    lista.Add(aux);
                }

                datos.cerrarConexion();

                // Cargamos todas las imágenes de la tabla Imagenes para cada artículo
                foreach (var art in lista)
                {
                    AccesoDatos datosImg = new AccesoDatos();
                    try
                    {
                        datosImg.setConsulta("SELECT ImagenUrl FROM Imagenes WHERE IdArticulo = @IdArticulo");
                        datosImg.setParametro("@IdArticulo", art.Id);
                        datosImg.ejecutarLectura();
                        while (datosImg.Lector.Read())
                        {
                            art.ImagenesUrl.Add((string)datosImg.Lector["ImagenUrl"]);
                        }
                    }
                    finally
                    {
                        datosImg.cerrarConexion();
                    }
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

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // 1. Insertar el artículo y obtener el ID generado
                datos.setConsulta(@"INSERT INTO Articulos (Codigo, Nombre, Descripcion, Precio, Stock, IdMarca, IdCategoria, Activo) 
                            VALUES (@codigo, @nombre, @descripcion, @precio, @stock, @idMarca, @idCategoria, 1); 
                            SELECT SCOPE_IDENTITY();");

                datos.setParametro("@codigo", nuevo.Codigo);
                datos.setParametro("@nombre", nuevo.Nombre);
                datos.setParametro("@descripcion", string.IsNullOrEmpty(nuevo.Descripcion) ? DBNull.Value : (object)nuevo.Descripcion);
                datos.setParametro("@precio", nuevo.Precio);
                datos.setParametro("@stock", nuevo.Stock);
                datos.setParametro("@idMarca", nuevo.Marca != null ? (object)nuevo.Marca.Id : DBNull.Value);
                datos.setParametro("@idCategoria", nuevo.Categoria != null ? (object)nuevo.Categoria.Id : DBNull.Value);

                int idGenerado = Convert.ToInt32(datos.ejecutarScalar());
                datos.cerrarConexion();

                // 2. Insertar la imagen si fue ingresada
                if (nuevo.ImagenesUrl != null && nuevo.ImagenesUrl.Count > 0 && !string.IsNullOrWhiteSpace(nuevo.ImagenesUrl[0]))
                {
                    AccesoDatos datosImg = new AccesoDatos();
                    datosImg.setConsulta("INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES (@idArticulo, @url)");
                    datosImg.setParametro("@idArticulo", idGenerado);
                    datosImg.setParametro("@url", nuevo.ImagenesUrl[0]);
                    datosImg.ejecutarAccion();
                    datosImg.cerrarConexion();
                }
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

        public void Modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // 1. Actualizar campos del artículo
                datos.setConsulta(@"UPDATE Articulos 
                            SET Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, 
                                Precio = @precio, Stock = @stock, IdMarca = @idMarca, IdCategoria = @idCategoria 
                            WHERE Id = @id");

                datos.setParametro("@id", art.Id);
                datos.setParametro("@codigo", art.Codigo);
                datos.setParametro("@nombre", art.Nombre);
                datos.setParametro("@descripcion", string.IsNullOrEmpty(art.Descripcion) ? DBNull.Value : (object)art.Descripcion);
                datos.setParametro("@precio", art.Precio);
                datos.setParametro("@stock", art.Stock);
                datos.setParametro("@idMarca", art.Marca != null ? (object)art.Marca.Id : DBNull.Value);
                datos.setParametro("@idCategoria", art.Categoria != null ? (object)art.Categoria.Id : DBNull.Value);

                datos.ejecutarAccion();
                datos.cerrarConexion();

                // 2. Actualizar o agregar imagen principal
                if (art.ImagenesUrl != null && art.ImagenesUrl.Count > 0 && !string.IsNullOrWhiteSpace(art.ImagenesUrl[0]))
                {
                    AccesoDatos datosImg = new AccesoDatos();
                    // Eliminamos la imagen previa e insertamos la nueva para mantener el registro simple
                    datosImg.setConsulta("DELETE FROM Imagenes WHERE IdArticulo = @idArticulo; INSERT INTO Imagenes (IdArticulo, ImagenUrl) VALUES (@idArticulo, @url);");
                    datosImg.setParametro("@idArticulo", art.Id);
                    datosImg.setParametro("@url", art.ImagenesUrl[0]);
                    datosImg.ejecutarAccion();
                    datosImg.cerrarConexion();
                }
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

        public void EliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setConsulta("UPDATE Articulos SET Activo = 0 WHERE Id = @id");
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