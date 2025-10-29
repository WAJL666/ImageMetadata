namespace ImageMetadataTools.css
{
    public class Style
    {
        public static void MostrarMenu()
        {
            //Console.Clear();
            int ancho = Console.WindowWidth;

            Console.WriteLine("\n╔═════════════════════════════════════════════ Lector De Metadatos ═════════════════════════════════════════════════╗");

            if (ancho >= 120)
            {
                Console.WriteLine("║ 1. Procesar Imagen. ║ 2. Guardar metadatos en archivo. ║ 3. Ingresar Carpeta. ║ 4. Agrupar Imágenes. ║ 5. Salir.  ║");
            }
            else
            {
                Console.WriteLine("║ 1. Procesar imagen.              ║");
                Console.WriteLine("║ 2. Guardar metadatos en archivo. ║");
                Console.WriteLine("║ 3. Buscar Carpeta.               ║");
                Console.WriteLine("║ 4. Agrupar imágenes.             ║");
                Console.WriteLine("║ 5. Salir.                        ║");
            }

            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");
        }
        public static int MostarMenuAgrupar()
        {
            //Console.Clear();
            Console.WriteLine("\n╔═════════════════════════════════════ Agrupar Imágenes ═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1. Agrupar por fecha.                                                                                            ║");
            Console.WriteLine("║ 2. Agrupar por lugar (requiere metadatos GPS).                                                                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");

            if (int.TryParse(Console.ReadLine(), out int subopcion))
                return subopcion;

            return -1; // opción inválida
        }

        public static int MostarMenuNavegar()
        {
            //Console.Clear();
            Console.WriteLine("\n  ╔═════════════════════════════════════ Navegar Carpeta ══════════════════════════════════╗");
            Console.WriteLine("    ║ 1. Ir a subcarpeta ║ 2. Atras  ║ 3. Siguiente  ║ 4. Procesar Imagen  ║ 5. Salir        ║                                                                                                         ║");
            Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");

            if (int.TryParse(Console.ReadLine(), out int subopcion))
                return subopcion;

            return -1; // opción inválida
        }
        #region colores
        //2 
        public static void MostrarLiena(ConsoleColor color)
        {
            int anchoConsola = Console.WindowWidth;
            Console.ForegroundColor = color;//2.1 
            for (int i = 0; i < anchoConsola; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine();
            Console.ResetColor(); //2.2 
        }

        //3 
        public static void MostrarTitulo(string titulo, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            MostrarLiena(ConsoleColor.Green);
            Console.WriteLine($"                     {titulo}");
            MostrarLiena(ConsoleColor.Green);
            Console.ResetColor();
        }
        //4 
        public static void MostrarError(string cometario)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(cometario);
            Console.ResetColor();
        }
        //5
        public static void MostrarComentarios(string cometario, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(cometario);
            Console.ResetColor();
        }
        public static void MostrarContenido(string nombre, ConsoleColor color, int anchoColumna)
        {
            Console.ForegroundColor = color;
            Console.Write(string.Format("{0,-" + anchoColumna + "}", nombre));
            Console.ResetColor();
        }
        #endregion colores
    }
}

/*
 1. Mostrar menu principal.
 2. Aplicar color a la linea. 
    2.1 Aplicar color.
    2.2 Resetear color.
 3. Recibe mensaje y aplicar color al titulo.
 4. Recibe mensaje y aplica color al error.
 5. Recibe mensaje y color para mostrar comentarios en diferentes colores.
 */