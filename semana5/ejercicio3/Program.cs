using System;
using System.Collections.Generic;

namespace Listas
{
    // Clase que representa una colección de números
    class Numeros
    {
        private List<int> lista;

        // Constructor: llena la lista con los números del 1 al 10
        public Numeros()
        {
            lista = new List<int>();
            Console.WriteLine("Los números del 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ");
            for (int i = 1; i <= 10; i++)
            {
                lista.Add(i);
            }
        }

        // Método que muestra los números en orden inverso separados por comas
        public void MostrarInverso()
        {
            lista.Reverse(); // <- Esta línea es esencial
            Console.WriteLine("Los números en orden inverso:");
            for (int i = 0; i < lista.Count; i++)
            {
                if (i == lista.Count - 1)
                    Console.Write(lista[i]); // Último número sin coma
                else
                    Console.Write(lista[i] + ", ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EJERCICIO Muestra los números del 1 al 10 en orden inverso\n");

            // Crear objeto de la clase Numeros
            Numeros numeros = new Numeros();

            // Llamar al método para mostrar los números invertidos
            numeros.MostrarInverso();

            // Esperar que el usuario presione una tecla antes de cerrar
            Console.WriteLine("\n\nPresiona ENTER para salir...");
            Console.ReadLine();
        }
    }
}
