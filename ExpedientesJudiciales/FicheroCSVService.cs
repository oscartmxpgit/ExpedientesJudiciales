using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracasaInstrumental;

namespace ExpedientesJudiciales
{
    internal class FicheroCSVService : IFicheroCSVService
    {
        public string[] ObtenerContenidoCSV(string rutaFicheroCSV)
        {
            return File.ReadLines(@rutaFicheroCSV).ToArray();
        }
    }
}
