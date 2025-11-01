namespace ImageMetadataTools.css
{
    public static class Style
    {
        // Muestra el menú principal con opciones
        public static void MostrarMenu()
        {
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

        // Muestra el submenú para agrupar imágenes
        public static int MostrarMenuAgrupar()
        {
            Console.WriteLine("\n╔═════════════════════════════════════ Agrupar Imágenes ═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1. Agrupar por fecha.                                                                                            ║");
            Console.WriteLine("║ 2. Agrupar por lugar (requiere metadatos GPS).                                                                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");
            return LeerOpcion();
        }

        // Muestra el submenú para navegar carpetas
        public static int MostrarMenuNavegar()
        {
            Console.WriteLine("\n╔═════════════════════════════════════ Navegar Carpeta ════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1. Ir a subcarpeta ║ 2. Atrás ║ 3. Siguiente ║ 4. Procesar Imagen ║ 5. Salir                                     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");
            return LeerOpcion();
        }

        // Lee y valida la opción ingresada por el usuario
        private static int LeerOpcion()
        {
            if (int.TryParse(Console.ReadLine(), out int opcion))
                return opcion;
            return -1;
        }

        // Muestra una línea horizontal con el color especificado
        public static void MostrarLinea(ConsoleColor color)
        {
            int anchoConsola = Math.Max(40, Console.WindowWidth);
            Console.ForegroundColor = color;
            Console.WriteLine(new string('─', anchoConsola));
            Console.ResetColor();
        }

        // Muestra un título centrado con el color especificado
        public static void MostrarTitulo(string titulo, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            MostrarLinea(color);

            int ancho = Console.WindowWidth;
            int margen = Math.Max(0, (ancho - titulo.Length) / 2);
            Console.WriteLine(new string(' ', margen) + titulo);

            MostrarLinea(color);
            Console.ResetColor();
        }

        // Muestra un mensaje de error en color rojo
        public static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        // Muestra un comentario con el color especificado
        public static void MostrarComentarios(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        // Muestra contenido con el color y ancho de columna especificados
        public static void MostrarContenido(string nombre, ConsoleColor color, int anchoColumna)
        {
            Console.ForegroundColor = color;
            Console.Write(string.Format("{0,-" + anchoColumna + "}", nombre));
            Console.ResetColor();
        }
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