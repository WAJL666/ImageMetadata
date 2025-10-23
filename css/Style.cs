namespace ImageMetadataTools.css
{
    public class Style
    {
        #region colores
        //Aplicar color a la linea 
        public static void MostrarLiena()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;//aplicar color
            Console.WriteLine("──────────────────────────────────────────────────────────────");
            Console.ResetColor(); //resetea el color
        }

        //aplicar color al titulo
        public static void MostrarTitulo(string titulo, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.WriteLine($"─────────────────────────────────────────────────────");
            Console.WriteLine($"               {titulo}");
            Console.WriteLine($"─────────────────────────────────────────────────────");
            Console.ResetColor();
        }
        //aplicar color al error
        public static void MostrarError(string cometario)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(cometario);
            Console.ResetColor();
        }
        #endregion colores
    }
}
