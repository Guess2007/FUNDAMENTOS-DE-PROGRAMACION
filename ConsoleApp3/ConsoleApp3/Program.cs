using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Persona> personas = new List<Persona>()
{
    new Persona { Nombre = "Ana", Edad = 25 },
    new Persona { Nombre = "Luis", Edad = 40 },
    new Persona { Nombre = "Juan", Edad = 30 }
};

            // Ordenar de mayor a menor por la propiedad 'Edad'
            var personasOrdenadas = personas.OrderByDescending(p => p.Edad).ToList();
            for (int i = 0; i < personasOrdenadas.Count; i++)
            {
                Console.WriteLine($"Nombre: {personasOrdenadas[i].Nombre}, Edad: {personasOrdenadas[i].Edad}");
            }
        }
    }
}
