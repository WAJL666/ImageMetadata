using ImageMetadataTools.css;
using ImageMetadataTools.Models;
using System.Text;

//Guardar información en archivos 
namespace ImageMetadataTools.Services
{
    public class MetadataSaver
    {
        //Guarda los metadatos en un archivo de texto
        public static void GuardarEnArchivo(MetadataInfo metadata)
        {
            string rutaTxt = ObtenerRutaDestino(metadata);
            File.WriteAllText(rutaTxt, GenerarTexto(metadata));

            Style.MostrarComentarios($"Éxito.\nMetadatos guardados en: {rutaTxt}", ConsoleColor.Green);
   }

        //saca los metadatos para retornar la información para guadarlo en texto
        private static string? GenerarTexto(MetadataInfo meta)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Archivo: {meta.FileName}");
            sb.AppendLine($"Peso: {meta.FileSize}");
            sb.AppendLine($"Dimensiones: {meta.Width}x{meta.Height}");
            sb.AppendLine($"Formato: {meta.Format}");
            sb.AppendLine($"Orientación: {meta.Orientation}");
            sb.AppendLine($"Cámara: {meta.CameraMake} {meta.CameraModel}");
            sb.AppendLine($"Software: {meta.Software}");
            sb.AppendLine($"Fecha captura: {meta.DateTaken}");
            sb.AppendLine($"Fecha digitalización: {meta.DateDigitized}");
            sb.AppendLine($"Exposición: {meta.ExposureTime}");
            sb.AppendLine($"Apertura: {meta.Aperture}");
            sb.AppendLine($"ISO: {meta.ISO}");
            sb.AppendLine($"Distancia focal: {meta.FocalLength}");
            sb.AppendLine($"Programa de exposición: {meta.ExposureProgram}");
            sb.AppendLine($"Medición de luz: {meta.MeteringMode}");
            sb.AppendLine($"Flash: {meta.Flash}");
            sb.AppendLine($"Lente: {meta.LensMake} {meta.LensModel}");
            sb.AppendLine($"Balance blancos: {meta.WhiteBalance}");
            sb.AppendLine($"Fuente de luz: {meta.LightSource}");
            sb.AppendLine($"Zoom digital: {meta.DigitalZoomRatio}");
            sb.AppendLine($"GPS: {meta.GPSLatitude}, {meta.GPSLongitude}, Altitud: {meta.GPSAltitude}");

            return sb.ToString();
        }

        //Obtener la ruta destino para el archivo de metadatos
        private static string ObtenerRutaDestino(MetadataInfo metadata)
        {
            string carpeta = Path.GetDirectoryName(metadata.FullPack) ?? ".";
            string nombreArchivo = Path.GetFileNameWithoutExtension(metadata.FileName);
            return Path.Combine(carpeta, $"{nombreArchivo}_metadatos.txt");
        }
    }
}