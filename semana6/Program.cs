using System;
using System.Collections.Generic; 
using System.Linq; 

namespace ListaEnlazadaEjercicios
{
    // Clase principal de la aplicación. Contiene el método Main que inicia el programa.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ESTRUCTURA DE DATOS - UEA    EJERCICIOS/ SEMANA 06"); // Mensaje inicial del ejemplo

            while (true) // Bucle infinito para el menú hasta que el usuario decida salir
            {
                Console.WriteLine("\nSeleccione un ejercicio (1, 5), o 0 para salir:"); // Menú ajustado para ejercicio 1 y 5
                string opcion = Console.ReadLine(); 

                // Usa una estructura switch para ejecutar la acción correspondiente a la opción.
                switch (opcion)
                {
                    case "1":
                        // Llama al método estático Ejecutar de la clase Ejercicio01.
                        Ejercicio01.Ejecutar(); 
                        break;
                    case "5": 
                        // Llama al método estático Ejecutar de la clase Ejercicio05.
                        Ejercicio05.Ejecutar(); 
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
                        return; // Termina la ejecución del método Main y, por lo tanto, del programa.
                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingrese una opción del 0, 1 o 5.");
                        break;
                }
            }
        }
    }
}