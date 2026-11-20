using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace TPC_E_commerce_grupo_i
{
    public static class Validacion
    {
        public static bool EstaVacio(TextBox control)
        {
            return control == null || string.IsNullOrWhiteSpace(control.Text);
        }

        public static bool EsEmailValido(TextBox control)
        {
            if (EstaVacio(control)) return false;
            return Regex.IsMatch(control.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        // Acepta tanto "35000,00" como "35000.00" y exige > 0
        public static bool EsPrecioValido(TextBox control, out decimal precio)
        {
            precio = 0;
            if (EstaVacio(control)) return false;

            string textoNormalizado = control.Text.Trim().Replace(',', '.');
            return decimal.TryParse(textoNormalizado, NumberStyles.Any, CultureInfo.InvariantCulture, out precio) && precio > 0;
        }

        // Acepta 0 o mayor (para Stock Disponible)
        public static bool EsEnteroNoNegativo(TextBox control, out int valor)
        {
            valor = 0;
            if (EstaVacio(control)) return false;
            return int.TryParse(control.Text.Trim(), out valor) && valor >= 0;
        }

        // Para DropDownList de Marca, Categoría o Rol
        public static bool TieneSeleccion(DropDownList ddl)
        {
            return ddl != null && !string.IsNullOrEmpty(ddl.SelectedValue) && ddl.SelectedValue != "0";
        }

        // Para URLs opcionales (si está vacía devuelve true; si escribieron algo, valida http/https)
        public static bool EsUrlOpcionalValida(TextBox control)
        {
            if (EstaVacio(control)) return true;
            return Uri.TryCreate(control.Text.Trim(), UriKind.Absolute, out Uri uri)
                   && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}