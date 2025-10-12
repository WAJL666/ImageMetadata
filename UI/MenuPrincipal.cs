using ImageMetadataTools.Models;
using ImageMetadataTools.Services;

namespace ImageMetadataTools.UI
{
    internal class MenuPrincipal
    {
        private MetadataInfo metadata = null;

        public void Inicio()
        {
            #region Menu

            int option;
            do
            {
                Console.WriteLine("\n");
                Console.WriteLine("╔═══════════════════════════ Lector De Metadatos ════════════════════════╗");
                Console.WriteLine("║ 1. Procesar imágen. ║ 2. Guardar metadatos en archivo.  ║ 3. Salir.    ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════╝");
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
                        Console.WriteLine("\nGracias por usar el lector de metadatos. ¡Hasta pronto!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("❌ Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }

            } while (option != 3);
        }

        //Opcion 2. guarda metadatos en archivo plano txt
        void GuardarInformacion()
        {
            if (metadata == null)
            {
                MostrarError("No hay metadatos cargados. Primero procese una imagen.");
                return;
            }
            string ruta = metadata.FileName;
            File.WriteAllText(ruta, GenerarTexto(metadata));

            Console.WriteLine($" Metadatos guardados en: {ruta}");
            VolverAlMenu();
        }

        //saca los metadatos para retornar la información para guadarlo en texto
        string? GenerarTexto(MetadataInfo meta)
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

        //Opcion 1, muestra los metadatos
        void ProcesarImagenExif()
        {
            string imagePath = PedirRuta();
            if (!ValidarArchivo(imagePath)) return;
            MetadataReader reader = new(); //instanciamos
            metadata = MetadataReader.GetMetadata(imagePath);//ruta de la imagen y saca información
            if (metadata != null)
            {
                MostrarLiena();
                MostrarTitulo("Información Basica", ConsoleColor.Cyan);
                Console.WriteLine("Archivo: " + metadata.FileName);
                Console.WriteLine("Peso: " + metadata.FileSize);
                Console.WriteLine("Dimensiones: " + metadata.Width + " x " + metadata.Height);
                Console.WriteLine("Formato: " + metadata.Format);
                Console.WriteLine("Orientación: " + metadata.Orientation);

                MostrarTitulo("Información de la cámara", ConsoleColor.Magenta);
                Console.WriteLine("Fabricante de la cámara: " + metadata.CameraMake);
                Console.WriteLine("Modelo de la cámara: " + metadata.CameraModel);
                Console.WriteLine("Software que generó la foto: " + metadata.Software);

                MostrarTitulo("Información de la fotografía", ConsoleColor.Blue);
                Console.WriteLine("Fecha de captura: " + metadata.DateTaken);
                Console.WriteLine("Fecha digitalización: " + metadata.DateDigitized);
                Console.WriteLine("Tiempo de exposición: " + metadata.ExposureTime);
                Console.WriteLine("Apertura: " + metadata.Aperture);
                Console.WriteLine("Iso: " + metadata.ISO);
                Console.WriteLine("Distancia focal: " + metadata.FocalLength);
                Console.WriteLine("Programa de exposición: " + metadata.ExposureProgram);
                Console.WriteLine("Medición de luz: " + metadata.LightSource);
                Console.WriteLine("Flash usado (sí/no): " + metadata.Flash);

                MostrarTitulo("Información del lente", ConsoleColor.DarkYellow);
                Console.WriteLine("Fabricante del lente: " + metadata.LensMake);
                Console.WriteLine("Modelo del lente: " + metadata.LensModel);
                Console.WriteLine("Balance de blancos: " + metadata.WhiteBalance);
                Console.WriteLine("LightSource: " + metadata.LightSource);
                Console.WriteLine("Zoom: " + metadata.DigitalZoomRatio);

                MostrarTitulo("Información del GPS", ConsoleColor.Yellow);
                Console.WriteLine("Latitud: " + metadata.GPSLatitude);
                Console.WriteLine("Longitud: " + metadata.GPSLongitude);
                Console.WriteLine("Altitud: " + metadata.GPSAltitude);
                MostrarLiena();
            }
            else
            {
                MostrarError("No se pudieron leer los metadatos EXIF");
            }

            VolverAlMenu();
        }

        #endregion Menu

        #region colores
        //Aplicar color a la linea 
        private void MostrarLiena()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;//aplicar color
            Console.WriteLine("\n──────────────────────────────────────────────────────────────");
            Console.ResetColor(); //resetea el color
        }

        //aplicar color al titulo
        private void MostrarTitulo(string titulo, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"─────────────────────────────────────────────────────");
            Console.WriteLine($"               {titulo}");
            Console.WriteLine($"─────────────────────────────────────────────────────");
            Console.ResetColor();
        }
        //aplicar color al error
        public void MostrarError(string v)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(v);
            Console.ResetColor();
            VolverAlMenu();
        }

        #endregion colores

        //vuelve al menu
        private void VolverAlMenu()
        {
            //Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        // Leer ruta desde la consola  D:\imagenes\Fotos\Fotos\Emily.JPG
        private string PedirRuta()
        {
            Console.Write("\n\nIngrese la ruta completa de la imagen: ");
            return Console.ReadLine();
        }

        //valida si un archivo existe y su formato
        private bool ValidarArchivo(string imagePath)
        {
            //archivo existe?
            if (!System.IO.File.Exists(imagePath))
            {
                MostrarError("El archivo no existe. Intente de nuevo.");
            }

            string ext = System.IO.Path.GetExtension(imagePath.ToLower());//sacamos la extención
            if (!(ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".tiff"))
            {
                MostrarError("Formato no soportado. Use JPG, PNG, BMP o TIFF.");
                return false;
            }
            return true;
        }

    }
}