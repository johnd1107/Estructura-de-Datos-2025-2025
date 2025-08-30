using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Traductor
{
    static string archivoDiccionario = "diccionario.json";

    static Dictionary<string, string> CargarDiccionario()
    {
        if (File.Exists(archivoDiccionario))
        {
            string json = File.ReadAllText(archivoDiccionario);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
        else
        {
            return new Dictionary<string, string>();
        }
    }

    static void GuardarDiccionario(Dictionary<string, string> diccionario)
    {
        string json = JsonSerializer.Serialize(diccionario, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(archivoDiccionario, json);
    }

    static void TraducirFrase(Dictionary<string, string> diccionario)
    {
        Console.Write("Ingrese una frase: ");
        string frase = Console.ReadLine();
        string[] palabras = frase.Split(' ');

        Console.Write("Traducción: ");
        foreach (var palabra in palabras)
        {
            if (diccionario.ContainsKey(palabra.ToLower()))
                Console.Write(diccionario[palabra.ToLower()] + " ");
            else
                Console.Write(palabra + " "); // Si no existe, deja la palabra igual
        }
        Console.WriteLine();
    }

    static void AgregarPalabra(Dictionary<string, string> diccionario)
    {
        Console.Write("Ingrese la palabra en español: ");
        string esp = Console.ReadLine().ToLower();

        if (diccionario.ContainsKey(esp))
        {
            Console.WriteLine("Esa palabra ya existe en el diccionario.");
            return;
        }

        Console.Write("Ingrese la traducción en inglés: ");
        string eng = Console.ReadLine();

        diccionario[esp] = eng;
        GuardarDiccionario(diccionario);
        Console.WriteLine("Palabra agregada correctamente.");
    }

    static void Main(string[] args)
    {
        Dictionary<string, string> diccionario = CargarDiccionario();

        int opcion;
        do
        {
            Console.WriteLine("\n================ MENÚ ================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    TraducirFrase(diccionario);
                    break;
                case 2:
                    AgregarPalabra(diccionario);
                    break;
                case 0:
                    Console.WriteLine("Saliendo del traductor...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != 0);
    }
}
