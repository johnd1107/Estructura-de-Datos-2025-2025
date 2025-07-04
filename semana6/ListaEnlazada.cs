using System;

namespace ListaEnlazadaEjercicios
{
    // Clase que implementa una lista enlazada simple.
    // Permite gestionar una colección de nodos.
    public class ListaEnlazada
    {
        public Nodo? cabeza; // El primer nodo de la lista. Si es null, la lista está vacía.

        // Constructor para inicializar una lista enlazada vacía.
        public ListaEnlazada()
        {
            cabeza = null;
        }

        // Agrega un nuevo nodo al inicio de la lista.
       
        public void AgregarInicio(int dato)
        {
            Nodo nuevo = new Nodo(dato); // Crea un nuevo nodo.
            nuevo.Siguiente = cabeza;    // El nuevo nodo apunta al nodo que era la cabeza.
            cabeza = nuevo;              // El nuevo nodo se convierte en la nueva cabeza de la lista.
        }

        // Agrega un nuevo nodo al final de la lista.
        public void AgregarFinal(int dato)
        {
            Nodo nuevo = new Nodo(dato); // Crea un nuevo nodo.
            if (cabeza == null)
            {
                cabeza = nuevo; // Si la lista está vacía, el nuevo nodo es la cabeza.
            }
            else
            {
                Nodo actual = cabeza;
                // Recorre la lista hasta encontrar el último nodo (aquel cuyo 'Siguiente' es null).
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo; // El último nodo ahora apunta al nuevo nodo.
            }
        }

        // Cuenta el número total de elementos (nodos) en la lista.
        public int Contar()
        {
            int contador = 0;
            Nodo? actual = cabeza; // Se usa '?' para indicar que puede ser nulo.
            // Recorre la lista desde la cabeza hasta el final, incrementando el contador.
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }

        // Muestra todos los elementos de la lista en la consola.
        public void Mostrar()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            Nodo? actual = cabeza; 
            while (actual != null)
            {
                Console.Write(actual.Dato + " ");
                actual = actual.Siguiente;
            }
            Console.WriteLine(); // Añade un salto de línea al final para una mejor presentación.
        }
    }
}