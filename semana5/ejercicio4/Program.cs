using System;
using System.Collections.Generic;

namespace ListasYNotas
{
    class Curso
    {
        private List<string> asignaturas;

        public Curso()
        {
            // Lista de asignaturas del curso
            asignaturas = new List<string> { "Matemáticas", "Física", "Química", "Historia", "Lengua" };
        }

        // Método que pide notas y elimina asignaturas aprobadas
        public void EvaluarAsignaturas()
        {
            List<string> reprobadas = new List<string>(); // Para guardar asignaturas no aprobadas

            foreach (string asignatura in asignaturas)
            {
                Console.Write($"¿Qué nota sacaste en {asignatura}? ");
                string entrada = Console.ReadLine();
                int nota;

                // Validación básica de entrada
                if (int.TryParse(entrada, out nota))
                {
                    if (nota < 7)
                    {
                        reprobadas.Add(asignatura); // Si no aprueba, la guarda
                    }
                }
                else
                {
                    Console.WriteLine("Nota inválida. Se considerará reprobada.");
                    reprobadas.Add(asignatura); // Si la entrada es inválida, también la guarda
                }
            }

            // Mostrar resultados
            Console.WriteLine("\nAsignaturas que debes repetir:");
            foreach (string materia in reprobadas)
            {
                Console.WriteLine("- " + materia);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EVALUACIÓN DE ASIGNATURAS ===\n");

            Curso curso = new Curso();
            curso.EvaluarAsignaturas();

            Console.WriteLine("\nPresiona ENTER para salir...");
            Console.ReadLine();
        }
    }
}
