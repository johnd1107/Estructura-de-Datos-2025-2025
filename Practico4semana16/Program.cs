using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

/// Representa una arista ponderada (vuelo) para la persistencia
public class Vuelo
{
    public string Origen { get; set; }
    public string Destino { get; set; }
    public int Costo { get; set; }
}

// --- Clase de Gestion de Grafo y Algoritmos ---

public class GrafoVuelos
{
    // Lista de Adyacencia: [Ciudad Origen] -> Lista de (Ciudad Destino, Costo)
    private Dictionary<string, List<Tuple<string, int>>> adyacencia;

    public GrafoVuelos()
    {
        adyacencia = new Dictionary<string, List<Tuple<string, int>>>();
    }

    ///Reconstruye el grafo (Lista de Adyacencia) a partir de la lista de vuelos cargada
    public void ConstruirDesdeVuelos(List<Vuelo> vuelos)
    {
        adyacencia.Clear(); 
        
        foreach (var vuelo in vuelos)
        {
            // Añade Origen y su arista ponderada
            if (!adyacencia.ContainsKey(vuelo.Origen))
            {
                adyacencia[vuelo.Origen] = new List<Tuple<string, int>>();
            }
            adyacencia[vuelo.Origen].Add(new Tuple<string, int>(vuelo.Destino, vuelo.Costo));

            // Asegura que Destino exista como nodo
            if (!adyacencia.ContainsKey(vuelo.Destino))
            {
                adyacencia[vuelo.Destino] = new List<Tuple<string, int>>();
            }
        }
    }

    ///Algoritmo de Dijkstra: encuentra la ruta de menor costo
    public (List<string> ruta, int costo) EncontrarRutaMasBarata(string inicio, string fin)
    {
        if (!adyacencia.ContainsKey(inicio) || !adyacencia.ContainsKey(fin))
        {
            return (new List<string>(), -1);
        }

        var distancias = adyacencia.Keys.ToDictionary(k => k, v => int.MaxValue);
        var previos = adyacencia.Keys.ToDictionary(k => k, v => (string)null);
        var colaPrioridad = new SortedList<int, string>(); 
        
        distancias[inicio] = 0;
        colaPrioridad.Add(0, inicio); 

        while (colaPrioridad.Count > 0)
        {
            var actualCosto = colaPrioridad.Keys.First();
            var actualCiudad = colaPrioridad.Values.First();
            colaPrioridad.RemoveAt(0); 

            if (actualCiudad.Equals(fin)) break;
            if (actualCosto > distancias[actualCiudad]) continue;

            foreach (var vecino in adyacencia[actualCiudad])
            {
                var siguienteCiudad = vecino.Item1;
                var costoVuelo = vecino.Item2;
                var nuevoCosto = actualCosto + costoVuelo;

                if (nuevoCosto < distancias[siguienteCiudad])
                {
                    distancias[siguienteCiudad] = nuevoCosto;
                    previos[siguienteCiudad] = actualCiudad;
                    // Añadir la nueva distancia a la cola de prioridad
                    colaPrioridad.Add(nuevoCosto, siguienteCiudad); 
                }
            }
        }

        // Reconstrucción de la ruta
        var ruta = new List<string>();
        if (distancias[fin] == int.MaxValue) return (new List<string>(), -1);
        
        var paso = fin;
        while (paso != null)
        {
            ruta.Insert(0, paso);
            if (paso.Equals(inicio)) break;
            paso = previos[paso];
        }

        return (ruta, distancias[fin]);
    }
}

// Clase de Persistencia y Control del Programa

public class GestorVuelos
{
    private const string JsonPath = "vuelos.json";
    private List<Vuelo> vuelosData = new List<Vuelo>();
    private GrafoVuelos grafo;

    public GestorVuelos()
    {
        grafo = new GrafoVuelos();
        CargarVuelos();
    }

    // Funciones de Persistencia

    private List<Vuelo> ObtenerDatosIniciales()
    {
        return new List<Vuelo>
        {
            new Vuelo { Origen = "Quito", Destino = "Guayaquil", Costo = 50 },
            new Vuelo { Origen = "Quito", Destino = "Manta", Costo = 80 },
            new Vuelo { Origen = "Guayaquil", Destino = "Cuenca", Costo = 60 },
            new Vuelo { Origen = "Manta", Destino = "Cuenca", Costo = 30 },
            new Vuelo { Origen = "Cuenca", Destino = "Loja", Costo = 40 },
            new Vuelo { Origen = "Manta", Destino = "Loja", Costo = 150 },
            new Vuelo { Origen = "Guayaquil", Destino = "Loja", Costo = 120 }
        };
    }

    private void GuardarVuelos()
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(vuelosData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(JsonPath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[ERROR DE GUARDADO]: No se pudo guardar el archivo JSON. {ex.Message}");
        }
    }

