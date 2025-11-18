namespace ImageMetadataTools.css
{
    public static class Style
    {
        // Muestra el menú principal con opciones
        public static int MostrarMenu()
        {
            int ancho = Console.WindowWidth;

            Console.WriteLine("\n╔══════════════ Lector De Metadatos ═════════════════╗");

            if (ancho >= 120)
            {
                Console.WriteLine("║ 1. Procesar Imagen. ║ 2. Navegar. ║ 3. Salir.      ║");
            }
            else
            {
                Console.WriteLine("║ 1. Procesar imagen.     ║");
                Console.WriteLine("║ 2. Navegar.             ║");
                Console.WriteLine("║ 3. Salir .              ║");
            }

            Console.WriteLine("╚════════════════════════════════════════════════════╝");
            return LeerOpcion();
        }

        // Muestra el submenú para agrupar imágenes
        public static int MostrarMenuAgrupar()
        {
            Console.WriteLine("\n╔═════════════════════════════════════ Agrupar Imágenes ═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║ 1. Agrupar por fecha.                                                                                            ║");
            Console.WriteLine("║ 2. Agrupar por lugar (requiere metadatos GPS).                                                                   ║");
            Console.WriteLine("║ 3. Menú anterior.");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
            return LeerOpcion();
        }

        // Muestra el submenú para navegar carpetas
        public static int MostrarMenuNavegar()
        {
            Console.WriteLine("\n╔═════════════════════════════════════ Navegar Carpeta ════════════════════════════════════════════╗");
            Console.WriteLine("║ 1. Ir a subcarpeta ║ 2. Atrás ║ 3. Siguiente ║ 4. Procesar Imagen ║ 5. Agrupar ║ 6. Salir        ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════════════════════════╝");
            return LeerOpcion();
        }

        // Lee y valida la opción ingresada por el usuario
        public static int LeerOpcion()
        {
            Console.Write("Seleccione una opción: ");

            if (int.TryParse(Console.ReadLine(), out int opcion))
                return opcion;

            MostrarComentarios("Entrada inválida. Debe ser un número.", ConsoleColor.Red);
            return -1;
        }

        // Muestra una línea horizontal con el color especificado
        public static void MostrarLinea(ConsoleColor color)
        {
            int anchoConsola = Math.Max(40, Console.WindowWidth);
            Console.ForegroundColor = color;
            Console.WriteLine(new string('=', anchoConsola));
            Console.ResetColor();
        }

        // Muestra un título centrado con el color especificado
        public static void MostrarTitulo(string texto, ConsoleColor color)
        {
            int ancho = Console.WindowWidth;
            int margen = Math.Max((ancho - texto.Length) / 2, 0);

            Console.ForegroundColor = color;
            Console.WriteLine();

            MostrarLinea(color);

            Console.WriteLine(new string(' ', margen) + texto);

            MostrarLinea(color);

            Console.ResetColor();
        }

        //Muestra un comentario con el color especificado
        public static void MostrarComentarios(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"• {mensaje}");
            Console.ResetColor();
        }

        // Calcula el número de columnas que se pueden mostrar en la consola
        public static int CalcularColumnas(List<string> elementos, int margen = 2)
        {
            if (elementos == null || elementos.Count == 0)
                return 1;

            int anchoMax = elementos.Select(e => Path.GetFileName(e).Length).Max();
            int anchoColumna = anchoMax + margen;
            int anchoConsola = Console.WindowWidth;

            return Math.Max(1, anchoConsola / anchoColumna);
        }

        // Muestra una lista de elementos en columnas
        public static void MostrarEnColumnas(List<string> elementos, ConsoleColor color, int columnas)
        {
            if (elementos == null || elementos.Count == 0)
                return;

            int anchoColumna = elementos.Select(e => Path.GetFileName(e).Length).Max() + 2;

            for (int i = 0; i < elementos.Count; i++)
            {
                string nombre = Path.GetFileName(elementos[i]);
                MostrarContenido(nombre, color, anchoColumna);

                if ((i + 1) % columnas == 0)
                    Console.WriteLine();
            }
            Console.WriteLine();
        }

        // Muestra contenido con alineación y color especificados
        public static void MostrarContenido(string texto, ConsoleColor color, int ancho, bool alinearIzquierda = true)
        {
            if (string.IsNullOrEmpty(texto))
                texto = string.Empty;

            string alineado = alinearIzquierda ? texto.PadRight(ancho) : texto.PadLeft(ancho);

            Console.ForegroundColor = color;
            Console.Write(alineado);
            Console.ResetColor();
        }

        // Limpia la pantalla de la consola
        public static void LimpiarPantalla()
        {
            Console.Clear();
        }
    }
}
