using System;

namespace AgendaTelefonica
{
    // Clase que representa un contacto en la agenda
    class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        // Método para mostrar la información de un contacto
        public void Mostrar()
        {
            Console.WriteLine($"Nombre: {Nombre}, Teléfono: {Telefono}, Correo: {Correo}");
        }
    }

    class Program
    {
        // Arreglo para almacenar los contactos
        static Contacto[] agenda = new Contacto[100];
        static int contador = 0; // Lleva la cuenta de contactos ingresados

        static void Main()
        {
            int opcion;
            do
            {
                // Mostrar el menú principal
                Console.WriteLine("\n--- MENÚ AGENDA ---");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Mostrar contactos");
                Console.WriteLine("3. Buscar contacto");
                Console.WriteLine("4. Eliminar contacto");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                // Validar que la entrada sea un número
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    continue;
                }

                // Ejecutar la opción seleccionada
                switch (opcion)
                {
                    case 1: AgregarContacto(); break;
                    case 2: MostrarContactos(); break;
                    case 3: BuscarContacto(); break;
                    case 4: EliminarContacto(); break;
                    case 5: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            } while (opcion != 5); // Repetir hasta que el usuario elija salir
        }

        // Método para agregar un nuevo contacto
        static void AgregarContacto()
        {
            if (contador < agenda.Length)
            {
                Contacto nuevo = new Contacto();
                Console.Write("Ingrese nombre: ");
                nuevo.Nombre = Console.ReadLine();
                Console.Write("Ingrese teléfono: ");
                nuevo.Telefono = Console.ReadLine();
                Console.Write("Ingrese correo: ");
                nuevo.Correo = Console.ReadLine();

                agenda[contador++] = nuevo; // Guardar el nuevo contacto
                Console.WriteLine("Contacto agregado con éxito.");
            }
            else
            {
                Console.WriteLine("Agenda llena.");
            }
        }

        // Método para mostrar todos los contactos
        static void MostrarContactos()
        {
            if (contador == 0)
            {
                Console.WriteLine("No hay contactos en la agenda.");
                return;
            }

            Console.WriteLine("\n--- Lista de contactos ---");
            for (int i = 0; i < contador; i++)
            {
                Console.Write($"{i + 1}. ");
                agenda[i].Mostrar(); // Mostrar cada contacto
            }
        }

        // Método para buscar un contacto por nombre
        static void BuscarContacto()
        {
            if (contador == 0)
            {
                Console.WriteLine("No hay contactos para buscar.");
                return;
            }

            Console.Write("Ingrese nombre a buscar: ");
            string nombre = Console.ReadLine().ToLower();
            bool encontrado = false;

            for (int i = 0; i < contador; i++)
            {
                if (agenda[i].Nombre.ToLower() == nombre)
                {
                    agenda[i].Mostrar();
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("Contacto no encontrado.");
            }
        }

        // Método para eliminar un contacto por nombre
        static void EliminarContacto()
        {
            if (contador == 0)
            {
                Console.WriteLine("No hay contactos para eliminar.");
                return;
            }

            Console.Write("Ingrese nombre a eliminar: ");
            string nombre = Console.ReadLine().ToLower();

            for (int i = 0; i < contador; i++)
            {
                if (agenda[i].Nombre.ToLower() == nombre)
                {
                    // Desplazar los contactos hacia atrás para eliminar el contacto actual
                    for (int j = i; j < contador - 1; j++)
                    {
                        agenda[j] = agenda[j + 1];
                    }

                    agenda[--contador] = null; // Eliminar el último duplicado
                    Console.WriteLine("Contacto eliminado.");
                    return;
                }
            }
            Console.WriteLine("Contacto no encontrado.");
        }
    }
}

