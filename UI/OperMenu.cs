using ImageMetadataTools.css;
using ImageMetadataTools.Models;
using ImageMetadataTools.Services;

namespace ImageMetadataTools.UI
{
    public class OperMenu
    {
        #region Attributes
        //Almacena los metadatos leídos
        private static MetadataInfo? metadata = null;
        #endregion

        #region Public Methods
        //Opcion 1, muestra los metadatos
        public static void ProcesarImagen(string ruta)
        {
            if (!ValidarArchivo(ruta)) return;

            metadata = MetadataReader.GetMetadata(ruta);
            if (metadata != null)
            {
                MostrarMetadatos(metadata);
            }
            else
            {
                Style.MostrarError("No se pudieron leer los metadatos EXIF.");
            }

            VolverAlMenu();
        }

        //Opcion 2. guarda metadatos en archivo plano txt
        public static void GuardarInformacion()
        {
            if (metadata == null)
            {
                Style.MostrarError("No hay metadatos para guardar. Procese una imagen primero.");
                return;
            }

            MetadataSaver.GuardarEnArchivo(metadata);
            VolverAlMenu();
        }

        //Opcion 3, ingresa carpeta
        public static void IngresarCarpeta(string ruta)
        {
            //rutaDestino = PedirRuta();
            //Manage.IniciarExploracion(ruta);
        }

        //Opcion 4, agrupa imagenes
        public static void EjecutarAgrupacion(int subopcion)
        {
            switch (subopcion)
            {
                case 1:
                    Console.WriteLine("\nOpción 1: Agrupar por fecha");
                    break;
                case 2:
                    Console.WriteLine("\nOpción 2: Agrupar por lugar");
                    break;
                default:
                    Style.MostrarError("Opción no válida en el submenú.");
                    break;
            }

            VolverAlMenu();
        }

        //Valida que el archivo exista y sea de formato imagen
        public static bool ValidarArchivo(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Style.MostrarError("El archivo no existe. Intente de nuevo.");
                return false;
            }

            string ext = Path.GetExtension(imagePath).ToLowerInvariant();
            string[] extensionesValidas = [".jpg", ".jpeg", ".png", ".bmp", ".tiff"];

            if (!extensionesValidas.Contains(ext))
            {
                Style.MostrarError("Formato no soportado. Use JPG, PNG, BMP o TIFF.");
                return false;
            }

            return true;
        }
        #endregion

        #region Private Methods
        //Muestra los metadatos en consola
        private static void MostrarMetadatos(MetadataInfo metadata)
        {
            Console.ResetColor();
            Style.MostrarLinea(ConsoleColor.DarkGray);
            //Información básica
            Style.MostrarTitulo("Información Básica", ConsoleColor.Cyan);
            Console.WriteLine($"Archivo: {metadata.FileName}");
            Console.WriteLine($"Peso: {metadata.FileSize}");
            Console.WriteLine($"Dimensiones: {metadata.Width} x {metadata.Height}");
            Console.WriteLine($"Formato: {metadata.Format}");
            Console.WriteLine($"Orientación: {metadata.Orientation}");
            //Información de la cámara
            Style.MostrarTitulo("Información de la Cámara", ConsoleColor.Magenta);
            Console.WriteLine($"Fabricante: {metadata.CameraMake}");
            Console.WriteLine($"Modelo: {metadata.CameraModel}");
            Console.WriteLine($"Software: {metadata.Software}");
            //Información de la fotografía
            Style.MostrarTitulo("Fotografía", ConsoleColor.Blue);
            Console.WriteLine($"Fecha de captura: {metadata.DateTaken}");
            Console.WriteLine($"Fecha digitalización: {metadata.DateDigitized}");
            Console.WriteLine($"Exposición: {metadata.ExposureTime}");
            Console.WriteLine($"Apertura: {metadata.Aperture}");
            Console.WriteLine($"ISO: {metadata.ISO}");
            Console.WriteLine($"Focal: {metadata.FocalLength}");
            Console.WriteLine($"Programa: {metadata.ExposureProgram}");
            Console.WriteLine($"Medición: {metadata.MeteringMode}");
            Console.WriteLine($"Flash: {metadata.Flash}");
            //Información del lente
            Style.MostrarTitulo("Lente", ConsoleColor.DarkYellow);
            Console.WriteLine($"Fabricante: {metadata.LensMake}");
            Console.WriteLine($"Modelo: {metadata.LensModel}");
            Console.WriteLine($"Balance blancos: {metadata.WhiteBalance}");
            Console.WriteLine($"Fuente de luz: {metadata.LightSource}");
            Console.WriteLine($"Zoom digital: {metadata.DigitalZoomRatio}");
            //Información GPS
            Style.MostrarTitulo("GPS", ConsoleColor.Yellow);
            Console.WriteLine($"Latitud: {metadata.GPSLatitude}");
            Console.WriteLine($"Longitud: {metadata.GPSLongitude}");
            Console.WriteLine($"Altitud: {metadata.GPSAltitude}");
            Style.MostrarLinea(ConsoleColor.DarkGray);
        }

        //Pausa antes de volver al menu
        private static void VolverAlMenu()
        {
            Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }
        #endregion
    }
}
