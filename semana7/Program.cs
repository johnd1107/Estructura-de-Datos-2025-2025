using System;

namespace PilaEjercicios
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("========= MENÚ PRINCIPAL =========");
                Console.WriteLine("1. Verificación de paréntesis balanceados");
                Console.WriteLine("2. Resolver Torres de Hanoi usando pilas");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) opcion = 0;

                switch (opcion)
                {
                    case 1:
                        Ejercicio1.VerificarParentesis();
                        break;
                    case 2:
                        Ejercicio2.TorresDeHanoi();
                        break;
                    case 3:
                        Console.WriteLine("Programa finalizado.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (opcion != 3)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 3);
        }
    }
}
