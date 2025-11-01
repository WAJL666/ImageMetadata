using ImageMetadataTools.css;

namespace ImageMetadataTools.UI
{
    public class MenuPrincipal
    {
        private static string rutaDestino = string.Empty;
        public static void Inicio()
        {
            int option;
            do
            {
                Style.MostrarMenu();

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Style.MostrarError("Entrada inválida. Debe ser un número.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        rutaDestino = PedirRuta();
                        OperMenu.ProcesarImagen(rutaDestino);
                        break;
                    case 2:
                        OperMenu.GuardarInformacion();
                        break;
                    case 3:
                        rutaDestino = PedirRuta();
                        OperMenu.IngresarCarpeta(rutaDestino);
                        break;
                    case 4:
                        int subopcion = Style.MostrarMenuAgrupar();
                        break;
                    case 5:
                        Style.MostrarComentarios("\nGracias por usar el lector de metadatos. ¡Hasta pronto!", ConsoleColor.Green);
                        Environment.Exit(0);
                        break;
                    default:
                        Style.MostrarError("Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }

            } while (option != 5);
        }

        private static string PedirRuta()
        {
            Style.MostrarComentarios("\nIngrese la ruta completa del archivo o carpeta.", ConsoleColor.Yellow);
            return Console.ReadLine().Trim('"');
        }
    }
}
