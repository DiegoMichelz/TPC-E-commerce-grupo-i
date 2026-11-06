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
                // Traemos los artículos con los nombres de Marca y Categoría
                string consulta = @"
                    SELECT 
                        A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, 
                        M.Id AS IdMarca, M.Descripcion AS Marca,
                        C.Id AS IdCategoria, C.Descripcion AS Categoria
                    FROM Articulos A
                    LEFT JOIN Marcas M ON A.IdMarca = M.Id
                    LEFT JOIN Categorias C ON A.IdCategoria = C.Id
                    WHERE A.Activo = 1";

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

                    // Cargar Categoría
                    if (!(datos.Lector["IdCategoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Descripcion = (string)datos.Lector["Categoria"]
                        };
                    }

                    // Cargar Lista de Imágenes desde la nueva tabla
                    aux.ImagenesUrl = new List<string>();
                    AccesoDatos datosImagen = new AccesoDatos();
                    try
                    {
                        datosImagen.setConsulta("SELECT ImagenUrl FROM Imagenes WHERE IdArticulo = @IdArticulo");
                        datosImagen.setParametro("@IdArticulo", aux.Id);
                        datosImagen.ejecutarLectura();
                        while (datosImagen.Lector.Read())
                        {
                            aux.ImagenesUrl.Add((string)datosImagen.Lector["ImagenUrl"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        datosImagen.cerrarConexion();
                    }

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