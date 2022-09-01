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
                string fechaInoacion = string.Empty;
                if (!string.IsNullOrWhiteSpace(line))
                {
                    fechaInoacion = line.Split(',').Skip(1).FirstOrDefault();
                    string codigoTramitacion = line.Split(',').Skip(2).FirstOrDefault();

                    if (codigoTramitacion == "DIP" || codigoTramitacion == "SUM")
                    {
                        totalDays += DaysFromToday(fechaInoacion);
                    }
                }
            }
            return totalDays;
        }

        static int DaysFromToday(string fecha)
        {
            DateTime dt = DateTime.ParseExact(fecha, "yyyyMMdd", CultureInfo.InvariantCulture);
            DateTime today = DateTime.Now;
            //DateTime today = new DateTime(2021, 12, 12);

            int days = 0;
            days = (today - dt).Days;
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