using System;

namespace Palindromo
{
    class Verificador
    {
        public bool EsPalindromo(string palabra)
        {
            palabra = palabra.ToLower(); // Convierte a minúsculas

            char[] letras = palabra.ToCharArray();
            Array.Reverse(letras); // Invierte el arreglo

            string invertida = new string(letras);
            return palabra == invertida; // Compara
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Verificador verificador = new Verificador();
            string opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== VERIFICADOR DE PALÍNDROMOS ===\n");

                Console.Write("Ingresa una palabra: ");
                string palabra = Console.ReadLine();

                if (verificador.EsPalindromo(palabra))
                {
                    Console.WriteLine($"\nLa palabra \"{palabra}\" es un palíndromo.");
                }
                else
                {
                    Console.WriteLine($"\nLa palabra \"{palabra}\" no es un palíndromo.");
                }

                Console.Write("\n¿Deseas ingresar otra palabra? (s/n): ");
                opcion = Console.ReadLine().ToLower();

            } while (opcion == "s");

            Console.WriteLine("\nPrograma finalizado. Presiona ENTER para salir...");
            Console.ReadLine();
        }
    }
}

