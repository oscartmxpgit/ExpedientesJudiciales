﻿using ExpedientesJudiciales;
using System.Globalization;

namespace TracasaInstrumental
{
    public class Ejercicio
    {
        private IFicheroCSVService _ficheroCSVService;

        public Ejercicio(IFicheroCSVService ficheroCSVService)
        {
            _ficheroCSVService = ficheroCSVService;
        }

        public int ObtenerResultado(string rutaFicheroCSV)
        {
            string[] lineas = _ficheroCSVService.ObtenerContenidoCSV(rutaFicheroCSV);

            int totalDays = 0;

            foreach (string linea in lineas)
            {
                string? fechaIncoacion = linea.Split(',').Skip(1).FirstOrDefault();
                string? codigoTramitacion = linea.Split(',').Skip(2).FirstOrDefault();
                if (!string.IsNullOrEmpty(codigoTramitacion))
                { 
                    if ((new[] { "DIP", "SUM" }).Contains(codigoTramitacion))
                    {
                        if (!string.IsNullOrEmpty(fechaIncoacion))
                        {
                            try
                            {
                                int diasTranscurridos = DiasTranscurridos(fechaIncoacion);
                                if (diasTranscurridos > 360)
                                {
                                    totalDays += diasTranscurridos;
                                }
                            }
                            catch (Exception)
                            {
                                // Esta excepción deberíamos registrarla en un log
                            }                           
                        }
                    }
                }
            }

            return totalDays;
        }

        public static int DiasTranscurridos(string fechaStr)
        {
            DateTime fecha;
            //comprobar el formato de la fecha
            if (!DateTime.TryParseExact(fechaStr, "yyyyMMdd", null, DateTimeStyles.None, out fecha))
            {
                throw new Exception("Fecha no válida");
            }
            DateTime hoy = DateTime.Now;

            return (hoy - fecha).Days;        
        }
    }

    public interface IFicheroCSVService
    {
        public string[] ObtenerContenidoCSV(string rutaFicheroCSV);
    }

}