using ImageMetadataTools.css;
using ImageMetadataTools.Models;
using ImageMetadataTools.Services;

namespace ImageMetadataTools.UI
{
    public class MenuPrincipal
    {
        private MetadataInfo? metadata = null;

        public void Inicio()
        {
            int option;
            do
            {
                MostrarMenu();

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("❌ Entrada inválida. Debe ser un número.");
                    continue;
                }

                switch (option)
                {
                    case 1: ProcesarImagenExif(); 
                        break;
                    case 2: GuardarInformacion(); 
                        break;
                    case 3: IngresarCarpeta(); 
                        break;
                    case 4:
                        Console.WriteLine("\nGracias por usar el lector de metadatos. ¡Hasta pronto!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }

            } while (option != 4);
        }

        //muestra el menu
        private static void MostrarMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("╔═════════════════════════════════════ Lector De Metadatos ═══════════════════════════════════╗");
            Console.WriteLine("║ 1. Procesar imagen. ║ 2. Guardar metadatos en archivo. ║ 3. Buscar Carpeta. ║ 4. Salir.     ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════╝");
            Console.Write("\nSeleccione una opción: ");
        }

        //Opcion 3, ingresa carpeta
        private static void IngresarCarpeta()
        {
            string rutaCarpeta = PedirRuta();
            Manage.ImageManage(rutaCarpeta);
        }

        //Opcion 2. guarda metadatos en archivo plano txt
        private void GuardarInformacion()
        {
            if (metadata == null)
            {
                Style.MostrarError("No hay metadatos para guardar. Procese una imagen primero.");
                return;
            }
            MetadataSaver.GuardarEnArchivo(metadata);
            VolverAlMenu();
        }

        //Opcion 1, muestra los metadatos
        private void ProcesarImagenExif()
        {
            string imagePath = PedirRuta();
            if (!ValidarArchivo(imagePath)) return;

            metadata = MetadataReader.GetMetadata(imagePath);
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

        //muestra los metadatos en consola
        private static void MostrarMetadatos(MetadataInfo metadata)
        {
            Style.MostrarLiena();
            Style.MostrarTitulo("Información Básica", ConsoleColor.Cyan);
            //Información básica
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
            Style.MostrarLiena();
        }

        //vuelve al menu
        public static void VolverAlMenu()
        {
            Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        private static string PedirRuta()
        {
            Console.Write("\n\nIngrese la ruta: ");
            return Console.ReadLine().Trim('"');
        }

        //valida si un archivo existe y su formato
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
    }
}