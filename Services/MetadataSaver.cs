using ImageMetadataTools.Models;
using ImageMetadataTools.UI;

//Guardar información en archivos 
namespace ImageMetadataTools.Services
{
    public class MetadataSaver
    {
        public static void GuardarEnArchivo(MetadataInfo metadata)
        {
            // Carpeta donde se guardará (la misma de la imagen)
            if (metadata == null)
            {
                MenuPrincipal ui=new ();
               ui.MostrarError("No hay metadatos cargados. Primero procese una imagen.");
                return;
            }
            string carpeta = Path.GetDirectoryName(metadata.FullPack) ?? ".";
            string nombreArchivo = Path.GetFileNameWithoutExtension(metadata.FileName);
            string rutaTxt = Path.Combine(carpeta, nombreArchivo + "_metadatos.txt");

            File.WriteAllText(rutaTxt, GenerarTexto(metadata));

            Console.WriteLine($" Metadatos guardados en: {rutaTxt}");
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
    }
}