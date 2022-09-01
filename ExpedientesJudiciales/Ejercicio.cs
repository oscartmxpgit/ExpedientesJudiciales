using ExpedientesJudiciales;
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
                string? fechaInoacion = string.Empty;
                
                //comprobar que haya 3 columnas, separadas por comas
                if (linea.Count(l => l == ',')==2)
                {
                    fechaInoacion = linea.Split(',').Skip(1).FirstOrDefault();
                    string? codigoTramitacion = linea.Split(',').Skip(2).FirstOrDefault();

                    if (codigoTramitacion == "DIP" || codigoTramitacion == "SUM")
                    {
                        if (!string.IsNullOrEmpty(fechaInoacion))
                         totalDays += DiasTranscurridos(fechaInoacion);
                    }
                }
            }
            return totalDays;
        }

        static int DiasTranscurridos(string fechaStr)
        {
            DateTime fecha;

            //comprobar el formato de la fecha
            if (!DateTime.TryParseExact(fechaStr, "yyyyMMdd", null, DateTimeStyles.None, out fecha))
            {
                return 0;
            }

            DateTime hoy = DateTime.Now;
            
            int diasTranscurridos = 0;
            diasTranscurridos = (hoy - fecha).Days;
            if (diasTranscurridos > 360)
                return diasTranscurridos;
            else
                return 0;
        }
    }

    public interface IFicheroCSVService
    {
        public string[] ObtenerContenidoCSV(string rutaFicheroCSV);
    }

}