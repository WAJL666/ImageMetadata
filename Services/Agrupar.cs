using ImageMetadataTools.css;
using System.Drawing;

namespace ImageMetadataTools.Services
{
    public class Agrupar
    {
        //private static MetadataInfo? info = null;
        //public static void AgruparPorFecha()
        //{
        //    string ruta = MenuPrincipal.PedirRuta();
        //    var imagenes=Manage.ObtenerImagenesValidas(ruta);
        //    foreach (var foto in imagenes)
        //    {
        //        info = MetadataReader.GetMetadata(Path.Combine(ruta, Path.GetFileName(foto)));
        //        Console.WriteLine(  "Hola");
        //        if (info != null)
        //        {
        //            string fechaCarpeta = info.DateTaken ?? "SinFecha";
        //            if (DateTime.TryParse(info.DateTaken, out DateTime fecha))
        //            {
        //                fechaCarpeta = fecha.ToString("yyyy-MM-dd_HH-mm-ss");
        //            }
        //            string rutaCarpetaDestino = Path.Combine(ruta, fechaCarpeta);
        //            if (!Directory.Exists(rutaCarpetaDestino))
        //            {
        //                Directory.CreateDirectory(rutaCarpetaDestino);
        //            }
        //            string rutaArchivoDestino = Path.Combine(rutaCarpetaDestino, Path.GetFileName(foto));
        //            if (!File.Exists(rutaArchivoDestino))
        //            {
        //                File.Move(foto, rutaArchivoDestino);
        //            }
        //        }
        //    }
        //}

        #region Public Methods
        // Método principal para agrupar imágenes por año
        public static void AgruparPorAño(string ruta)
        {
            Style.MostrarTitulo("Agrupación por año", ConsoleColor.Cyan);

            if (!Manage.EsRutaValida(ruta))
            {
                Style.MostrarComentarios("La ruta ingresada no es válida. Verifica e intenta nuevamente.", ConsoleColor.Red);
                return;
            }

            var imagenes = Manage.ObtenerImagenesValidas(ruta)
                                 .Where(EsImagenValida)
                                 .ToList();

            var gruposPorAño = AgruparPorFecha(imagenes, ruta);

            ProcesarGruposPorAño(gruposPorAño, ruta);

            Style.MostrarComentarios("Proceso de agrupación por año finalizado.", ConsoleColor.Green);
        }
        #endregion

        #region Private Methods
        // Procesa cada grupo de imágenes por año
        private static void ProcesarGruposPorAño(Dictionary<string, List<string>> gruposPorAño, string rutaBase)
        {
            foreach (var grupo in gruposPorAño)
            {
                MostrarResumenDeGrupo(grupo.Key, grupo.Value);

                if (Confirmar($"¿Deseas mover todas las imágenes del año {grupo.Key}?"))
                    MoverImagenes(grupo.Value, rutaBase, grupo.Key);

                if (Confirmar($"¿Deseas guardar los metadatos del grupo {grupo.Key}?"))
                    GuardarMetadatos(grupo.Value);
            }
        }

        // Agrupa las imágenes según su año de modificación
        private static Dictionary<string, List<string>> AgruparPorFecha(List<string> imagenes, string rutaBase)
        {
            var grupos = new Dictionary<string, List<string>>();

            foreach (var foto in imagenes)
            {
                string rutaCompleta = Path.Combine(rutaBase, Path.GetFileName(foto));
                string año = ObtenerAño(rutaCompleta);

                if (!grupos.TryGetValue(año, out List<string>? value))
                {
                    value = [];
                    grupos[año] = value;
                }

                value.Add(foto);
            }

            return grupos;
        }

        // Obtiene el año de la fecha de modificación del archivo
        private static string ObtenerAño(string rutaCompleta)
        {
            if (File.Exists(rutaCompleta))
            {
                DateTime fechaModificacion = File.GetLastWriteTime(rutaCompleta);
                return fechaModificacion.Year.ToString();
            }

            return "Sin Fecha";
        }

        // Verifica si la imagen es válida intentando cargarla
        private static bool EsImagenValida(string ruta)
        {
            try
            {
                using var stream = new FileStream(ruta, FileMode.Open, FileAccess.Read);
                using var img = Image.FromStream(stream, false, true);
                return true;
            }
            catch
            {
                Style.MostrarComentarios($"Imagen inválida: {Path.GetFileName(ruta)}", ConsoleColor.DarkYellow);
                return false;
            }
        }

        // Muestra un resumen del grupo de imágenes por año
        private static void MostrarResumenDeGrupo(string año, List<string> imagenes)
        {
            Style.MostrarTitulo($"Grupo del año {año} - {imagenes.Count} imagen(es)", ConsoleColor.Magenta);

            int columnas = Style.CalcularColumnas(imagenes);
            Style.MostrarEnColumnas(imagenes, ConsoleColor.Gray, columnas);
        }

        // Confirma una acción con el usuario
        private static bool Confirmar(string mensaje)
        {
            Style.MostrarComentarios($"\n{mensaje} (S/N): ", ConsoleColor.Yellow);
            string? respuesta = Console.ReadLine()?.Trim().ToUpper();
            return respuesta == "S";
        }

        // Mueve el grupo de imágenes a la carpeta correspondiente al año
        private static void MoverImagenes(List<string> imagenes, string rutaBase, string año)
        {
            string carpetaDestino = Path.Combine(rutaBase, año);

            if (!Directory.Exists(carpetaDestino))
                Directory.CreateDirectory(carpetaDestino);

            foreach (var foto in imagenes)
            {
                string nombre = Path.GetFileName(foto);
                string destino = Path.Combine(carpetaDestino, nombre);

                if (!File.Exists(destino))
                {
                    File.Move(foto, destino);
                    Style.MostrarComentarios($"Movida: {nombre}", ConsoleColor.Green);
                }
                else
                    Style.MostrarComentarios($"Ya existe: {nombre}", ConsoleColor.DarkYellow);
            }

            Style.MostrarComentarios($"Grupo del año {año} movido.", ConsoleColor.Cyan);
        }

        // Guarda los metadatos de cada imagen en el grupo
        private static void GuardarMetadatos(List<string> imagenes)
        {
            foreach (var foto in imagenes)
            {
                var metadata = MetadataReader.GetMetadata(foto);
                if (metadata != null)
                {
                    MetadataSaver.GuardarEnArchivo(metadata);
                    Style.MostrarComentarios($"Metadatos guardados: {Path.GetFileName(foto)}", ConsoleColor.Green);
                }
                else
                    Style.MostrarComentarios($"No se pudo extraer metadatos: {Path.GetFileName(foto)}", ConsoleColor.DarkYellow);
            }
        }
        #endregion
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