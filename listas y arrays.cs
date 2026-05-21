using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Indique que proceso desea ejecutar");
            int n = int.Parse(Console.ReadLine());

            while (!n.TryParse(Console.Readline(), n out))
            {
                Console.WriteLine("Dato invalido, intente de nuevo...");
            }
        }
    }
}