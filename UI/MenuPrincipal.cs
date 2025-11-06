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
                Style.MostrarMenu();
                option = Style.LeerOpcion();

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
                        IniciarNavegacion();
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
                    return Manage.NavegarASubcarpeta(ref rutaActual);
                case 2:
                    return Manage.NavegarAtras(ref rutaActual);
                case 3:
                    return Manage.NavegarAdelante(ref rutaActual);
                case 4:
                    return OperMenu.ProcesaImage(ref rutaActual);
                case 5:
                    return false;
                default:
                    Style.MostrarError("Opción inválida.");
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
                Style.MostrarError("La carpeta no existe. Intente de nuevo.");
                return;
            }

            Manage.Navegar(rutaActual);
        }
        #endregion
    }
}
