using ImageMetadataTools.css;
using ImageMetadataTools.Services;

namespace ImageMetadataTools.UI
{
    public class MenuPrincipal
    {
        #region Atributos
        // Almacena la ruta destino ingresada por el usuario.
        private static string rutaDestino = string.Empty;
        #endregion

        #region Public Methods
        // Inicia el menú principal y procesa las opciones seleccionadas por el usuario.
        public static void Inicio()
        {
            int option;
            do
            {
                option = Style.MostrarMenu();

                switch (option)
                {
                    case 1:
                        Console.Clear();
                        rutaDestino = PedirRuta();
                        OperMenu.ProcesarImagen(rutaDestino);
                        break;
                    case 2:
                        Console.Clear();
                        OperMenu.GuardarInformacion();
                        break;
                    case 3:
                        Console.Clear();
                        IniciarNavegacion();
                        break;
                    case 4:
                        Console.Clear();
                        int subopcion = Style.MostrarMenuAgrupar();
                        EjecutarAgrupacion(subopcion);
                        break;
                    case 5:
                        Console.Clear();
                        Style.MostrarComentarios("\nGracias por usar el lector de metadatos. ¡Hasta pronto!", ConsoleColor.Green);
                        Environment.Exit(0);
                        break;
                    default:
                        Style.MostrarComentarios("Opción inválida. Intente de nuevo.", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }

            } while (option != 5);
        }

        // Solicita al usuario que ingrese una ruta de archivo o carpeta.
        public static string PedirRuta()
        {
            Style.MostrarComentarios("\nIngrese la ruta completa o nombre del archivo o carpeta.", ConsoleColor.Yellow);
            return Console.ReadLine().Trim('"');
        }

        // Procesa la opción 3 seleccionada en el menú de navegación.
        public static bool ProcesarOpcion(int opcion, ref string rutaActual)
        {
            switch (opcion)
            {
                case 1:
                    Console.Clear();
                    return Manage.NavegarASubcarpeta(ref rutaActual);
                case 2:
                    Console.Clear();
                    return Manage.NavegarAtras(ref rutaActual);
                case 3:
                    Console.Clear();
                    return Manage.NavegarAdelante(ref rutaActual);
                case 4:
                    Console.Clear();
                    return OperMenu.ProcesaImage(ref rutaActual);
                case 5:
                    return false;
                default:
                    Style.MostrarComentarios("Opción inválida.", ConsoleColor.Red);
                    return true;
            }
        }
        #endregion

        #region Private Methods
        // Inicia el proceso de navegación de carpetas.
        private static void IniciarNavegacion()
        {
            string rutaActual = PedirRuta();

            if (!Manage.EsRutaValida(rutaActual))
            {
                Style.MostrarComentarios("La carpeta no existe. Intente de nuevo.", ConsoleColor.Red);
                return;
            }

            Manage.Navegar(rutaActual);
        }

        // Ejecuta la acción de agrupación según la subopción seleccionada.
        private static void EjecutarAgrupacion(int subopcion)
        {
            switch (subopcion)
            {
                case 1:
                    Style.MostrarComentarios("\nAgrupando imágenes por fecha", ConsoleColor.Green);
                    break;
                case 2:
                    Style.MostrarComentarios("\nAgrupando imágenes por lugar", ConsoleColor.Green);
                    break;
                default:
                    Style.MostrarComentarios("Opción no válida en el submenú.", ConsoleColor.Red);
                    break;
            }
        }
        #endregion
    }
}
