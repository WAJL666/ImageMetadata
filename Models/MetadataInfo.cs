using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMetadataTools.Models
{        public class MetadataInfo
        {
            // ---- Información básica de la imagen ----
            public string FileName { get; set; }           // Nombre del archivo
            public string FileSize{ get; set; }             //peso
            public int Width { get; set; }                 // Ancho en píxeles
            public int Height { get; set; }                // Alto en píxeles
            public string Format { get; set; }             // Formato (JPEG, PNG, etc.)
            public int Orientation { get; set; }           // Orientación

            // ---- Información de la cámara ----
            public string CameraMake { get; set; }         // Fabricante de la cámara
            public string CameraModel { get; set; }        // Modelo de la cámara
            public string Software { get; set; }           // Software que generó la foto

            // ---- Información de la fotografía ----
            public string DateTaken { get; set; }          // Fecha de captura
            public string DateDigitized { get; set; }      // Fecha digitalización
            public string ExposureTime { get; set; }       // Tiempo de exposición
            public string Aperture { get; set; }           // Apertura (f/2.8, f/5.6, etc.)
            public int ISO { get; set; }                   // ISO
            public string FocalLength { get; set; }        // Distancia focal
            public string ExposureProgram { get; set; }    // Programa de exposición
            public string MeteringMode { get; set; }       // Medición de luz
            public string Flash { get; set; }              // Flash usado (sí/no)

            // ---- Información GPS ----
            public string GPSLatitude { get; set; }
            public string GPSLongitude { get; set; }
            public string GPSAltitude { get; set; }

            // ---- Información del lente ----
            public string LensMake { get; set; }
            public string LensModel { get; set; }
            public string WhiteBalance { get; set; }
            public string LightSource { get; set; }
            public string DigitalZoomRatio { get; set; }
        }

}
