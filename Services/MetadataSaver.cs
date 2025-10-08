using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ImageMetadataTools.Models;

//Guardar información en archivos 
namespace ImageMetadataTools.Services
{
    internal class MetadataSaver
    {
        public static void GuardarEnArchivo(MetadataInfo metadata)
        {
            // Carpeta donde se guardará (la misma de la imagen)
            string carpeta = Path.GetDirectoryName(metadata.FileName);
            string nombreArchivo = Path.GetFileNameWithoutExtension(metadata.FileName);

            Directory.CreateDirectory(carpeta);

            // Guardar como TXT
            
                string rutaTxt = Path.Combine(carpeta, nombreArchivo);
                using (StreamWriter escribir = new(rutaTxt, true)) 
                {
                    escribir.WriteLine("================================");
                    escribir.WriteLine("Archivo: " + metadata.FileName);
                    escribir.WriteLine("Peso: " + metadata.FileSize);
                    escribir.WriteLine("Dimensiones: " + metadata.Width + " x " + metadata.Height);
                    escribir.WriteLine("Formato: " + metadata.Format);
                    escribir.WriteLine("Orientación: " + metadata.Orientation);
                    escribir.WriteLine("Software:: " + metadata.Software);
                    escribir.WriteLine("Fecha captura: " + metadata.DateTaken);
                    escribir.WriteLine("Fecha digitalización: " + metadata.DateDigitized);
                    escribir.WriteLine("Exposición: " + metadata.ExposureTime);
                    escribir.WriteLine("Apertura: " + metadata.Aperture);
                    escribir.WriteLine("ISO:  " + metadata.ISO);
                    escribir.WriteLine("Distancia focal: " + metadata.FocalLength);
                    escribir.WriteLine("Programa de exposición: " + metadata.ExposureProgram);
                    escribir.WriteLine("Medición de luz: " + metadata.MeteringMode);
                    escribir.WriteLine("Flash: " + metadata.Flash);
                    escribir.WriteLine("Lente: " + metadata.LensMake + metadata.LensModel);
                    escribir.WriteLine("Balance blancos: " + metadata.WhiteBalance);
                    escribir.WriteLine("Fuente de luz:" + metadata.LightSource);
                    escribir.WriteLine("Zoom digital: " + metadata.DigitalZoomRatio);
                    escribir.WriteLine("GPS: Latitud: "+metadata.GPSLatitude + ", Longitud: "+ metadata.GPSLongitude+", Altitud: " + metadata.GPSAltitude);                      
                    escribir.WriteLine("================================\n");
                }
                Console.WriteLine($"✅ Metadatos guardados en: {rutaTxt}");
            }
        }
    }

