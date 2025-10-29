using ImageMetadataTools.css;
namespace ImageMetadataTools.Services
{
    public class Manage
    {
        private static readonly Stack<string> _carpetaAtras = new();       // pila para navegar atras
        private static readonly Stack<string> _carpetaSiguiente = new();   // pila para navegar adelante

        public static Stack<string> CarpetaAtras => _carpetaAtras;
        public static Stack<string> CarpetaSiguiente => _carpetaSiguiente;

        public static void ImageManage(string rutaCarpeta)
        {
            if (!ValidarCarpeta(rutaCarpeta)) return;
            CarpetaAtras.Push(rutaCarpeta); // carpeta inicial

            List<string> imagenes = ObtenerImagenesValidas(rutaCarpeta);
            MostrarImagenesEncontradas(rutaCarpeta, imagenes);
            MostrarNombres(imagenes, ConsoleColor.Green);

            List<string> carpetas = ObtenerCarpetas(rutaCarpeta);
            MostrarNombres(carpetas, ConsoleColor.Magenta);

            NavegarCarpetas(rutaCarpeta);
        }

        public static void NavegarCarpetas(string rutaCarpeta)
        {
            while (true)
            {
                int opcion = Style.MostarMenuNavegar();
                if (!ProcesarOpcionNavegacion(opcion, rutaCarpeta))
                    break;
            }
        }

        private static bool ProcesarOpcionNavegacion(int opcion, string rutaActual)
        {
            switch (opcion)
            {
                case 1:
                    return NavegarHaciaSubcarpeta(rutaActual);

                case 2:
                    return NavegarHaciaAtras(rutaActual);

                case 3:
                    return NavegarHaciaAdelante(rutaActual);

                case 4:
                    UI.MenuPrincipal.ProcesarImagenExif(rutaActual);
                    return true;

                case 5: // Opción para salir
                    return false;

                default:
                    Style.MostrarError("Opción inválida.");
                    return true;
            }
        }

        private static bool NavegarHaciaSubcarpeta(string rutaActual)
        {
            Style.MostrarComentarios("\nIngrese nombre de la carpeta:", ConsoleColor.Cyan);
            string nombreCarpeta = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(nombreCarpeta))
            {
                Style.MostrarError("Nombre de carpeta vacío.");
                return true;
            }

            string nuevaRuta = Path.Combine(rutaActual, nombreCarpeta);

            if (!ValidarCarpeta(nuevaRuta))
            {
                Style.MostrarError("La carpeta no existe.");
                return true;
            }

            CarpetaAtras.Push(rutaActual);
            CarpetaSiguiente.Clear();
            ImageManage(nuevaRuta);
            return false;
        }

        private static bool NavegarHaciaAtras(string rutaActual)
        {
            if (CarpetaAtras.Count == 0)
            {
                Style.MostrarError("No hay carpeta anterior.");
                return true;
            }

            string rutaAnterior = CarpetaAtras.Pop();
            CarpetaSiguiente.Push(rutaActual);
            NavegarCarpetas(rutaAnterior); 
            return false;
        }

        private static bool NavegarHaciaAdelante(string rutaActual)
        {
            if (CarpetaSiguiente.Count == 0)
            {
                Style.MostrarError("No hay carpeta siguiente.");
                return true;
            }

            string rutaSiguiente = CarpetaSiguiente.Pop();
            CarpetaAtras.Push(rutaActual);
            NavegarCarpetas(rutaSiguiente); 
            return false;
        }

        private static bool ValidarCarpeta(string ruta)
        {
            if (!Directory.Exists(ruta)) // 1
            {
                Style.MostrarError("La carpeta no existe. Intente de nuevo.");
                return false;
            }
            return true;
        }

        private static List<string> ObtenerImagenesValidas(string ruta)
        {
            List<string> imagenes = [];

            foreach (string archivo in Directory.GetFiles(ruta))
            {
                if (UI.MenuPrincipal.ValidarArchivo(archivo))
                {
                    imagenes.Add(archivo);
                }
            }
            return imagenes;
        }
        private static List<string> ObtenerCarpetas(string ruta)
        {
            List<string> carpe = [];
            string[] carpeta = Directory.GetDirectories(ruta);  // 4  

            foreach (string archivo in carpeta)
            {
                carpe.Add(archivo);
            }
            return carpe;
        }

        private static void MostrarImagenesEncontradas(string ruta, List<string> imagenes)
        {
            Style.MostrarTitulo(ruta, ConsoleColor.Green);  // 6
            Console.WriteLine($"Imágenes encontradas: {imagenes.Count}");
            Style.MostrarLiena(ConsoleColor.Green);

            if (imagenes.Count == 0)
            {
                Style.MostrarError("No se encontraron imágenes en la carpeta.");
                Style.MostrarLiena(ConsoleColor.Green);
            }
        }

        public static void MostrarNombres(List<string> imagenes, ConsoleColor color)
        {
            if (imagenes == null || imagenes.Count == 0)
                return;

            // Calcular el ancho máximo del nombre de archivo
            int anchoMaximo = imagenes
                .Select(img => Path.GetFileName(img).Length)
                .Max();

            // Añadir margen para separación visual
            int anchoColumna = anchoMaximo + 2;

            // Obtener el ancho de la consola
            int anchoConsola = Console.WindowWidth;

            // Calcular cuántas columnas caben
            int columnas = Math.Max(1, anchoConsola / anchoColumna);

            for (int i = 0; i < imagenes.Count; i++)
            {
                string nombre = Path.GetFileName(imagenes[i]);
                Style.MostrarContenido(nombre, color, anchoColumna);
                if ((i + 1) % columnas == 0)
                    Console.WriteLine();
            }
            Console.WriteLine(); // Salto final
        }
    }
}

/*  
 *  0. Llamada recursiva para procesar subcarpetas
    1. Verificar si la carpeta existe (Directory.Exists).Si no existe → mostrar un mensaje de error y salir.
    2.  "*"=cualquier nombre, ".*"=cualquier extensión, "."= en conjunto.    
             SearchOption.TopDirectoryOnly=solo en la carpeta indicada, no en subcarpetas.
             SearchOption.AllDirectories=incluye subcarpetas.

/*  
    0. Llamada recursiva para procesar subcarpetas
    1. Verificar si la carpeta existe (Directory.Exists).Si no existe → mostrar un mensaje de error y salir.
    6. Mostrar en consola los nombres de los archivos. puedes usar Path.GetFileName(rutaArchivo) para mostrar solo el nombre, no toda la ruta).
    7. {nombre,-25} deja cada nombre ocupando 25 caracteres de ancho, alineado a la izquierda.muestra solo el nombre del archivo.
    8. Cada 5 nombres, hacer un salto de línea.

se modificó el punto 5 para que la validación de extensiones se haga en el método ValidarArchivo de MenuPrincipal.cs
    3. Obtener todos los archivos de la carpeta (Directory.GetFiles).
    4. Obtener todas las carpetas (Directory.GetDirectories).
    5. Usa un filtro por extensiones válidas (por ejemplo .jpg, .png, .bmp, .tiff). 
  //string[] extensionesValidas = [".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".gif"]; // 5
                //string extension = Path.GetExtension(archivo).ToLowerInvariant(); // 3, 4
                //if (extensionesValidas.Contains(extension))
                //{
                //    imagenes.Add(archivo);
                //}
*/