    private void CargarVuelos()
    {
        if (!File.Exists(JsonPath))
        {
            Console.WriteLine($"[INFO]: Archivo '{JsonPath}' no encontrado. Creando base de datos inicial...");
            vuelosData = ObtenerDatosIniciales();
            GuardarVuelos();
        }
        else
        {
            try
            {
                string jsonString = File.ReadAllText(JsonPath);
                vuelosData = JsonSerializer.Deserialize<List<Vuelo>>(jsonString) ?? new List<Vuelo>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR DE CARGA]: Error al leer o deserializar '{JsonPath}'. Usando datos internos. {ex.Message}");
                vuelosData = ObtenerDatosIniciales();
            }
        }
        // Reconstruir el grafo cada vez que se cargan o modifican los datos
        grafo.ConstruirDesdeVuelos(vuelosData);
    }

    // --- Funciones del Menu (CRUD y Consulta) ---

    /// Opcion 1: Consulta la ruta mas barata usando Dijkstra
    public void ConsultarRuta()
    {
        Console.WriteLine("\n--- 1. CONSULTA DE RUTA MÁS BARATA (DIJKSTRA) ---");
        Console.Write("Ingrese Ciudad de Origen: ");
        string origen = Console.ReadLine().Trim();
        Console.Write("Ingrese Ciudad de Destino: ");
        string destino = Console.ReadLine().Trim();

        var (ruta, costo) = grafo.EncontrarRutaMasBarata(origen, destino);

        if (costo != -1 && ruta.Count > 0)
        {
            Console.WriteLine($"\n✅ RUTA OPTIMA: {string.Join(" -> ", ruta)}");
            Console.WriteLine($"Costo Total Minimo: ${costo}");
        }
        else
        {
            Console.WriteLine($"\n❌ No se encontró una ruta de {origen} a {destino} o alguna ciudad no existe.");
        }
    }

    /// Opcion 2: Muestra el reporte de todos los vuelos (Reportería)
    public void VisualizarReporte()
    {
        Console.WriteLine("\n--- 2. REPORTE COMPLETO DE VUELOS (ELEMENTOS DEL GRAFO) ---");
        if (vuelosData.Count == 0)
        {
            Console.WriteLine("La base de datos de vuelos está vacía.");
            return;
        }

        Console.WriteLine($"Total de vuelos (Aristas Ponderadas): {vuelosData.Count}\n");
        int i = 1;
        foreach (var v in vuelosData)
        {
             Console.WriteLine($"{i++.ToString().PadLeft(2)}. {v.Origen} -> {v.Destino} | Costo: ${v.Costo}");
        }
    }

    /// Opcion 3: Agrega un nuevo vuelo (Ingresar)
    public void IngresarVuelo()
    {
        Console.WriteLine("\n--- 3. INGRESAR NUEVO VUELO ---");
        Console.Write("Origen: ");
        string origen = Console.ReadLine().Trim();
        Console.Write("Destino: ");
        string destino = Console.ReadLine().Trim();
        Console.Write("Costo (solo numeros): ");
        if (int.TryParse(Console.ReadLine(), out int costo) && costo >= 0)
        {
            var nuevoVuelo = new Vuelo { Origen = origen, Destino = destino, Costo = costo };
            vuelosData.Add(nuevoVuelo);
            GuardarVuelos();
            CargarVuelos(); // Reconstruir el grafo
            Console.WriteLine($"\n✅ Vuelo '{origen} -> {destino}' con costo ${costo} ingresado con éxito.");
        }
        else
        {
            Console.WriteLine("\n❌ Costo no válido. Vuelo no ingresado.");
        }
    }

    /// Opcion 4: Elimina un vuelo existente (Eliminar)
    public void EliminarVuelo()
    {
        Console.WriteLine("\n--- 4. ELIMINAR VUELO ---");
        Console.Write("Origen del vuelo a eliminar: ");
        string origen = Console.ReadLine().Trim();
        Console.Write("Destino del vuelo a eliminar: ");
        string destino = Console.ReadLine().Trim();

        int countAntes = vuelosData.Count;
        
        // Elimina todos los vuelos que coincidan con Origen y Destino
        vuelosData.RemoveAll(v => v.Origen.Equals(origen, StringComparison.OrdinalIgnoreCase) && 
                                  v.Destino.Equals(destino, StringComparison.OrdinalIgnoreCase));

        if (vuelosData.Count < countAntes)
        {
            GuardarVuelos();
            CargarVuelos(); // Reconstruir el grafo
            Console.WriteLine($"\n✅ Vuelo '{origen} -> {destino}' eliminado con éxito.");
        }
        else
        {
            Console.WriteLine($"\n❌ Vuelo '{origen} -> {destino}' no encontrado. Nada fue eliminado.");
        }
    }
}

public class Programa
{
    public static void MostrarMenu()
    {
        Console.WriteLine("\n========================================================");
        Console.WriteLine("Universidad Estatal Amazonica");
        Console.WriteLine("Integrantes:");
        Console.WriteLine("Luis John Defaz Diaz");
        Console.WriteLine("Jonathan Andres Felix Cisneros");
        Console.WriteLine("Joselyn Mayli Angamarca Angamarca");
        Console.WriteLine("    PRACTICA #04: GRAFOS Y ALGORITMO DE DIJKSTRA");
        Console.WriteLine("========================================================");
        Console.WriteLine("1. Consultar Ruta mas Barata");
        Console.WriteLine("2. Visualizar Reporte de Vuelos");
        Console.WriteLine("3. Ingresar Nuevo Vuelo");
        Console.WriteLine("4. Eliminar Vuelo");
        Console.WriteLine("5. Salir del Programa");
        Console.WriteLine("--------------------------------------------------------");
        Console.Write("Seleccione una opción: ");
    }

    public static void Main()
    {
        var gestor = new GestorVuelos();
        bool continuar = true;

        while (continuar)
        {
            MostrarMenu();
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    gestor.ConsultarRuta();
                    break;
                case "2":
                    gestor.VisualizarReporte();
                    break;
                case "3":
                    gestor.IngresarVuelo();
                    break;
                case "4":
                    gestor.EliminarVuelo();
                    break;
                case "5":
                    continuar = false;
                    Console.WriteLine("\nPrograma finalizado. ¡Gracias!");
                    break;
                default:
                    Console.WriteLine("\nOpción no valida. Intente de nuevo.");
                    break;
            }
            if(continuar)
            {
                Console.WriteLine("\nPresione ENTER para continuar...");
                Console.ReadLine();
            }
        }
    }
}