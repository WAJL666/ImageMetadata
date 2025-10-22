using ImageMetadataTools.css;
namespace ImageMetadataTools.Services
{
    internal class Manage
    {
        public static void ImageManage(string rutaCarpeta)
        {
            string[] archivos = Directory.GetFiles(rutaCarpeta, "*.*", SearchOption.TopDirectoryOnly); // 2, 3 
            string[] carpetas = Directory.GetDirectories(rutaCarpeta, "*", SearchOption.TopDirectoryOnly);  // 4            
            string[] extensionesValidas = [".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".gif"]; // 5
            List<string> imagenesValidas = [];

            foreach (string carpeta in carpetas)
            {
                ImageManage(carpeta); // 0
            }
            if (!Directory.Exists(rutaCarpeta))// 1
            {
                Style.MostrarError("La carpeta no existe. Intente de nuevo.");
                return;
            }                  
            
            foreach (string archivo in archivos)
            {
                string extension = Path.GetExtension(archivo).ToLower();
                foreach (string extValida in extensionesValidas)
                {
                    if (extension == extValida)
                    {
                        imagenesValidas.Add(archivo);
                        break; 
                    }
                }
            }            
            Style.MostrarTitulo(rutaCarpeta, ConsoleColor.Green);// 6     
            Console.WriteLine($"Imágenes econtradas: {imagenesValidas.Count}");
            Style.MostrarLiena();
            if (imagenesValidas.Count == 0)
            {
                Style.MostrarError("No se encontraron imágenes en la carpeta.");
                Style.MostrarLiena();
                return;
            }
            for (int i = 0; i < imagenesValidas.Count; i++)
            {                
                Console.Write($"{Path.GetFileName(imagenesValidas[i]),-25}");// 7 
                if ((i + 1) % 5 == 0) // 8 
                {
                    Console.WriteLine();
                }
            }
            
        }
    }
}
/* 0. Llamada recursiva para procesar subcarpetas
   1. Verificar si la carpeta existe (Directory.Exists).Si no existe → mostrar un mensaje de error y salir.
   2.  "*"=cualquier nombre, ".*"=cualquier extensión, "."= en conjunto.    
             SearchOption.TopDirectoryOnly=solo en la carpeta indicada, no en subcarpetas.
             SearchOption.AllDirectories=incluye subcarpetas.
    3. Obtener todos los archivos de la carpeta (Directory.GetFiles).
    4. Obtener todas las carpetas (Directory.GetDirectories).
    5. Usa un filtro por extensiones válidas (por ejemplo .jpg, .png, .bmp, .tiff). 
    6. Mostrar en consola los nombres de los archivos. puedes usar Path.GetFileName(rutaArchivo) para mostrar solo el nombre, no toda la ruta).
    7. {nombre,-25} deja cada nombre ocupando 25 caracteres de ancho, alineado a la izquierda.muestra solo el nombre del archivo.
    8. Cada 5 nombres, hacer un salto de línea.*/