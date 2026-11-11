using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using TPC_E_commerce_grupo_i.Models;
using TPC_E_commerce_grupo_i.Negocio;

namespace TPC_E_commerce_grupo_i
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos { get; set; }
        public List<int> CategoriasSeleccionadas { get; set; } = new List<int>();
        public List<int> MarcasSeleccionadas { get; set; } = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1. SIEMPRE capturamos los valores tildados primero
            ObtenerFiltrosSeleccionados();

            if (!IsPostBack)
            {
                // Si viene filtrado por Categoría Rápida desde el banner (QueryString)
                if (Request.QueryString["idCategoria"] != null)
                {
                    int idCat;
                    if (int.TryParse(Request.QueryString["idCategoria"], out idCat))
                    {
                        CategoriasSeleccionadas.Add(idCat);
                    }
                }
            }

            // 2. Cargamos el sidebar y los productos en cada petición para mantener visualmente los 'checked'
            CargarFiltrosSidebar();
            CargarArticulos();
        }

        private void CargarFiltrosSidebar()
        {
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            List<Categoria> listaCat = catNegocio.Listar();
            List<Marca> listaMarca = marcaNegocio.Listar();

            repFiltroCategorias.DataSource = listaCat;
            repFiltroCategorias.DataBind();

            repCategoriasRapidas.DataSource = listaCat;
            repCategoriasRapidas.DataBind();

            repFiltroMarcas.DataSource = listaMarca;
            repFiltroMarcas.DataBind();
        }

        private void ObtenerFiltrosSeleccionados()
        {
            CategoriasSeleccionadas.Clear();
            MarcasSeleccionadas.Clear();

            // Leer categorías enviadas por el formulario HTML
            string cats = Request.Form["chkCat"];
            if (!string.IsNullOrEmpty(cats))
            {
                foreach (string id in cats.Split(','))
                {
                    int idCat;
                    if (int.TryParse(id.Trim(), out idCat))
                        CategoriasSeleccionadas.Add(idCat);
                }
            }

            // Leer marcas enviadas por el formulario HTML
            string marcas = Request.Form["chkMarca"];
            if (!string.IsNullOrEmpty(marcas))
            {
                foreach (string id in marcas.Split(','))
                {
                    int idMarca;
                    if (int.TryParse(id.Trim(), out idMarca))
                        MarcasSeleccionadas.Add(idMarca);
                }
            }
        }

        public bool EsCategoriaSeleccionada(int id)
        {
            return CategoriasSeleccionadas.Contains(id);
        }

        public bool EsMarcaSeleccionada(int id)
        {
            return MarcasSeleccionadas.Contains(id);
        }

        private void CargarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> listaOriginal = negocio.Listar();

            if (listaOriginal == null)
                listaOriginal = new List<Articulo>();

            // Filtrado seguro por Categoría (previene errores si a algún artículo le falta la categoría)
            if (CategoriasSeleccionadas.Count > 0)
            {
                listaOriginal = listaOriginal
                    .Where(a => a.Categoria != null && CategoriasSeleccionadas.Contains(a.Categoria.Id))
                    .ToList();
            }

            // Filtrado seguro por Marca (previene errores si a algún artículo le falta la marca)
            if (MarcasSeleccionadas.Count > 0)
            {
                listaOriginal = listaOriginal
                    .Where(a => a.Marca != null && MarcasSeleccionadas.Contains(a.Marca.Id))
                    .ToList();
            }

            // Ordenamiento
            switch (ddlOrden.SelectedValue)
            {
                case "1": // Menor precio
                    listaOriginal = listaOriginal.OrderBy(a => a.Precio).ToList();
                    break;
                case "2": // Mayor precio
                    listaOriginal = listaOriginal.OrderByDescending(a => a.Precio).ToList();
                    break;
                case "3": // Nombre A-Z
                    listaOriginal = listaOriginal.OrderBy(a => a.Nombre).ToList();
                    break;
            }

            ListaArticulos = listaOriginal;
            repArticulos.DataSource = ListaArticulos;
            repArticulos.DataBind();
        }

        protected void btnAplicarFiltros_Click(object sender, EventArgs e)
        {
            // El trabajo se procesa en Page_Load al hacer PostBack
        }

        protected void ddlOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            // El ordenamiento se procesa en Page_Load al cambiar la opción
        }
    }
}