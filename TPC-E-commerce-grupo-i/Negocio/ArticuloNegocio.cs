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
    }
}