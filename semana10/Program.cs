using System;
using System.Collections.Generic;
using System.Linq;

namespace CampanaVacunacion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Campaña de Vacunación COVID-19 ===");
            
            const int totalCiudadanos = 500;
            const int numPfizer = 75;
            const int numAstraZeneca = 75;

            HashSet<string> todosCiudadanos = GenerarCiudadanos(totalCiudadanos);
            HashSet<string> vacunadosPfizer = GenerarVacunados(numPfizer);
            HashSet<string> vacunadosAstraZeneca = GenerarVacunados(numAstraZeneca);

            HashSet<string> vacunados = new HashSet<string>(vacunadosPfizer.Union(vacunadosAstraZeneca));
            HashSet<string> noVacunados = new HashSet<string>(todosCiudadanos.Except(vacunados));
            HashSet<string> ambasDosis = new HashSet<string>(vacunadosPfizer.Intersect(vacunadosAstraZeneca));
            HashSet<string> soloPfizer = new HashSet<string>(vacunadosPfizer.Except(vacunadosAstraZeneca));
            HashSet<string> soloAstraZeneca = new HashSet<string>(vacunadosAstraZeneca.Except(vacunadosPfizer));

            Console.WriteLine($"\nTotal ciudadanos: {totalCiudadanos}");
            Console.WriteLine($"Vacunados con Pfizer: {vacunadosPfizer.Count}");
            Console.WriteLine($"Vacunados con AstraZeneca: {vacunadosAstraZeneca.Count}");
            Console.WriteLine($"Vacunados: {vacunados.Count}");
            Console.WriteLine($"No vacunados: {noVacunados.Count}");
            Console.WriteLine($"Ambas dosis: {ambasDosis.Count}");
            Console.WriteLine($"Solo Pfizer: {soloPfizer.Count}");
            Console.WriteLine($"Solo AstraZeneca: {soloAstraZeneca.Count}");

            // Mostrar los ciudadanos de cada grupo
            ImprimirGrupo("\n--- Ciudadanos vacunados con Pfizer ---", vacunadosPfizer);
            ImprimirGrupo("\n--- Ciudadanos vacunados con AstraZeneca ---", vacunadosAstraZeneca);
            ImprimirGrupo("\n--- Ciudadanos con ambas dosis ---", ambasDosis);
            ImprimirGrupo("\n--- Ciudadanos solo con Pfizer ---", soloPfizer);
            ImprimirGrupo("\n--- Ciudadanos solo con AstraZeneca ---", soloAstraZeneca);
            ImprimirGrupo("\n--- Ciudadanos no vacunados ---", noVacunados);
        }

        static HashSet<string> GenerarCiudadanos(int cantidad)
        {
            HashSet<string> conjunto = new HashSet<string>();
            for (int i = 1; i <= cantidad; i++)
                conjunto.Add("Ciudadano " + i);
            return conjunto;
        }

        static HashSet<string> GenerarVacunados(int nv)
        {
            HashSet<string> conjunto = new HashSet<string>();
            Random random = new Random();
            while (conjunto.Count < nv)
                conjunto.Add("Ciudadano " + random.Next(1, 501));
            return conjunto;
        }

        static void ImprimirGrupo(string titulo, HashSet<string> grupo)
        {
            Console.WriteLine(titulo);
            foreach (var ciudadano in grupo)
                Console.Write(ciudadano + " ");
            Console.WriteLine("\n");
        }
    }
}
