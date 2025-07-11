using System;
using System.Collections.Generic;

namespace PilaEjercicios
{
    public class Ejercicio2
    {
        public static void TorresDeHanoi()
        {
            Console.Clear();
            Console.Write("Ingrese el número de discos: ");
            int n;
            if (!int.TryParse(Console.ReadLine(), out n) || n < 1)
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            Stack<int> origen = new Stack<int>();
            Stack<int> destino = new Stack<int>();
            Stack<int> auxiliar = new Stack<int>();

            for (int i = n; i >= 1; i--)
                origen.Push(i);

            Console.WriteLine("\nMovimientos para resolver Torres de Hanoi:\n");
            MoverDiscos(n, origen, destino, auxiliar, "Origen", "Destino", "Auxiliar");

            Console.WriteLine("\n Torres de Hanoi completado.");
        }

        private static void MoverDiscos(int n, Stack<int> origen, Stack<int> destino, Stack<int> auxiliar,
                                        string nombreOrigen, string nombreDestino, string nombreAuxiliar)
        {
            if (n == 1)
            {
                int disco = origen.Pop();
                destino.Push(disco);
                Console.WriteLine($"Mover disco {disco} de {nombreOrigen} a {nombreDestino}");
            }
            else
            {
                MoverDiscos(n - 1, origen, auxiliar, destino, nombreOrigen, nombreAuxiliar, nombreDestino);
                MoverDiscos(1, origen, destino, auxiliar, nombreOrigen, nombreDestino, nombreAuxiliar);
                MoverDiscos(n - 1, auxiliar, destino, origen, nombreAuxiliar, nombreDestino, nombreOrigen);
            }
        }
    }
}
