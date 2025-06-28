using System;
using System.Collections.Generic;

namespace ListasYTuplas
{
    class Curso
    {
        private List<string> asignaturas;

        public Curso()
        {
            asignaturas = new List<string>();
            asignaturas.Add("Matemáticas");
            asignaturas.Add("Física");
            asignaturas.Add("Química");
            asignaturas.Add("Historia");
            asignaturas.Add("Lengua");
        }

        public void MostrarAsignaturas()
        {
            Console.WriteLine("Asignaturas del curso:");
            foreach (string asignatura in asignaturas)
            {
                Console.WriteLine("- " + asignatura);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Curso curso = new Curso();
            curso.MostrarAsignaturas();

            Console.WriteLine("\nPresiona ENTER para salir...");
            Console.ReadLine(); // Mantiene la consola abierta
        }
    }
}

