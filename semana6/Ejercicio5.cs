using System;
using System.Linq;

namespace ListaEnlazadaEjercicios
{
    // Clase estática que implementa la lógica para el Ejercicio 05.
    // El objetivo es generar números aleatorios y clasificarlos en listas de primos y Armstrong.
    public static class Ejercicio05
    {
        // Método principal para ejecutar el Ejercicio 05.
        public static void Ejecutar()
        {
            Console.WriteLine("\n--- Ejecutando Ejercicio 05: Manejo de listas con primos y Armstrong ---");
            
            // Crea dos nuevas instancias de ListaEnlazada para almacenar números primos y Armstrong.
            ListaEnlazada primos = new ListaEnlazada();
            ListaEnlazada armstrong = new ListaEnlazada();
            Random rnd = new Random(); // Objeto para generar números aleatorios.

            // Genera 20 números aleatorios (entre 1 y 999) y los clasifica.
            for (int i = 0; i < 20; i++)
            {
                int num = rnd.Next(1, 1000); // Genera un número aleatorio.

                // Si el número es primo, lo agrega al final de la lista de primos.
                if (EsPrimo(num))
                {
                    primos.AgregarFinal(num);
                }

                // Si el número es un número Armstrong, lo agrega al inicio de la lista de Armstrong.
                if (EsArmstrong(num))
                {
                    armstrong.AgregarInicio(num);
                }
            }

            // Obtiene la cantidad total de elementos en cada lista.
            int totalPrimos = primos.Contar();
            int totalArmstrong = armstrong.Contar();

            // Muestra la lista de números primos y su total.
            Console.WriteLine("\nLista de números primos:");
            primos.Mostrar();
            Console.WriteLine($"Total de primos encontrados: {totalPrimos}");

            // Muestra la lista de números Armstrong y su total.
            Console.WriteLine("\nLista de números Armstrong:");
            armstrong.Mostrar();
            Console.WriteLine($"Total de Armstrong encontrados: {totalArmstrong}");

            // Compara la cantidad de elementos en ambas listas y muestra un mensaje.
            if (totalPrimos > totalArmstrong)
            {
                Console.WriteLine("\nLa lista de números primos tiene más elementos.");
            }
            else if (totalArmstrong > totalPrimos)
            {
                Console.WriteLine("\nLa lista de números Armstrong tiene más elementos.");
            }
            else
            {
                Console.WriteLine("\nAmbas listas tienen la misma cantidad de elementos.");
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey(); // Pausa la ejecución hasta que el usuario presione una tecla.
        }

        // Método auxiliar para determinar si un número es primo.
        private static bool EsPrimo(int n)
        {
            if (n < 2) return false; // Los números menores que 2 no son primos.
            // Itera desde 2 hasta la raíz cuadrada de n. Si encuentra un divisor, no es primo.
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false; // Si n es divisible por i, no es primo.
            }
            return true; // Si no se encontraron divisores, es primo.
        }

        // Método auxiliar para determinar si un número es un número Armstrong.
        // Un número Armstrong es un número de n dígitos que es igual a la suma de sus dígitos elevados a la n-ésima potencia.
        private static bool EsArmstrong(int n)
        {
            if (n < 0) return false; // Los números negativos no son Armstrong.
            int suma = 0;
            int temp = n;
            // Calcula el número de dígitos del número.
            int digitos = n.ToString().Length;

            // Itera a través de cada dígito del número.
            while (temp > 0)
            {
                int dig = temp % 10; // Obtiene el último dígito del número.
                suma += (int)Math.Pow(dig, digitos); // Suma el dígito elevado a la potencia de 'digitos'.
                temp /= 10; // Elimina el último dígito para procesar el siguiente.
            }
            return suma == n; // Retorna true si la suma de las potencias es igual al número original.
        }
    }
}