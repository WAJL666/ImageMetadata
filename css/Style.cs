using ImageMetadataTools.UI;

namespace ImageMetadataTools.css
{
    public class Style
    {
        #region colores
        //Aplicar color a la linea 
        public static void MostrarLiena()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;//aplicar color
            Console.WriteLine("\n──────────────────────────────────────────────────────────────");
            Console.ResetColor(); //resetea el color
        }

        //aplicar color al titulo
        public static void MostrarTitulo(string titulo, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"─────────────────────────────────────────────────────");
            Console.WriteLine($"               {titulo}");
            Console.WriteLine($"─────────────────────────────────────────────────────");
            Console.ResetColor();
        }
        //aplicar color al error
        public static void MostrarError(string v)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(v);
            Console.ResetColor();
            MenuPrincipal.VolverAlMenu();
        }
        #endregion colores
    }
}
