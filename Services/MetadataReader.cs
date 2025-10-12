using ImageMetadataTools.Models;      // Para acceder a MetadataInfo
using System.Drawing;                   // Para manejar imágenes
using System.Drawing.Imaging;
using ImageMetadataTools.UI;         // Para mostrar mensajes de error  

// lectura de información
namespace ImageMetadataTools.Services
{
    public class MetadataReader
    {
        public static MetadataInfo? GetMetadata(string imagePath)
        {
            MenuPrincipal uI = new ();
            try
            {
                FileInfo fileInfo = new(imagePath);
                using Image img = Image.FromFile(imagePath);

                // Estableciendo todas las propiedades.
                MetadataInfo info = new()
                {
                    FileName = Path.GetFileName(imagePath),
                    FileSize = $"{fileInfo.Length / 1024.0:F2} KB",
                    Width = img.Width,
                    Height = img.Height,
                    Format = img.RawFormat.ToString(),
                    Orientation = 0,
                    CameraMake = string.Empty,
                    CameraModel = string.Empty,
                    Software = string.Empty,
                    DateTaken = string.Empty,
                    DateDigitized = string.Empty,
                    ExposureTime = string.Empty,
                    Aperture = string.Empty,
                    ISO = 0,
                    FocalLength = string.Empty,
                    ExposureProgram = string.Empty,
                    MeteringMode = string.Empty,
                    Flash = string.Empty,
                    GPSLatitude = string.Empty,
                    GPSLongitude = string.Empty,
                    GPSAltitude = string.Empty,
                    LensMake = string.Empty,
                    LensModel = string.Empty,
                    WhiteBalance = string.Empty,
                    LightSource = string.Empty,
                    DigitalZoomRatio = string.Empty
                };

                foreach (PropertyItem prop in img.PropertyItems)
                {
                    switch (prop.Id)
                    {
                        case 0x010F: info.CameraMake = GetString(prop); break;
                        case 0x0110: info.CameraModel = GetString(prop); break;
                        case 0x0131: info.Software = GetString(prop); break;
                        // Información de la fotografía
                        case 0x0132: info.DateTaken = GetString(prop); break;
                        case 0x9004: info.DateDigitized = GetString(prop); break;
                        case 0x829A: info.ExposureTime = GetRationalString(prop); break;
                        case 0x829D: info.Aperture = GetRationalString(prop); break;
                        case 0x8827: info.ISO = GetShort(prop); break;
                        case 0x920A: info.FocalLength = GetRationalString(prop); break;
                        case 0x0112: info.Orientation = GetShort(prop); break;
                        case 0x8822: info.ExposureProgram = GetShort(prop).ToString(); break;
                        case 0x9207: info.MeteringMode = GetShort(prop).ToString(); break;
                        case 0x9209: info.Flash = GetShort(prop) == 0 ? "No" : "Sí"; break;
                        // Información del lente
                        case 0xA433: info.LensMake = GetString(prop); break;
                        case 0xA434: info.LensModel = GetString(prop); break;
                        case 0xA403: info.WhiteBalance = GetShort(prop).ToString(); break;
                        case 0x9208: info.LightSource = GetShort(prop).ToString(); break;
                        case 0xA404: info.DigitalZoomRatio = GetRationalString(prop); break;
                        // Información GPS
                        case 0x0002: info.GPSLatitude = GetGPS(prop) ?? string.Empty; break;
                        case 0x0004: info.GPSLongitude = GetGPS(prop) ?? string.Empty; break;
                        case 0x0006: info.GPSAltitude = GetRationalString(prop); break;
                    }
                }
                return info;
            }
            catch (FileNotFoundException rutaNoExiste)
            {
                // Captura si la ruta no existe o el archivo no está en la ubicación indicada.
                uI.MostrarError("No se encontró la imagen. Verifique la ruta e intente nuevamente.");
                Console.WriteLine(rutaNoExiste.Message);
                return null;
            }
            catch (OutOfMemoryException imagenNoValida)
            {
                // Captura si el archivo no es una imagen válida.
                uI.MostrarError("El archivo no es una imagen válida o está corrupto.");
                Console.WriteLine(imagenNoValida.Message);
                return null;
            }
            catch (Exception ex)
            {
                // Cualquier otro error inesperado
                uI.MostrarError("Ocurrió un error inesperado al procesar la imagen.");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /*
         * GetString         → convierte texto.
           GetRationalString → convierte fracciones a número legible.
           GetShort          → convierte valores pequeños a enteros.
           GetGPS            → convierte coordenadas GPS en grados decimales.
         */

        /* Convierte los bytes (prop.Value) a un string usando codificación ASCII.
           Trim('\0') elimina los caracteres nulos (\0) que muchas veces vienen al final del texto.*/
        private static string GetString(PropertyItem prop)
        {
            return System.Text.Encoding.ASCII.GetString(prop.Value).Trim('\0');
        }

        /*Muchos datos EXIF como la apertura, tiempo de exposición o longitud focal están guardados como una fracción (rational):
         * numerador/denominador.
         */
        private static string GetRationalString(PropertyItem prop)
        {
            //BitConverter es la herramienta para traducir bytes → números.
            uint numerator = BitConverter.ToUInt32(prop.Value, 0);
            uint denominator = BitConverter.ToUInt32(prop.Value, 4);
            if (denominator == 0) return "0";
            return ((double)numerator / denominator).ToString();
        }
        /*Convierte los primeros 2 bytes (UInt16) en un número entero.
         Se usa en EXIF para datos pequeños como orientación, ISO, flash, balance de blancos.*/
        private static int GetShort(PropertyItem prop) => BitConverter.ToUInt16(prop.Value, 0);

        /*Convierte las coordenadas GPS que están en formato grados, minutos, segundos (DMS) a un número decimal más fácil de usar.
        Divide cada parte (grados, minutos, segundos) por su denominador y suma la conversión.*/
        private static string? GetGPS(PropertyItem prop)
        {
            if (prop.Value.Length < 24) return null;

            double deg = BitConverter.ToUInt32(prop.Value, 0) / (double)BitConverter.ToUInt32(prop.Value, 4);
            double min = BitConverter.ToUInt32(prop.Value, 8) / (double)BitConverter.ToUInt32(prop.Value, 12);
            double sec = BitConverter.ToUInt32(prop.Value, 16) / (double)BitConverter.ToUInt32(prop.Value, 20);

            return (deg + (min / 60) + (sec / 3600)).ToString("0.######");
        }
    }
}