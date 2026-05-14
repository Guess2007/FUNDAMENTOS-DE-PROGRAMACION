using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int aux = 0;
            string a;
            int[] b = { 0, 0};
            int c;
            bool correct = true ; 
            Console.WriteLine("ingrese los numeros que quiere sumar :\n");
            do
            {
                for (int i = 0; i < 2; i++) 
                {
                   
                    Console.Write( i+1+" numero : ");
                    a = Console.ReadLine(); correct = int.TryParse(a, out c);
                    
                    if (!correct)
                    {
                        Console.WriteLine("Porfavor ingresar un numero valido :");
                    }
                    aux++;
                    b[i]= c;
                }
                if (aux > 2) 
                {
                    correct = false;
                    Console.WriteLine("QUE LOS 2 SEAN VALIDOS ");
                    aux = 0;
                } 
               

            } while (!correct);
            c = b[0] + b[1];


            Console.WriteLine("la suma :"+ c);
        }
    }
}
