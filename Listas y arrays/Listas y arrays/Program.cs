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
                    List<int> listaNumerica = new List<int>();
                    listaNumerica.Capacity = n1;

                    Console.WriteLine("ingrese sus elementos en la siguiente linea: ");
                    for (int i = 0; i < n1; i++)
                    {
                        int reader1;
                        Console.WriteLine("elemento nº" + (i + 1) + ":"); reader1 = int.Parse(Console.ReadLine());
                        listaNumerica.Add(reader1);
                    }
                    foreach (int i in listaNumerica)
                    {
                        Console.WriteLine("elemento nº" + i + ": " + i);
                    }
                    Console.WriteLine("Su lista ha sido creada exitosamente! ¿Desea realizar algun proceso extra? (s/n)");
                    n = int.Parse(Console.ReadLine());
                    if (n == 1) 
                    {
                        funciones_numericas(listaNumerica);
                    }
                    else if (n == 0)
                    {
                        Console.WriteLine("Gracias por usar el programa, vuelva pronto!");
                        Environment.Exit(0);
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
        static public List<int> funciones_numericas(List<int> lista) 
        {
            int n, n1;
            Console.WriteLine("Bienvenido al menu de funciones extra, ¿que proceso desea ejecutar?" +
                "\n1. insertar elementos \n2. buscar elementos\n3. modificar lista\n4. eliminar lista\n5. ordenar lista");
            n = int.Parse(Console.ReadLine());
            switch (n) 
            { 
                case 1:
                    Console.WriteLine("ingrese el dato a insertar: "); n = int.Parse(Console.ReadLine());
                    Console.WriteLine("ingrese la posicion a insertar: "); n1 = int.Parse(Console.ReadLine());
                    lista.Insert(n1, n);
                    Console.WriteLine("Lista actualizada:");
                    foreach (int i in lista)
                    {
                        Console.WriteLine("elemento nº" + i + ": " + i);
                    }
                    break;
                case 2:
                    Console.WriteLine("ingrese el dato a buscar: "); n = int.Parse(Console.ReadLine());
                    var result = lista.Find(x => x == n);
                    if (result == null)
                    {
                        Console.WriteLine("Elemento no encontrado.");
                    }
                    else
                    {
                        Console.WriteLine("Elemento encontrado: " + result);
                        return lista;                       
                    }
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }
            return lista;
        }
        static public void Array()
        {
            Console.WriteLine("Desea empezar? (s/n)"); char lector;
            while (!char.TryParse(Console.ReadLine(), out lector) || (lector != 's' && lector != 'n'))
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
