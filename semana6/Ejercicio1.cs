using System;

namespace ListaEnlazadaEjercicios
{
    // Clase estática que implementa la lógica para el Ejercicio 01.
    // El objetivo es demostrar cómo contar elementos en una lista enlazada.
    public static class Ejercicio01
    {
        // Método principal para ejecutar el Ejercicio 01.
        public static void Ejecutar()
        {
            Console.WriteLine("\n--- Ejecutando Ejercicio 01: Contar elementos de una lista ---");
            
            // Crea una nueva instancia de ListaEnlazada.
            ListaEnlazada lista = new ListaEnlazada();
            
            // Agrega algunos elementos de ejemplo a la lista.
            lista.AgregarFinal(10);
            lista.AgregarFinal(20);
            lista.AgregarFinal(30);
            lista.AgregarFinal(40);
            lista.AgregarInicio(5); // Demuestra la adición al inicio.

            Console.WriteLine("Elementos en la lista:");
            lista.Mostrar(); // Muestra los elementos actuales de la lista.
            
            // Utiliza el método Contar() de la lista enlazada y muestra el resultado.
            Console.WriteLine($"Cantidad total de elementos en la lista: {lista.Contar()}");
            
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey(); // Pausa la ejecución hasta que el usuario presione una tecla.
        }
    }
}