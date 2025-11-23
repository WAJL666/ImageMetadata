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
                        Style.LimpiarPantalla();
                        rutaDestino = PedirRuta();
                        OperMenu.ProcesarImagen(rutaDestino);
                        break;
                    case 2:
                        Style.LimpiarPantalla();
                        rutaDestino = PedirRuta();
                        IniciarNavegacion(rutaDestino);
                        break;
                    case 3:
                        Style.LimpiarPantalla();
                        Style.MostrarComentarios("\nGracias por usar el lector de metadatos. ¡Hasta pronto!", ConsoleColor.Green);
                        Environment.Exit(0);
                        break;
                    default:
                        Style.LimpiarPantalla();
                        break;
                }
            } while (option != 3);
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
                    Style.LimpiarPantalla();
                    return Manage.NavegarASubcarpeta(ref rutaActual);
                case 2:
                    Style.LimpiarPantalla();
                    return Manage.NavegarAtras(ref rutaActual);
                case 3:
                    Style.LimpiarPantalla();
                    return Manage.NavegarAdelante(ref rutaActual);
                case 4:
                    Style.LimpiarPantalla();
                    return OperMenu.ProcesaImage(ref rutaActual);
                case 5:
                    Style.LimpiarPantalla();
                    int subopcion = Style.MostrarMenuAgrupar();
                    EjecutarAgrupacion(subopcion, rutaActual);
                    return true;
                case 6:
                    return false;
                default:
                    Style.LimpiarPantalla();
                    Style.MostrarComentarios("Opción inválida.", ConsoleColor.Red);
                    return true;
            }
        }
        #endregion

        #region Private Methods
        // Inicia el proceso de navegación de carpetas.
        private static void IniciarNavegacion(string ruta)
        {
            if (!Manage.EsRutaValida(ruta))
            {
                Style.MostrarComentarios("La carpeta no existe. Intente de nuevo.", ConsoleColor.Red);
                return;
            }

            Manage.Navegar(ruta);
        }

        // Ejecuta la acción de agrupación según la subopción seleccionada.
        private static void EjecutarAgrupacion(int subopcion, string rutaActual)
        {
            switch (subopcion)
            {
                case 1:
                    Style.LimpiarPantalla();
                    Agrupar.AgruparPorAño(rutaActual);
                    break;
                case 2:
                    Style.MostrarComentarios("\nAgrupando imágenes por lugar", ConsoleColor.Green);
                    break;
                case 3:
                    IniciarNavegacion(rutaDestino);
                    break;
                default:
                    Style.MostrarComentarios("Opción no válida en el submenú.", ConsoleColor.Red);
                    break;
            }
        }
        #endregion
    }
}
