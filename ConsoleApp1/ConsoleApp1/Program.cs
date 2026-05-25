using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Indique que proceso desea ejecutar");
            Console.WriteLine("1. Lista \n2. Array");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Dato invalido, intente de nuevo...");
            }
            Console.Clear();
            if (n == 1)
            {
                Lista();
            }
            else if (n == 2)
            {
                Array();
            }
            else
            {
                Console.WriteLine("Comando invalido, intenta de nuevo...");
            }
        }

        static public void Lista()
        {
            Console.WriteLine("¿Que tipo de lista desea?");
            Console.WriteLine("1. Lista numerica \n2. Lista de caracteres \n3. Lista de palabras");

            int n, n1;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Dato invalido, intente de nuevo...");
                Console.Clear();
            }
            Console.WriteLine("¿Cual será el tamaño destinado?");
            while (!int.TryParse(Console.ReadLine(), out n1))
            {
                Console.WriteLine("Dato invalido, intente de nuevo...");
                Console.Clear();
            }
            switch (n)
            {
                case 1:
                    Console.WriteLine("Ejecutando proceso de lista numerica...");
                    List<int> listaNumerica = new List<int>(n1);
                    int reader1;
                    Console.WriteLine("ingrese sus elementos en la siguiente linea: ");
                    while (!int.TryParse(Console.ReadLine(), out reader1))
                    {
                        Console.Write("Dato invalido, intente de nuevo...");
                    }
                    for (int i = 0; i < n1; i++)
                    {
                        Console.WriteLine("elemento nº" + (i + 1) + ":"); listaNumerica.Add(reader1);
                    }

                    foreach (int i in listaNumerica)
                    {
                        Console.WriteLine("elemento nº" + (listaNumerica.IndexOf(i) + 1) + ": " + i);
                    }
                    break;
                case 2:
                    Console.WriteLine("Ejecutando proceso de lista de caracteres...");
                    List<char> listaCaracteres = new List<char>(n1);
                    break;
                case 3:
                    Console.WriteLine("Ejecutando proceso de lista de palabras...");
                    List<string> listaPalabras = new List<string>(n1);
                    break;
                default:
                    Console.WriteLine("Comando invalido, intenta de nuevo...");
                    break;
            }

        }
        static public void Array()
        {

            /* string.IsNullOrEmpty(Console.ReadLine()) y string.IsNullOrWhiteSpace(Console.ReadLine())
            para validar que el usuario no ingrese un string vacio o solo espacios en blanco, respectivamente.
            toma esto en cuenta para cuando quieras hacer algo que verifique y apliques un bucle while o do-while
            en este caso, no lo aplique ya que pude reparar el bucle de la forma que queria
            sin embargo, esto puede implementarse a futuro...*/
            Console.WriteLine("Desea empezar? (s/n)"); char lector; 
            while (!char.TryParse(Console.ReadLine(), out lector) || (lector != 's' && lector != 'n') )
            {
                Console.WriteLine("ingrese un dato valido");
            }
            Console.Clear();
            if (lector == 's')
            {
                string l;
                do
                {
                    Console.WriteLine("Desea empezar? (s/n)"); l = Console.ReadLine();
                    if (l == "s")
                    {
                        Console.WriteLine("ingrese el tamaño del array: "); int n = int.Parse(Console.ReadLine());
                        int[] a = new int[n];
                        for (int i = 0; i < n; i++)
                        {
                            Console.WriteLine("elemento nº" + (i + 1) + ":");
                            a[i] = int.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Su array");
                        for (int i = 0; i < n; i++)
                        {
                            Console.WriteLine("elemento nº" + a[i] + ":");
                        }
                    }
                    else if (l == "n")
                    {
                        Console.WriteLine("Saliendo del sistema");
                        Environment.Exit(20);
                    }
                } while (l == "s");                     
            }
            else
            {
                Console.WriteLine("Saliendo del sistema");
                Environment.Exit(20);
            }           
        }
    }
}
