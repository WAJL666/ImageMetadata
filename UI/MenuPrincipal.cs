using ImageMetadataTools.css;
using ImageMetadataTools.Models;
using ImageMetadataTools.Services;

namespace ImageMetadataTools.UI
{
    public class MenuPrincipal
    {
        private static MetadataInfo? metadata = null;
        public static string rutaDestino = string.Empty;

        public void Inicio()
        {
            int option;
            do
            {
               Style.MostrarMenu();

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Style.MostrarError("Entrada inválida. Debe ser un número.");
                    continue;
                }

                switch (option)
                {
                    case 1: 
                        rutaDestino = PedirRuta();
                        ProcesarImagenExif(rutaDestino); 
                        break;
                    case 2:
                        GuardarInformacion(); 
                        break;
                    case 3: 
                        IngresarCarpeta(); 
                        break;
                    case 4:
                        int subopcion = Style.MostrarMenuAgrupar();
                        switch (subopcion)
                        {
                            case 1:
                                Console.WriteLine("\nOpcion 1");
                                //AgruparPorFecha(imagenes, rutaDestino);
                                break;
                            case 2:
                                Console.WriteLine("\nOpcion 2");
                                //AgruparPorLugar(imagenes, rutaDestino);
                                break;
                            default:
                                Style.MostrarError("Opción no válida en el submenú.");
                                break;
                        }
                        break;
                    case 5:
                        Style.MostrarComentarios("\nGracias por usar el lector de metadatos. ¡Hasta pronto!", ConsoleColor.Green);
                        Environment.Exit(0);
                        break;
                    default:
                        Style.MostrarError("Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }

            } while (option != 5);
        }

        //Opcion 3, ingresa carpeta
        private static void IngresarCarpeta()
        {
           rutaDestino = PedirRuta();            
           Manage.IniciarExploracion(rutaDestino);
        }

        //Opcion 2. guarda metadatos en archivo plano txt
        private static void GuardarInformacion()
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
        public static void ProcesarImagenExif(string ruta)
        {
            if (!ValidarArchivo(rutaDestino)) return;

            metadata = MetadataReader.GetMetadata(rutaDestino);
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
            Console.ResetColor();
            Style.MostrarLinea(ConsoleColor.DarkGray);
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
            Style.MostrarLinea(ConsoleColor.DarkGray);
        }

        //vuelve al menu
        public static void VolverAlMenu()
        {
            Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        private static string PedirRuta()
        {
            Style.MostrarComentarios("\nIngrese la ruta completa del archivo o carpeta.", ConsoleColor.Yellow);
            return Console.ReadLine().Trim('"');
        }

     
        public static bool ValidarArchivo(string imagePath)
        {
            int msm = 0;
            if (!File.Exists(imagePath))
            {
                msm = 1;
                return false;
            }

            string ext = Path.GetExtension(imagePath).ToLowerInvariant();
            string[] extensionesValidas = [".jpg", ".jpeg", ".png", ".bmp", ".tiff"];

            if (!extensionesValidas.Contains(ext))
            {
                msm = 2;
                return false;
            }
            if (msm == 1)
            {
                Style.MostrarError("El archivo no existe. Intente de nuevo.");

            }
            else if (msm == 2)
            {
                Style.MostrarError("Formato no soportado. Use JPG, PNG, BMP o TIFF.");
            }
            return true;
        }

    }
}