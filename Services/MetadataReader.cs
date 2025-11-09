using ImageMetadataTools.css;           // Para mostrar mensajes de error  
using ImageMetadataTools.Models;        // Para acceder a MetadataInfo
using System.Drawing;                   // Para manejar imágenes
using System.Drawing.Imaging;
using System.Text;

// lectura de información
namespace ImageMetadataTools.Services
{
    public class MetadataReader
    {
        // Método principal para obtener los metadatos de una imagen.
        public static MetadataInfo? GetMetadata(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Style.MostrarComentarios("No se encontró la imagen. Verifique la ruta e intente nuevamente.", ConsoleColor.Red);
                return null;
            }

            try
            {
                FileInfo fileInfo = new(imagePath);
                using Image img = Image.FromFile(imagePath);

                MetadataInfo info = InicializarMetadata(imagePath, fileInfo, img);
                ExtraerExif(img, info);

                return info;
            }
            catch (OutOfMemoryException ex)
            {
                Style.MostrarComentarios("El archivo no es una imagen válida o está corrupto.", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Style.MostrarComentarios("Ocurrió un error inesperado al procesar la imagen.", ConsoleColor.Red);
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Inicializa las propiedades básicas de MetadataInfo.
        private static MetadataInfo InicializarMetadata(string imagePath, FileInfo fileInfo, Image img)
        {
            return new MetadataInfo
            {
                FullPack = imagePath,
                FileName = Path.GetFileName(imagePath),
                FileSize = $"{fileInfo.Length / 1024.0:F2} KB",
                Width = img.Width,
                Height = img.Height,
                Format = GetImageFormat(img.RawFormat),
                Orientation = 0,
                CameraMake = "",
                CameraModel = "",
                Software = "",
                DateTaken = "",
                DateDigitized = "",
                ExposureTime = "",
                Aperture = "",
                ISO = 0,
                FocalLength = "",
                ExposureProgram = "",
                MeteringMode = "",
                Flash = "",
                GPSLatitude = "",
                GPSLongitude = "",
                GPSAltitude = "",
                LensMake = "",
                LensModel = "",
                WhiteBalance = "",
                LightSource = "",
                DigitalZoomRatio = ""
            };
        }

        // Extrae los datos EXIF específicos y los asigna a las propiedades correspondientes en MetadataInfo.
        private static void ExtraerExif(Image img, MetadataInfo info)
        {
            foreach (PropertyItem prop in img.PropertyItems)
            {
                switch (prop.Id)
                {
                    // Información de la cámara
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
                    case 0x0002: info.GPSLatitude = GetGPS(prop) ?? ""; break;
                    case 0x0004: info.GPSLongitude = GetGPS(prop) ?? ""; break;
                    case 0x0006: info.GPSAltitude = GetRationalString(prop); break;
                }
            }
        }

        // Convierte el formato de imagen a un string legible.
        private static string GetImageFormat(ImageFormat format)
        {
            if (format.Equals(ImageFormat.Jpeg)) return "JPEG";
            if (format.Equals(ImageFormat.Png)) return "PNG";
            if (format.Equals(ImageFormat.Bmp)) return "BMP";
            if (format.Equals(ImageFormat.Gif)) return "GIF";
            if (format.Equals(ImageFormat.Tiff)) return "TIFF";
            return "Desconocido";
        }

        // Convierte los bytes (prop.Value) a un string usando codificación ASCII.
        private static string GetString(PropertyItem prop)
        {
            return Encoding.ASCII.GetString(prop.Value).Trim('\0');
        }

        // Muchos datos EXIF como la apertura, tiempo de exposición o longitud focal están guardados como una fracción (rational): numerador/denominador.
        private static string GetRationalString(PropertyItem prop)
        {
            if (prop.Value.Length < 8) return "0";
            uint numerator = BitConverter.ToUInt32(prop.Value, 0);
            uint denominator = BitConverter.ToUInt32(prop.Value, 4);
            return denominator == 0 ? "0" : ((double)numerator / denominator).ToString("0.###");
        }

        // Convierte los primeros 2 bytes (UInt16) en un número entero.
        private static int GetShort(PropertyItem prop)
        {
            return prop.Value.Length >= 2 ? BitConverter.ToUInt16(prop.Value, 0) : 0;
        }

        // Convierte las coordenadas GPS que están en formato grados, minutos, segundos (DMS) a un número decimal más fácil de usar.
        private static string? GetGPS(PropertyItem prop)
        {
            if (prop.Value.Length < 24) return null;

            double deg = BitConverter.ToUInt32(prop.Value, 0) / (double)BitConverter.ToUInt32(prop.Value, 4);
            double min = BitConverter.ToUInt32(prop.Value, 8) / (double)BitConverter.ToUInt32(prop.Value, 12);
            double sec = BitConverter.ToUInt32(prop.Value, 16) / (double)BitConverter.ToUInt32(prop.Value, 20);

            return (deg + (min / 60) + (sec / 3600)).ToString("0.######");
        }

          /*
           GetString         → convierte texto.
           GetRationalString → convierte fracciones a número legible.
           GetShort          → convierte valores pequeños a enteros.
           GetGPS            → convierte coordenadas GPS en grados decimales.
         */
    }
}