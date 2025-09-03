using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class TorneoFutbol
{
    // Diccionario (mapa) que asocia equipo -> conjunto de jugadores
    static Dictionary<string, HashSet<string>> equipos = new Dictionary<string, HashSet<string>>();

    // Archivo para guardar los datos
    static string archivo = "torneo.json";

    static void Main(string[] args)
    {
        CargarDatos(); // Cargar datos guardados al iniciar

        int opcion;
        do
        {
            // Menú principal
            Console.WriteLine("\n=== MENÚ TORNEO DE FÚTBOL ===");
            Console.WriteLine("1. Registrar equipo");
            Console.WriteLine("2. Agregar jugador a un equipo");
            Console.WriteLine("3. Ver equipos registrados");
            Console.WriteLine("4. Ver jugadores de un equipo");
            Console.WriteLine("5. Ver todos los jugadores del torneo");
            Console.WriteLine("6. Eliminar jugador de un equipo");
            Console.WriteLine("7. Eliminar equipo");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    RegistrarEquipo();
                    break;
                case 2:
                    AgregarJugador();
                    break;
                case 3:
                    VerEquipos();
                    break;
                case 4:
                    VerJugadoresEquipo();
                    break;
                case 5:
                    VerTodosLosJugadores();
                    break;
                case 6:
                    EliminarJugador();
                    break;
                case 7:
                    EliminarEquipo();
                    break;
                case 0:
                    GuardarDatos(); // Guardar antes de salir
                    Console.WriteLine("Datos guardados. Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != 0);
    }

    // Registrar equipo
    static void RegistrarEquipo()
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string nombreEquipo = Console.ReadLine();

        if (!equipos.ContainsKey(nombreEquipo))
        {
            equipos[nombreEquipo] = new HashSet<string>();
            Console.WriteLine("Equipo registrado correctamente.");
        }
        else
        {
            Console.WriteLine("El equipo ya está registrado.");
        }
        GuardarDatos();
    }

    // Agregar jugador a un equipo
    static void AgregarJugador()
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string equipo = Console.ReadLine();

        if (equipos.ContainsKey(equipo))
        {
            Console.Write("Ingrese el nombre del jugador: ");
            string jugador = Console.ReadLine();

            if (equipos[equipo].Add(jugador)) // HashSet evita duplicados
            {
                Console.WriteLine("Jugador agregado correctamente.");
            }
            else
            {
                Console.WriteLine("El jugador ya está registrado en este equipo.");
            }
        }
        else
        {
            Console.WriteLine("El equipo no existe.");
        }
        GuardarDatos();
    }

    // Ver todos los equipos
    static void VerEquipos()
    {
        Console.WriteLine("\nEquipos registrados:");
        foreach (var equipo in equipos.Keys)
        {
            Console.WriteLine("- " + equipo);
        }
    }

    // Ver jugadores de un equipo
    static void VerJugadoresEquipo()
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string equipo = Console.ReadLine();

        if (equipos.ContainsKey(equipo))
        {
            Console.WriteLine($"\nJugadores del equipo {equipo}:");
            foreach (var jugador in equipos[equipo])
            {
                Console.WriteLine("- " + jugador);
            }
        }
        else
        {
            Console.WriteLine("El equipo no está registrado.");
        }
    }

    // Ver todos los jugadores del torneo
    static void VerTodosLosJugadores()
    {
        HashSet<string> todos = new HashSet<string>();

        foreach (var jugadores in equipos.Values)
        {
            todos.UnionWith(jugadores); // Unir jugadores de todos los equipos
        }

        Console.WriteLine("\nTodos los jugadores del torneo:");
        foreach (var jugador in todos)
        {
            Console.WriteLine("- " + jugador);
        }
    }

    // Eliminar un jugador de un equipo
    static void EliminarJugador()
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string equipo = Console.ReadLine();

        if (equipos.ContainsKey(equipo))
        {
            Console.Write("Ingrese el nombre del jugador a eliminar: ");
            string jugador = Console.ReadLine();

            if (equipos[equipo].Remove(jugador))
            {
                Console.WriteLine("Jugador eliminado correctamente.");
            }
            else
            {
                Console.WriteLine("El jugador no existe en este equipo.");
            }
        }
        else
        {
            Console.WriteLine("El equipo no está registrado.");
        }
        GuardarDatos();
    }

    // Eliminar un equipo completo
    static void EliminarEquipo()
    {
        Console.Write("Ingrese el nombre del equipo a eliminar: ");
        string equipo = Console.ReadLine();

        if (equipos.Remove(equipo))
        {
            Console.WriteLine("Equipo eliminado correctamente.");
        }
        else
        {
            Console.WriteLine("El equipo no existe.");
        }
        GuardarDatos();
    }

    // Guardar datos en archivo JSON
    static void GuardarDatos()
    {
        string json = JsonSerializer.Serialize(equipos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(archivo, json);
    }

    // Cargar datos desde archivo JSON
    static void CargarDatos()
    {
        if (File.Exists(archivo))
        {
            string json = File.ReadAllText(archivo);
            equipos = JsonSerializer.Deserialize<Dictionary<string, HashSet<string>>>(json);
        }
    }
}
