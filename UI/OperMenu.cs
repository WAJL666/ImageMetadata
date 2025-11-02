using ImageMetadataTools.css;
using ImageMetadataTools.Models;
using ImageMetadataTools.Services;

namespace ImageMetadataTools.UI
{
    public class OperMenu
    {
        private static MetadataInfo? metadata = null;
        
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
            Manage.IniciarExploracion(ruta);
        }

        //Opcion 4, agrupa imagenes
        public static void EjecutarAgrupacion(int subopcion)
        {
            switch (subopcion)
            {
                case 1:
                    Style.MostrarComentarios("\nAgrupando imágenes por fecha", ConsoleColor.Green);
                    break;
                case 2:
                    Style.MostrarComentarios("\nAgrupando imágenes por lugar", ConsoleColor.Green);
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

        //Muestra los metadatos en consola
        private static void MostrarMetadatos(MetadataInfo metadata)
        {
            Console.ResetColor();
            Style.MostrarLinea(ConsoleColor.DarkGray);
            //Información básica
            Style.MostrarTitulo("Información Básica", ConsoleColor.Cyan);
            Style.MostrarComentarios($"Archivo: {metadata.FileName}",ConsoleColor.White);
            Style.MostrarComentarios($"Peso: {metadata.FileSize}",ConsoleColor.White);
            Style.MostrarComentarios($"Dimensiones: {metadata.Width} x {metadata.Height}", ConsoleColor.White);
            Style.MostrarComentarios($"Formato: {metadata.Format}", ConsoleColor.White);
            Style.MostrarComentarios($"Orientación: {metadata.Orientation}", ConsoleColor.White);
            //Información de la cámara
            Style.MostrarTitulo("Información de la Cámara", ConsoleColor.Magenta);
            Style.MostrarComentarios($"Fabricante: {metadata.CameraMake}", ConsoleColor.White);
            Style.MostrarComentarios($"Modelo: {metadata.CameraModel}", ConsoleColor.White);
            Style.MostrarComentarios($"Software: {metadata.Software}", ConsoleColor.White);
            //Información de la fotografía
            Style.MostrarTitulo("Fotografía", ConsoleColor.Blue);
            Style.MostrarComentarios($"Fecha de captura: {metadata.DateTaken}", ConsoleColor.White);
            Style.MostrarComentarios($"Fecha digitalización: {metadata.DateDigitized}", ConsoleColor.White);
            Style.MostrarComentarios($"Exposición: {metadata.ExposureTime}", ConsoleColor.White);
            Style.MostrarComentarios($"Apertura: {metadata.Aperture}", ConsoleColor.White);
            Style.MostrarComentarios($"ISO: {metadata.ISO}", ConsoleColor.White);
            Style.MostrarComentarios($"Focal: {metadata.FocalLength}", ConsoleColor.White);
            Style.MostrarComentarios($"Programa: {metadata.ExposureProgram}", ConsoleColor.White);
            Style.MostrarComentarios($"Medición: {metadata.MeteringMode}", ConsoleColor.White);
            Style.MostrarComentarios($"Flash: {metadata.Flash}", ConsoleColor.White);
            //Información del lente
            Style.MostrarTitulo("Lente", ConsoleColor.DarkYellow);
            Style.MostrarComentarios($"Fabricante: {metadata.LensMake}", ConsoleColor.White);
            Style.MostrarComentarios($"Modelo: {metadata.LensModel}", ConsoleColor.White);
            Style.MostrarComentarios($"Balance blancos: {metadata.WhiteBalance}", ConsoleColor.White);
            Style.MostrarComentarios($"Fuente de luz: {metadata.LightSource}", ConsoleColor.White);
            Style.MostrarComentarios($"Zoom digital: {metadata.DigitalZoomRatio}", ConsoleColor.White);
            //Información GPS
            Style.MostrarTitulo("GPS", ConsoleColor.Yellow);
            Style.MostrarComentarios($"Latitud: {metadata.GPSLatitude}", ConsoleColor.White);
            Style.MostrarComentarios($"Longitud: {metadata.GPSLongitude}", ConsoleColor.White);
            Style.MostrarComentarios($"Altitud: {metadata.GPSAltitude}", ConsoleColor.White);
            Style.MostrarLinea(ConsoleColor.DarkGray);
        }

        //Pausa antes de volver al menu
        private static void VolverAlMenu()
        {
            Style.MostrarComentarios("\nPresione cualquier tecla para volver al menú...",ConsoleColor.White);
            Console.ReadKey();
        }
    }
}
