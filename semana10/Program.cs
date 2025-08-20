using System;
using System.Collections.Generic;
using System.Linq;

namespace VacunacionCOVID
{
    class Program
    {
        static void Main(string[] args)
        {
            // Conjunto total de ciudadanos ficticios (500 ciudadanos)
            HashSet<string> ciudadanos = new HashSet<string>();
            for (int i = 1; i <= 500; i++)
            {
                ciudadanos.Add("Ciudadano " + i);
            }

            // Conjunto de ciudadanos vacunados con Pfizer (75 ciudadanos ficticios)
            HashSet<string> vacunadosPfizer = new HashSet<string>();
            for (int i = 1; i <= 75; i++)
            {
                vacunadosPfizer.Add("Ciudadano " + i);
            }

            // Conjunto de ciudadanos vacunados con AstraZeneca (75 ciudadanos ficticios)
            HashSet<string> vacunadosAstraZeneca = new HashSet<string>();
            for (int i = 50; i < 125; i++) // algunos coincidirán con Pfizer
            {
                vacunadosAstraZeneca.Add("Ciudadano " + i);
            }

            // 1. Ciudadanos que no se han vacunado
            HashSet<string> vacunados = new HashSet<string>(vacunadosPfizer.Union(vacunadosAstraZeneca));
            HashSet<string> noVacunados = new HashSet<string>(ciudadanos.Except(vacunados));

            // 2. Ciudadanos que han recibido ambas dosis (intersección de Pfizer y AstraZeneca)
            HashSet<string> ambasDosis = new HashSet<string>(vacunadosPfizer.Intersect(vacunadosAstraZeneca));

            // 3. Ciudadanos que solo han recibido Pfizer (Pfizer - AstraZeneca)
            HashSet<string> soloPfizer = new HashSet<string>(vacunadosPfizer.Except(vacunadosAstraZeneca));

            // 4. Ciudadanos que solo han recibido AstraZeneca (AstraZeneca - Pfizer)
            HashSet<string> soloAstraZeneca = new HashSet<string>(vacunadosAstraZeneca.Except(vacunadosPfizer));

            // Mostrar resultados en consola
            Console.WriteLine("==== LISTADOS DE VACUNACIÓN ====");
            Console.WriteLine("\n1. Ciudadanos no vacunados: " + noVacunados.Count);
            Console.WriteLine(string.Join(", ", noVacunados.Take(20)) + " ...");

            Console.WriteLine("\n2. Ciudadanos con ambas dosis: " + ambasDosis.Count);
            Console.WriteLine(string.Join(", ", ambasDosis));

            Console.WriteLine("\n3. Ciudadanos solo con Pfizer: " + soloPfizer.Count);
            Console.WriteLine(string.Join(", ", soloPfizer));

            Console.WriteLine("\n4. Ciudadanos solo con AstraZeneca: " + soloAstraZeneca.Count);
            Console.WriteLine(string.Join(", ", soloAstraZeneca));

            Console.WriteLine("\nProceso finalizado.");
        }
    }
}