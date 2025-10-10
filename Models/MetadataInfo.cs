namespace ImageMetadataTools.Models
{
    public class MetadataInfo
    {
        // ---- Información básica de la imagen ----
        public required string FileName { get; set; }           // Nombre del archivo
        public required string FileSize { get; set; }           //peso
        public int Width { get; set; }                          // Ancho en píxeles
        public int Height { get; set; }                         // Alto en píxeles
        public required string Format { get; set; }             // Formato (JPEG, PNG, etc.)
        public int Orientation { get; set; }                    // Orientación

        // ---- Información de la cámara ----
        public required string CameraMake { get; set; }         // Fabricante de la cámara
        public required string CameraModel { get; set; }        // Modelo de la cámara
        public required string Software { get; set; }           // Software que generó la foto

        // ---- Información de la fotografía ----
        public required string DateTaken { get; set; }          // Fecha de captura
        public required string DateDigitized { get; set; }      // Fecha digitalización
        public required string ExposureTime { get; set; }       // Tiempo de exposición
        public required string Aperture { get; set; }           // Apertura (f/2.8, f/5.6, etc.)
        public int ISO { get; set; }                            // ISO
        public required string FocalLength { get; set; }        // Distancia focal
        public required string ExposureProgram { get; set; }    // Programa de exposición
        public required string MeteringMode { get; set; }       // Medición de luz
        public required string Flash { get; set; }              // Flash usado (sí/no)

        // ---- Información GPS ----
        public required string GPSLatitude { get; set; }
        public required string GPSLongitude { get; set; }
        public required string GPSAltitude { get; set; }

        // ---- Información del lente ----
        public required string LensMake { get; set; }
        public required string LensModel { get; set; }
        public required string WhiteBalance { get; set; }
        public required string LightSource { get; set; }
        public required string DigitalZoomRatio { get; set; }
    }
}
