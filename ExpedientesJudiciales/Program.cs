using ExpedientesJudiciales;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace TracasaInstrumental
{
    internal class Program
    {
        //poner clase aparte e inyectar dependencias
        static void Main(string[] args)
        {
            FicheroCSVService csvService = new FicheroCSVService();
            Ejercicio ejercicio = new Ejercicio(csvService);
            int resultado = ejercicio.ObtenerResultado(@"./Assets/data.csv");
            Console.WriteLine("La suma de los días de tramitación de todos los expedientes judiciales que cumplen los requisitos es: " + resultado);
        }
    }
}
