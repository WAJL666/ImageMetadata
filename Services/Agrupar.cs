using ImageMetadataTools.Models;
using ImageMetadataTools.Services;
using ImageMetadataTools.UI;

namespace ImageMetadataTools.Services
{
   
    public class Agrupar
    {
        private static MetadataInfo? info = null;
        public static void AgruparPorFecha()
        {
            string ruta = MenuPrincipal.PedirRuta();
            var imagenes=Manage.ObtenerImagenesValidas(ruta);
            foreach (var foto in imagenes)
            {
                info = MetadataReader.GetMetadata(Path.Combine(ruta, Path.GetFileName(foto)));
                Console.WriteLine(  "Hola");
                if (info != null)
                {
                    string fechaCarpeta = info.DateTaken ?? "SinFecha";
                    if (DateTime.TryParse(info.DateTaken, out DateTime fecha))
                    {
                        fechaCarpeta = fecha.ToString("yyyy-MM-dd_HH-mm-ss");
                    }
                    string rutaCarpetaDestino = Path.Combine(ruta, fechaCarpeta);
                    if (!Directory.Exists(rutaCarpetaDestino))
                    {
                        Directory.CreateDirectory(rutaCarpetaDestino);
                    }
                    string rutaArchivoDestino = Path.Combine(rutaCarpetaDestino, Path.GetFileName(foto));
                    if (!File.Exists(rutaArchivoDestino))
                    {
                        File.Move(foto, rutaArchivoDestino);
                    }
                }
            }
        }
    }
}
//2.Solución(manteniendo DateTaken como string)

//Modifica la línea donde generas fechaCarpeta así:

//string fechaCarpeta = info.DateTaken ?? "SinFecha";

//// intentar parsear la fecha
//if (DateTime.TryParse(info.DateTaken, out DateTime fecha))
//{
//    fechaCarpeta = fecha.ToString("yyyy-MM-dd_HH-mm-ss");
//}
//else
//{
//    // si la fecha no es válida, usa un valor por defecto
//    fechaCarpeta = "SinFecha";
//}


//Y el bloque completo quedaría así:

//public static void AgruparPorFecha()
//{
//    string ruta = MenuPrincipal.PedirRuta();
//    var imagenes = Manage.ObtenerImagenesValidas(ruta);

//    foreach (var foto in imagenes)
//    {
//        info = MetadataReader.GetMetadata(Path.Combine(ruta, Path.GetFileName(foto)));

//        if (info != null)
//        {
//            string fechaCarpeta = info.DateTaken ?? "SinFecha";

//            if (DateTime.TryParse(info.DateTaken, out DateTime fecha))
//            {
//                fechaCarpeta = fecha.ToString("yyyy-MM-dd_HH-mm-ss");
//            }

//            // Limpiar caracteres inválidos del nombre
//            fechaCarpeta = LimpiarNombre(fechaCarpeta);

//            string rutaCarpetaDestino = Path.Combine(ruta, fechaCarpeta);

//            if (!Directory.Exists(rutaCarpetaDestino))
//                Directory.CreateDirectory(rutaCarpetaDestino);

//            string rutaArchivoDestino = Path.Combine(rutaCarpetaDestino, Path.GetFileName(foto));

//            if (!File.Exists(rutaArchivoDestino))
//                File.Move(foto, rutaArchivoDestino);
//        }
//    }
//}

//private static string LimpiarNombre(string nombre)
//{
//    foreach (char c in Path.GetInvalidFileNameChars())
//    {
//        nombre = nombre.Replace(c, '_');
//    }
//    return nombre;
//}