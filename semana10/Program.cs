using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlVacunas
{
    class App
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Control de Vacunación ===\n");

            int totalPersonas = 500;
            int aplicadasPfizer = 75;
            int aplicadasAstra = 75;

            // Crear la base de ciudadanos
            var registroGeneral = GenerarPersonas(totalPersonas);

            // Asignar vacunados aleatoriamente
            var listaPfizer = AsignarVacunados(aplicadasPfizer, totalPersonas);
            var listaAstra = AsignarVacunados(aplicadasAstra, totalPersonas);

            // Transformar en conjuntos para operaciones
            var setPfizer = new HashSet<string>(listaPfizer);
            var setAstra = new HashSet<string>(listaAstra);

            // Conjuntos derivados
            var todosVacunados = setPfizer.Union(setAstra).ToHashSet();
            var sinVacunar = registroGeneral.Except(todosVacunados).ToHashSet();
            var dobleVacuna = setPfizer.Intersect(setAstra).ToHashSet();
            var soloPfizer = setPfizer.Except(setAstra).ToHashSet();
            var soloAstra = setAstra.Except(setPfizer).ToHashSet();

            // Resumen
            Console.WriteLine(">>> RESUMEN GENERAL <<<");
            Console.WriteLine($"Cantidad total de personas registradas: {totalPersonas}");
            Console.WriteLine($"Vacunados con Pfizer: {setPfizer.Count}");
            Console.WriteLine($"Vacunados con AstraZeneca: {setAstra.Count}");
            Console.WriteLine($"Vacunados (al menos una dosis): {todosVacunados.Count}");
            Console.WriteLine($"Sin vacunar: {sinVacunar.Count}");
            Console.WriteLine($"Con doble dosis: {dobleVacuna.Count}");
            Console.WriteLine($"Únicamente Pfizer: {soloPfizer.Count}");
            Console.WriteLine($"Únicamente AstraZeneca: {soloAstra.Count}\n");

            // Listados
            Imprimir("Personas vacunadas con Pfizer", setPfizer);
            Imprimir("Personas vacunadas con AstraZeneca", setAstra);
            Imprimir("Personas con ambas vacunas", dobleVacuna);
            Imprimir("Personas solo con Pfizer", soloPfizer);
            Imprimir("Personas solo con AstraZeneca", soloAstra);
            Imprimir("Personas no vacunadas", sinVacunar);

            Console.WriteLine("\n=== Proceso finalizado ===");
        }

        // Genera la población
        static HashSet<string> GenerarPersonas(int cantidad)
        {
            var conjunto = new HashSet<string>();
            for (int i = 1; i <= cantidad; i++)
                conjunto.Add($"Ciudadano_{i}");
            return conjunto;
        }

        // Asignar vacunados de manera aleatoria
        static List<string> AsignarVacunados(int cantidad, int limite)
        {
            var seleccion = new HashSet<int>();
            Random rnd = new Random();

            while (seleccion.Count < cantidad)
                seleccion.Add(rnd.Next(1, limite + 1));

            // Convertir a lista de nombres
            return seleccion.Select(num => $"Ciudadano_{num}").ToList();
        }

        // Método para imprimir resultados
        static void Imprimir(string titulo, HashSet<string> grupo)
        {
            Console.WriteLine($"\n--- {titulo} ({grupo.Count}) ---");
            Console.WriteLine(string.Join(" | ", grupo));
        }
    }
}
