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
            string[] lines = _ficheroCSVService.ObtenerContenidoCSV(rutaFicheroCSV);

            int totalDays = 0;

            foreach (string line in lines)
            {
                string? fechaInoacion = string.Empty;
                
                //comprobar que haya 3 columnas, separadas por comas
                if (line.Count(l => l == ',')==2)
                {
                    fechaInoacion = line.Split(',').Skip(1).FirstOrDefault();
                    string? codigoTramitacion = line.Split(',').Skip(2).FirstOrDefault();

                    if (codigoTramitacion == "DIP" || codigoTramitacion == "SUM")
                    {
                        if (!string.IsNullOrEmpty(fechaInoacion))
                         totalDays += DaysFromToday(fechaInoacion);
                    }
                }
            }
            return totalDays;
        }

        static int DaysFromToday(string fecha)
        {
            DateTime dateResult;

            //comprobar el formato de la fecha
            if (!DateTime.TryParseExact(fecha, "yyyyMMdd", null, DateTimeStyles.None, out dateResult))
            {
                return 0;
            }

            DateTime today = DateTime.Now;
            
            int days = 0;
            days = (today - dateResult).Days;
            if (days > 360)
                return days;
            else
                return 0;
        }
    }

    public interface IFicheroCSVService
    {
        public string[] ObtenerContenidoCSV(string rutaFicheroCSV);
    }

}