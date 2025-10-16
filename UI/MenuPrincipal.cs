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
            #region Menu
            int option;
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("╔═══════════════════════════ Lector De Metadatos ═════════════════════════════════════════════╗");
                Console.WriteLine("║ 1. Procesar imágen. ║ 2. Guardar metadatos en archivo. ║ 3. Buscar Carpeta.  ║ 4. Salir.    ║");
                Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════════════════╝");
                Console.Write("\nSeleccione una opción: ");
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        ProcesarImagenExif();
                        break;
                    case 2:
                        GuardarInformacion();
                        break;
                        case 3:
                        IngresarCarpeta();
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

        private void IngresarCarpeta()
        {
            string rutaCarpeta=PedirRuta();
            Manage.ImageManage(rutaCarpeta);
        }

        //Opcion 2. guarda metadatos en archivo plano txt
        void GuardarInformacion()
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
        void ProcesarImagenExif()
        {
            string imagePath = PedirRuta();
            if (!ValidarArchivo(imagePath)) return;
            _ = new MetadataReader(); //instanciamos
            metadata = MetadataReader.GetMetadata(imagePath);//ruta de la imagen y saca información
            if (metadata != null)
            {
                Style.MostrarLiena();
                //Información básica
                Style.MostrarTitulo("Información Basica", ConsoleColor.Cyan);
                Console.WriteLine("Archivo: " + metadata.FileName);
                Console.WriteLine("Peso: " + metadata.FileSize);
                Console.WriteLine("Dimensiones: " + metadata.Width + " x " + metadata.Height);
                Console.WriteLine("Formato: " + metadata.Format);
                Console.WriteLine("Orientación: " + metadata.Orientation);
                //Información de la cámara
                Style.MostrarTitulo("Información de la cámara", ConsoleColor.Magenta);
                Console.WriteLine("Fabricante de la cámara: " + metadata.CameraMake);
                Console.WriteLine("Modelo de la cámara: " + metadata.CameraModel);
                Console.WriteLine("Software que generó la foto: " + metadata.Software);
                //Información de la fotografía
                Style.MostrarTitulo("Información de la fotografía", ConsoleColor.Blue);
                Console.WriteLine("Fecha de captura: " + metadata.DateTaken);
                Console.WriteLine("Fecha digitalización: " + metadata.DateDigitized);
                Console.WriteLine("Tiempo de exposición: " + metadata.ExposureTime);
                Console.WriteLine("Apertura: " + metadata.Aperture);
                Console.WriteLine("Iso: " + metadata.ISO);
                Console.WriteLine("Distancia focal: " + metadata.FocalLength);
                Console.WriteLine("Programa de exposición: " + metadata.ExposureProgram);
                Console.WriteLine("Medición de luz: " + metadata.LightSource);
                Console.WriteLine("Flash usado (sí/no): " + metadata.Flash);
                //Información del lente
                Style.MostrarTitulo("Información del lente", ConsoleColor.DarkYellow);
                Console.WriteLine("Fabricante del lente: " + metadata.LensMake);
                Console.WriteLine("Modelo del lente: " + metadata.LensModel);
                Console.WriteLine("Balance de blancos: " + metadata.WhiteBalance);
                Console.WriteLine("LightSource: " + metadata.LightSource);
                Console.WriteLine("Zoom: " + metadata.DigitalZoomRatio);
                //Información GPS
                Style.MostrarTitulo("Información del GPS", ConsoleColor.Yellow);
                Console.WriteLine("Latitud: " + metadata.GPSLatitude);
                Console.WriteLine("Longitud: " + metadata.GPSLongitude);
                Console.WriteLine("Altitud: " + metadata.GPSAltitude);
                Style.MostrarLiena();
            }
            else
            {
                Style.MostrarError("No se pudieron leer los metadatos EXIF");
            }
            VolverAlMenu();
        }
        #endregion Menu

        //vuelve al menu
        public static void VolverAlMenu()
        {
            //Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        // Leer ruta desde la consola  D:\imagenes\Fotos\Fotos\Emily.JPG
        private static string PedirRuta()
        {
            Console.Write("\n\nIngrese la ruta: ");
            return Console.ReadLine().Trim('"');
        }

        //valida si un archivo existe y su formato
       public static bool ValidarArchivo(string imagePath)
        {
            //archivo existe?
            if (!System.IO.File.Exists(imagePath))
            {
                Style.MostrarError("El archivo no existe. Intente de nuevo.");
            }

            string ext = System.IO.Path.GetExtension(imagePath.ToLower());//sacamos la extención
            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".tiff"))
            {
                Style.MostrarError("Formato no soportado. Use JPG, PNG, BMP o TIFF.");
                return false;
            }
            return true;
        }
    }
}