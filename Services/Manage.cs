using ImageMetadataTools.css;
using System.IO;
namespace ImageMetadataTools.Services
{
    internal class Manage
    {
        public static void ImageManage(string rutaCarpeta)
        {
            
            //Verificar si la carpeta existe (Directory.Exists).Si no existe → mostrar un mensaje de error y salir.
            if (!Directory.Exists(rutaCarpeta))
            {
                Style.MostrarError("La carpeta no existe. Intente de nuevo.");
                return;
            }

            //Obtener todos los archivos de la carpeta (Directory.GetFiles). Usa un filtro por extensiones válidas (por ejemplo .jpg, .png, .bmp, .tiff).
            // "*"=cualquier nombre, ".*"=cualquier extensión, "."= en conjunto.    
            // SearchOption.TopDirectoryOnly=solo en la carpeta indicada, no en subcarpetas.
            // SearchOption.AllDirectories=incluye subcarpetas.
            string[] archivos = Directory.GetFiles(rutaCarpeta, "*.*", SearchOption.AllDirectories);

            //Contar cuántas imágenes hay. archivos.Length
            //Mostrar en consola los nombres de los archivos. puedes usar Path.GetFileName(rutaArchivo) para mostrar solo el nombre, no toda la ruta).
            Style.MostrarTitulo(rutaCarpeta, ConsoleColor.Green);
            Console.WriteLine($"Imágenes econtradas: {archivos.Length}");
            if (archivos.Length == 0)
            {
                Style.MostrarError("No se encontraron imágenes en la carpeta.");
                Style.MostrarLiena();
                return;
            }
            for (int i = 0; i < archivos.Length; i++)
            {
                //{nombre,-25} deja cada nombre ocupando 25 caracteres de ancho, alineado a la izquierda.muestra solo el nombre del archivo
                Console.Write($"{Path.GetFileName(archivos[i]),-25}");
                if ((i + 1) % 5 == 0) //Cada 5 nombres, hacer un salto de línea.
                {
                    Console.WriteLine();
                }   
            }

            //Mostrar al final el total de imágenes encontradas.

        }
    }
}
