using System;

namespace AgendaTelefonica
{
    class Contacto
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"Nombre: {Nombre}, Teléfono: {Telefono}, Correo: {Correo}");
        }
    }

    class Program
    {
        static Contacto[] agenda = new Contacto[100];
        static int contador = 0;

        static void Main()
        {
            int opcion;
            do
            {
                Console.WriteLine("\n--- MENÚ AGENDA ---");
                Console.WriteLine("1. Agregar contacto");
                Console.WriteLine("2. Mostrar contactos");
                Console.WriteLine("3. Buscar contacto");
                Console.WriteLine("4. Eliminar contacto");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese un número válido.");
                    continue;
                }

                switch (opcion)
                {
                    case 1: AgregarContacto(); break;
                    case 2: MostrarContactos(); break;
                    case 3: BuscarContacto(); break;
                    case 4: EliminarContacto(); break;
                    case 5: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opción inválida."); break;
                }
            } while (opcion != 5);
        }

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
                agenda[contador++] = nuevo;
                Console.WriteLine("Contacto agregado con éxito.");
            }
            else
            {
                Console.WriteLine("Agenda llena.");
            }
        }

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
                agenda[i].Mostrar();
            }
        }

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
                    // Mover los contactos posteriores una posición hacia atrás
                    for (int j = i; j < contador - 1; j++)
                    {
                        agenda[j] = agenda[j + 1];
                    }
                    agenda[--contador] = null;
                    Console.WriteLine("Contacto eliminado.");
                    return;
                }
            }
            Console.WriteLine("Contacto no encontrado.");
        }
    }
}


