using System;
using System.Collections.Generic;

// Clase que representa un nodo del árbol
class Nodo
{
    public string Titulo;
    public Nodo Izquierdo;
    public Nodo Derecho;

    public Nodo(string titulo)
    {
        Titulo = titulo;
    }
}

// Clase para gestionar el catálogo de revistas
class CatalogoRevistas
{
    private Nodo _raiz;

    // Insertar un nuevo título de revista en el árbol
    public void Insertar(string titulo)
    {
        _raiz = InsertarRecursivo(_raiz, titulo);
    }

    private Nodo InsertarRecursivo(Nodo actual, string titulo)
    {
        if (actual == null)
        {
            return new Nodo(titulo);
        }

        if (string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase) < 0)
        {
            actual.Izquierdo = InsertarRecursivo(actual.Izquierdo, titulo);
        }
        else if (string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase) > 0)
        {
            actual.Derecho = InsertarRecursivo(actual.Derecho, titulo);
        }
        return actual;
    }

    // Método principal para eliminar un título
    public void Eliminar(string titulo)
    {
        _raiz = EliminarRecursivo(_raiz, titulo);
    }

    private Nodo EliminarRecursivo(Nodo actual, string titulo)
    {
        if (actual == null) return null;

        int comparacion = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);

        if (comparacion < 0)
        {
            actual.Izquierdo = EliminarRecursivo(actual.Izquierdo, titulo);
        }
        else if (comparacion > 0)
        {
            actual.Derecho = EliminarRecursivo(actual.Derecho, titulo);
        }
        else
        {
            // Caso 1: Nodo sin hijos o con un solo hijo
            if (actual.Izquierdo == null)
            {
                return actual.Derecho;
            }
            if (actual.Derecho == null)
            {
                return actual.Izquierdo;
            }

            // Caso 2: Nodo con dos hijos
            // Reemplazar por el sucesor (mínimo del subárbol derecho)
            Nodo sucesor = Minimo(actual.Derecho);
            actual.Titulo = sucesor.Titulo;
            actual.Derecho = EliminarRecursivo(actual.Derecho, sucesor.Titulo);
        }
        return actual;
    }

    // Encuentra el nodo con el valor mínimo en un subárbol
    private Nodo Minimo(Nodo actual)
    {
        while (actual.Izquierdo != null) actual = actual.Izquierdo;
        return actual;
    }

    // Búsqueda iterativa (usando un bucle)
    public bool BuscarIterativo(string titulo)
    {
        Nodo actual = _raiz;
        while (actual != null)
        {
            int comparacion = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
            if (comparacion == 0) return true;
            if (comparacion < 0)
            {
                actual = actual.Izquierdo;
            }
            else
            {
                actual = actual.Derecho;
            }
        }
        return false;
    }

    // Búsqueda recursiva (usando llamadas a la función)
    public bool BuscarRecursivo(string titulo)
    {
        return BuscarRecursivo(_raiz, titulo);
    }

    private bool BuscarRecursivo(Nodo actual, string titulo)
    {
        if (actual == null) return false;
        int comparacion = string.Compare(titulo, actual.Titulo, StringComparison.OrdinalIgnoreCase);
        if (comparacion == 0) return true;
        if (comparacion < 0)
        {
            return BuscarRecursivo(actual.Izquierdo, titulo);
        }
        else
        {
            return BuscarRecursivo(actual.Derecho, titulo);
        }
    }

    // Método para obtener y mostrar todos los títulos
    public List<string> ObtenerCatalogoOrdenado()
    {
        var lista = new List<string>();
        InordenRecursivo(_raiz, lista);
        return lista;
    }

    // Recorrido Inorden para obtener los títulos ordenados
    private void InordenRecursivo(Nodo actual, List<string> lista)
    {
        if (actual == null) return;
        InordenRecursivo(actual.Izquierdo, lista);
        lista.Add(actual.Titulo);
        InordenRecursivo(actual.Derecho, lista);
    }
}

// Clase principal que contiene el menú interactivo
class Program
{
    static void Main(string[] args)
    {
        var catalogo = new CatalogoRevistas();

        // Ingresar al menos 10 títulos iniciales en español
        string[] titulosIniciales = {
            "El País", "Hola", "Muy Interesante",
            "National Geographic", "Selecciones",
            "Mundo Científico", "Revista Digital",
            "Familia", "Orve Hogar", "Yanbal"
        };
        foreach (var titulo in titulosIniciales)
        {
            catalogo.Insertar(titulo);
        }

        while (true)
        {
            Console.WriteLine("\n--- Catálogo de Revistas ---");
            Console.WriteLine("1. Insertar un nuevo título");
            Console.WriteLine("2. Eliminar un título");
            Console.WriteLine("3. Buscar un título (Iterativo)");
            Console.WriteLine("4. Buscar un título (Recursivo)");
            Console.WriteLine("5. Mostrar todos los títulos");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el título a insertar: ");
                    string nuevoTitulo = Console.ReadLine();
                    catalogo.Insertar(nuevoTitulo);
                    Console.WriteLine($"Título '{nuevoTitulo}' insertado.");
                    break;
                case "2":
                    Console.Write("Ingrese el título a eliminar: ");
                    string tituloAEliminar = Console.ReadLine();
                    if (catalogo.BuscarIterativo(tituloAEliminar))
                    {
                        catalogo.Eliminar(tituloAEliminar);
                        Console.WriteLine($"✔ Título '{tituloAEliminar}' eliminado del catálogo.");
                    }
                    else
                    {
                        Console.WriteLine($"✖ El título '{tituloAEliminar}' no se encontró en el catálogo para ser eliminado.");
                    }
                    break;
                case "3":
                    Console.Write("Ingrese el título a buscar: ");
                    string tituloBusquedaIterativa = Console.ReadLine();
                    bool encontradoIterativo = catalogo.BuscarIterativo(tituloBusquedaIterativa);
                    if (encontradoIterativo)
                    {
                        Console.WriteLine($"✔ ¡El título '{tituloBusquedaIterativa}' está en el catálogo!");
                    }
                    else
                    {
                        Console.WriteLine($"✖ El título '{tituloBusquedaIterativa}' no se encontró.");
                    }
                    break;
                case "4":
                    Console.Write("Ingrese el título a buscar: ");
                    string tituloBusquedaRecursiva = Console.ReadLine();
                    bool encontradoRecursivo = catalogo.BuscarRecursivo(tituloBusquedaRecursiva);
                    if (encontradoRecursivo)
                    {
                        Console.WriteLine($"✔ ¡El título '{tituloBusquedaRecursiva}' está en el catálogo!");
                    }
                    else
                    {
                        Console.WriteLine($"✖ El título '{tituloBusquedaRecursiva}' no se encontró.");
                    }
                    break;
                case "5":
                    Console.WriteLine("--- Títulos en el Catálogo (Ordenado) ---");
                    var listaOrdenada = catalogo.ObtenerCatalogoOrdenado();
                    if (listaOrdenada.Count > 0)
                    {
                        foreach (var titulo in listaOrdenada)
                        {
                            Console.WriteLine($" - {titulo}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("El catálogo está vacío.");
                    }
                    Console.WriteLine("-----------------------------------------");
                    break;
                case "6":
                    Console.WriteLine("¡Hasta luego!");
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }
}