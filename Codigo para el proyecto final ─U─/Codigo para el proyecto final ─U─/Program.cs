using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codigo_para_el_proyecto_final__U_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Bienvenido al sistema de registro de trabajadores");
            Console.WriteLine("Registre el numero de trabajadores: ");
            int numeroDeUsuarios = int.Parse(Console.ReadLine());
            string[] trabajadores = new string[numeroDeUsuarios];
            Console.WriteLine("Registre el nombre de los trabajadores ");
            for (int i = 0; i < numeroDeUsuarios; i++) 
            {               
                Console.WriteLine("Trabajador nº" + (i + 1) + ": ");
                trabajadores[i] = Console.ReadLine();
            }
            Console.WriteLine("Los trabajadores registrados son: ");
            for (int i = 0; i < numeroDeUsuarios; i++) 
            {
                Console.WriteLine("Trabajador nº" + (i + 1) + ": " + trabajadores[i]);
            }
            */
            Console.WriteLine("Bienvenido al sistema de registro de trabajadores");
            Console.WriteLine("Ingrese el numero de trabajadores: "); int numeroDeUsuarios = int.Parse(Console.ReadLine());
            string[,] trabajadores = new string[numeroDeUsuarios, 3];
            Console.WriteLine("");
            for (int i = 0; i < trabajadores.Length; i++) 
            {
                for (int j = 0; j < 2; j++) 
                {
                    Console.WriteLine("ingrese el nombre del trabajador: ");
                    trabajadores[i, 0] = Console.ReadLine();
                    Console.WriteLine("ingrese el rol del trabajdor: ");
                    trabajadores[i, 1] = Console.ReadLine();
                    Console.WriteLine("ingrese el salario del trabajador: ");
                    trabajadores[i, 2] = Console.ReadLine();
                }
            }
            Console.WriteLine("su lista: ");
            for (int i = 0; i < trabajadores.Length; i++) 
            {
                for (int j = 0; j < 2; j++) 
                {
                    Console.WriteLine("Nombre: " + trabajadores[i, 0]);
                    Console.WriteLine("Rol: " + trabajadores[i, 1]);
                    Console.WriteLine("Salario: " + trabajadores[i, 2]);
                }
            }

        }
    }
}
