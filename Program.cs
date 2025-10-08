
using ImageMetadataTools.Models;
using ImageMetadataTools.Services;


namespace ImageMetadataTools

{
    internal class Program
    {
        private static MetadataInfo metadata;
        static void Main(string[] args)
        {

            int option;
            do
            {
                Console.Clear();
                Console.WriteLine("========== Lector de Metadatos ==========");
                Console.WriteLine("1. Procesar imagen básica");
                Console.WriteLine("2. Procesar imagen completa");
                Console.WriteLine("3. Guardar metadatos en archivo");
                Console.WriteLine("4. Salir");
                Console.Write("\nSeleccione una opción: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        ProcesarImagenBasico();
                        break;
                    case 2:
                        ProcesarImagenExif();
                        break;
                    case 3:
                        GuardarInformacion();
                        break;
                    case 4:
                        Console.WriteLine("👋 Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }

            } while (option != 4);     
        }

        //Opcion 3. guarda metadatos en archivo plano txt
        private static void GuardarInformacion()
        {
            if (metadata == null) {
                Console.WriteLine("❌ No hay metadatos cargados. Primero procese una imagen.");
                VolverAlMenu();
                return;
            }
            string ruta = metadata.FileName;
            File.WriteAllText(ruta, GenerarTexto(metadata));

            Console.WriteLine($" Metadatos guardados en: {ruta}");
            VolverAlMenu();
        }

        //saca los metadatos para retornar la información para guadarlo en texto
        private static string? GenerarTexto(MetadataInfo meta)
        {
            return
                 $"Archivo: {meta.FileName}\n" +
                 $"Peso: {meta.FileSize}\n" +
                 $"Dimensiones: {meta.Width}x{meta.Height}\n" +
                 $"Formato: {meta.Format}\n" +
                 $"Orientación: {meta.Orientation}\n" +
                 $"Cámara: {meta.CameraMake} {meta.CameraModel}\n" +
                 $"Software: {meta.Software}\n" +
                 $"Fecha captura: {meta.DateTaken}\n" +
                 $"Fecha digitalización: {meta.DateDigitized}\n" +
                 $"Exposición: {meta.ExposureTime}\n" +
                 $"Apertura: {meta.Aperture}\n" +
                 $"ISO: {meta.ISO}\n" +
                 $"Distancia focal: {meta.FocalLength}\n" +
                 $"Programa de exposición: {meta.ExposureProgram}\n" +
                 $"Medición de luz: {meta.MeteringMode}\n" +
                 $"Flash: {meta.Flash}\n" +
                 $"Lente: {meta.LensMake} {meta.LensModel}\n" +
                 $"Balance blancos: {meta.WhiteBalance}\n" +
                 $"Fuente de luz: {meta.LightSource}\n" +
                 $"Zoom digital: {meta.DigitalZoomRatio}\n" +
                 $"GPS: {meta.GPSLatitude}, {meta.GPSLongitude}, Altitud: {meta.GPSAltitude}";
        }

        //Opcion 2, muestra los metadatos
        private static void ProcesarImagenExif()
        {
            string imagePath = PedirRuta();
            if (!validarArchivo(imagePath)) return;
            MetadataReader reader = new (); //instanciamos
            metadata = reader.GetMetadata(imagePath);//ruta de la imagen y saca información
            if (metadata != null)
            {
               
                Console.WriteLine("\n=========== Información EXIF ===========");
                Console.WriteLine("\n¨¨¨¨¨¨¨¨¨¨Información de la cámara¨¨¨¨¨¨¨¨¨¨");
                Console.WriteLine("Fabricante de la cámara: " + metadata.CameraMake);
                Console.WriteLine("Modelo de la cámara: " + metadata.CameraModel);
                Console.WriteLine("Software que generó la foto: " + metadata.Software);

                Console.WriteLine("\n¨¨¨¨¨¨¨¨¨¨Información de la fotografía¨¨¨¨¨¨¨¨¨¨");
                Console.WriteLine("Fecha de captura: " + metadata.DateTaken);
                Console.WriteLine("Fecha digitalización: " + metadata.DateDigitized);
                Console.WriteLine("Tiempo de exposición: " + metadata.ExposureTime);
                Console.WriteLine("Apertura: " + metadata.Aperture);
                Console.WriteLine("Iso: " + metadata.ISO);
                Console.WriteLine("Distancia focal: " + metadata.FocalLength);
                Console.WriteLine("Programa de exposición: " + metadata.ExposureProgram);
                Console.WriteLine("Medición de luz: " + metadata.LightSource);
                Console.WriteLine("Flash usado (sí/no): " + metadata.Flash);

                Console.WriteLine("\n¨¨¨¨¨¨¨¨¨¨Información del lente¨¨¨¨¨¨¨¨¨¨");
                Console.WriteLine("Fabricante del lente: " + metadata.LensMake);
                Console.WriteLine("Modelo del lente: " + metadata.LensModel);
                Console.WriteLine("Balance de blancos: " + metadata.WhiteBalance);
                Console.WriteLine("LightSource: " + metadata.LightSource);
                Console.WriteLine("Zoom: " + metadata.DigitalZoomRatio);

                Console.WriteLine("\n¨¨¨¨¨¨¨¨¨¨Información del GPS¨¨¨¨¨¨¨¨¨¨");
                Console.WriteLine("Latitud: " + metadata.GPSLatitude);
                Console.WriteLine("Longitud: " + metadata.GPSLongitude);
                Console.WriteLine("Altitud: " + metadata.GPSAltitude);

            }
            else
            {
                Console.WriteLine("❌ No se pudieron leer los metadatos EXIF.");
            }

            VolverAlMenu();
        }
       

        // Opcion 1, muestra Información basica
        private static void ProcesarImagenBasico()
        {
            string imagePath = PedirRuta();
            MetadataReader reader = new (); //instanciamos
            metadata = reader.GetMetadata(imagePath);//ruta de la imagen y saca información
            if (metadata != null)
            {
                Console.WriteLine("\n\n¨¨¨¨¨¨¨¨¨¨Información Basica¨¨¨¨¨¨¨¨¨¨");
                Console.WriteLine("Archivo: " + metadata.FileName);
                Console.WriteLine("Peso: " + metadata.FileSize);
                Console.WriteLine("Dimensiones: " + metadata.Width + " x " + metadata.Height);
                Console.WriteLine("Formato: " + metadata.Format);
                Console.WriteLine("Orientación: " + metadata.Orientation);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("❌ No se pudieron leer los metadatos.");
            }
        }
        //vuelve al menu
        static void VolverAlMenu()
        {
            //Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        // Leer ruta desde la consola  D:\imagenes\Fotos\Fotos\Emily.JPG
        private static string PedirRuta()
        {
            Console.Write("\n\nIngrese la ruta completa de la imagen: ");
            return Console.ReadLine();
        }

        //valida si un archivo existe y su formato
        private static bool validarArchivo(string imagePath)
        {
            //archivo existe?
           if (!System.IO.File.Exists(imagePath)) {
                Console.WriteLine("❌ El archivo no existe.");
                Console.ReadKey();
            }
           
           string ext = System.IO.Path.GetExtension(imagePath.ToLower());//sacamos la extención
            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".tiff"))
            {
                Console.WriteLine("❌ El archivo no es una imagen válida.");
                Console.ReadKey();
                return false;
            }
            return true;
        }
    }
}
