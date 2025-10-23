using ImageMetadataTools.css;
namespace ImageMetadataTools.Services
{
    internal class Manage
    {
        public static void ImageManage(string rutaCarpeta)
        {
            if (!ValidarCarpeta(rutaCarpeta)) return; 

            List<string> imagenes = ObtenerImagenesValidas(rutaCarpeta);
            MostrarResumen(rutaCarpeta, imagenes);
            MostrarNombres(imagenes);

            foreach (string subcarpeta in Directory.GetDirectories(rutaCarpeta))
            {
                ImageManage(subcarpeta); // 0
            }
        }

        private static bool ValidarCarpeta(string ruta)
        {
            if (!Directory.Exists(ruta)) // 1
            {
                Style.MostrarError("La carpeta no existe. Intente de nuevo.");
                return false;
            }
            return true;
        }

        private static List<string> ObtenerImagenesValidas(string ruta)
        {
            string[] extensionesValidas = [".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".gif"]; // 5
            List<string> imagenes = [];

            foreach (string archivo in Directory.GetFiles(ruta))
            {
                string extension = Path.GetExtension(archivo).ToLowerInvariant(); // 3, 4
                if (extensionesValidas.Contains(extension))
                {
                    imagenes.Add(archivo);
                }
            }

            return imagenes;
        }

        private static void MostrarResumen(string ruta, List<string> imagenes)
        {
            Style.MostrarTitulo(ruta, ConsoleColor.Green);  // 6
            Console.WriteLine($"Imágenes encontradas: {imagenes.Count}");
            Style.MostrarLiena();

            if (imagenes.Count == 0)
            {
                Style.MostrarError("No se encontraron imágenes en la carpeta.");
                Style.MostrarLiena();
            }
        }

        private static void MostrarNombres(List<string> imagenes)
        {
            for (int i = 0; i < imagenes.Count; i++)
            {
                Console.Write($"{Path.GetFileName(imagenes[i]),-25}"); //7
                if ((i + 1) % 5 == 0)  //8
                    Console.WriteLine();
            }
            Console.WriteLine(); // Salto final
        }
    }
}

/* 0. Llamada recursiva para procesar subcarpetas
   1. Verificar si la carpeta existe (Directory.Exists).Si no existe → mostrar un mensaje de error y salir.
    3. Obtener todos los archivos de la carpeta (Directory.GetFiles).
    4. Obtener todas las carpetas (Directory.GetDirectories).
    5. Usa un filtro por extensiones válidas (por ejemplo .jpg, .png, .bmp, .tiff). 
    6. Mostrar en consola los nombres de los archivos. puedes usar Path.GetFileName(rutaArchivo) para mostrar solo el nombre, no toda la ruta).
    7. {nombre,-25} deja cada nombre ocupando 25 caracteres de ancho, alineado a la izquierda.muestra solo el nombre del archivo.
    8. Cada 5 nombres, hacer un salto de línea.*/