using ImageMetadataTools.css;
using ImageMetadataTools.UI;
namespace ImageMetadataTools.Services
{
    public class Manage
    {
        #region Attributes
        // Historial de navegación de carpetas.
        private static readonly Stack<string> _historialAtras = new();
        private static readonly Stack<string> _historialAdelante = new();
        #endregion

        #region Properties
        // Propiedades para acceder al historial de navegación.
        public static Stack<string> HistorialAtras => _historialAtras;
        public static Stack<string> HistorialAdelante => _historialAdelante;
        #endregion

        #region Public Methods
       
        #endregion

        #region Private Methods
        public static void MostrarContenidoCarpeta(string ruta)
        {
            var imagenes = ObtenerImagenesValidas(ruta);
            var carpetas = ObtenerSubcarpetas(ruta);
           

            Style.MostrarTitulo(ruta, ConsoleColor.Green);
            Style.MostrarComentarios($"Imágenes encontradas: {imagenes.Count}", ConsoleColor.White);
            Style.MostrarLinea(ConsoleColor.Green);

            if (imagenes.Count == 0)
                Style.MostrarError("No se encontraron imágenes en la carpeta.");

            Style.MostrarLinea(ConsoleColor.Green);
            Style.MostrarListado(imagenes, ConsoleColor.Green);
            Style.MostrarListado(carpetas, ConsoleColor.Magenta);
        }

        // Navega a una subcarpeta dentro de la ruta actual.
       

        public static bool NavegarASubcarpeta(ref string rutaActual)
        {
            Style.MostrarComentarios("\nIngrese nombre de la subcarpeta:", ConsoleColor.Cyan);
            string nombre = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Style.MostrarError("Nombre vacío.");
                return true;
            }

            string nuevaRuta = Path.Combine(rutaActual, nombre);

            if (!EsRutaValida(nuevaRuta))
            {
                Style.MostrarError("La carpeta no existe.");
                return true;
            }

            HistorialAtras.Push(rutaActual);
            HistorialAdelante.Clear();
            rutaActual = nuevaRuta;
            return true;
        }

        // Navega a la carpeta anterior en el historial.
        public static bool NavegarAtras(ref string rutaActual)
        {
            if (HistorialAtras.Count == 0)
            {
                Style.MostrarError("No hay carpeta anterior.");
                return true;
            }
            Style.LimpiarPantalla();
            HistorialAdelante.Push(rutaActual);
            rutaActual = HistorialAtras.Pop();
            return true;
        }

        // Navega a la carpeta siguiente en el historial.
        public static bool NavegarAdelante(ref string rutaActual)
        {
            if (HistorialAdelante.Count == 0)
            {
                Style.MostrarError("No hay carpeta siguiente.");
                return true;
            }
            Style.LimpiarPantalla();
            HistorialAtras.Push(rutaActual);
            rutaActual = HistorialAdelante.Pop();
            return true;
        }
        #endregion

        #region Private Methods
        // Verifica si la ruta de la carpeta es válida.
        public static bool EsRutaValida(string ruta)
        {
            if (!Directory.Exists(ruta))
            {
                Style.MostrarError("La carpeta no existe. Intente de nuevo.");
                return false;
            }
            return true;
        }

        // Obtiene las imágenes válidas en la carpeta.
        public static List<string> ObtenerImagenesValidas(string ruta)
        {
            return [.. Directory.GetFiles(ruta).Where(UI.OperMenu.ValidarArchivo)];
        }

        // Obtiene las subcarpetas en la carpeta.
        private static List<string> ObtenerSubcarpetas(string ruta)
        {
            return [.. Directory.GetDirectories(ruta)];
        }     
        #endregion
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