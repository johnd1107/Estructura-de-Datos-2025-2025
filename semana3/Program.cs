using System;
namespace RegistroEstudiantes
{
    // Clase que representa a un estudiante
    class Estudiante
    {
        // Atributos del estudiante
        public int Id;
        public string Nombres;
        public string Apellidos;
        public string Direccion;
        public string[] Telefonos; // Array para almacenar 3 teléfonos
        // Constructor que recibe e inicializa los datos del estudiante
        public Estudiante(int id, string nombres, string apellidos, string direccion, string[] telefonos)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefonos = telefonos;
        }
        // Método que imprime la información del estudiante
        public void MostrarInformacion()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Nombres: {Nombres}");
            Console.WriteLine($"Apellidos: {Apellidos}");
            Console.WriteLine($"Dirección: {Direccion}");
            Console.WriteLine("Teléfonos:");
            for (int i = 0; i < Telefonos.Length; i++)
            {
                Console.WriteLine($"\tTeléfono {i + 1}: {Telefonos[i]}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un array con tres números de teléfono
            string[] telefonos = new string[3] { "0999999999", "0988888888", "0977777777" };
            // Instanciar un objeto Estudiante con datos de ejemplo
            Estudiante estudiante = new Estudiante(1, "Luis", "Defaz", "Av. Amazonas y 12 de Febrero", telefonos);
            // Llamar al método para mostrar los datos
            estudiante.MostrarInformacion();
            // Esperar que el usuario presione una tecla para cerrar la consola
            Console.ReadLine();
        }
    }
}
