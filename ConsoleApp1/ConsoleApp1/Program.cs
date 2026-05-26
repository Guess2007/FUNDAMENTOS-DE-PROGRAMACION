using Microsoft.SqlServer.Server;
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
        static public void anotaciones_y_descubrimientos()
        {
            //PD: para el que llegue a revisar este codigo, este como tal tendra un par de cosas mas debido a que mi grandiosa mente se puso a trabajar los arrays en uno y las listas en otro,
            //por lo que a la hora de leer ambos archivos o tratar de modificarlos, honestamente no se porque alguien querria hacer eso, pero eso, tienen similares estructuras, pero estan atrasados en los metodos segun el metodo que sea trabajdo
            //en este caso, este esta atrasado en listas ya que esta mas centrado a array, que aun esta muy atrasado honestamente...

            /* ubicacion de la anotacion original: metodo "main"
             * nameof, tal y como lo dice su nombre, invoca el nombre de la variable, clase, método, etc. que se le indique, esto es útil para mostrar
             * el nombre de un array o lista sin necesidad de escribirlo manualmente, lo cual puede ser tedioso y propenso a errores, además de que si 
             * se cambia el nombre de la variable, el código seguirá funcionando correctamente sin necesidad de modificar el nombre en cada línea donde 
             * se muestre.
            int[] numeros = {1, 2, 3, 4, 5 };
            Console.WriteLine("================tu array================");
            Console.WriteLine("array con nombre: " + nameof(numeros));
            for (int i = 0; i < numeros.Length; i++)
            {
                
                Console.WriteLine("elemento nº" + (i + 1) + ": " + numeros[i]);
            }
            *ubicacion de la anotacion original: metodo array           
            string.IsNullOrEmpty(Console.ReadLine()) y string.IsNullOrWhiteSpace(Console.ReadLine())
            para validar que el usuario no ingrese un string vacio o solo espacios en blanco, respectivamente.
            toma esto en cuenta para cuando quieras hacer algo que verifique y apliques un bucle while o do -while
            en este caso, no lo aplique ya que pude reparar el bucle de la forma que queria
            sin embargo, esto puede implementarse a futuro...
            */

        }
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
            Console.WriteLine("Desea empezar? (s/n)"); char lector;
            while (!char.TryParse(Console.ReadLine(), out lector) || (lector != 's' && lector != 'n'))
            {
                Console.WriteLine("ingrese un dato valido");
            }
            Console.Clear();
            if (lector == 's')
            {
                Console.Write("¿Que tipo de array desea?");
                Console.WriteLine("1.Numerico\n2. String\n3. Char"); int lector1;
                while (!int.TryParse(Console.ReadLine(), out lector1) || (lector1 != 1 && lector1 != 2 && lector1 != 3))
                {
                    Console.WriteLine("ingrese un dato valido...");
                }
                Console.Clear();
                selector_array(lector1);
            }
            else
            {
                Console.WriteLine("Saliendo del sistema");
                Environment.Exit(20);
            }
        }
        static public int selector_array(int n)
        {
            do
            {
                if (n == 1)
                {
                    Console.WriteLine("enviando al proceso de array numerico...");
                    array_numerico();
                }
                if (n == 2)
                {
                    Console.WriteLine("enviando al proceso de array de strings...");
                    array_strings();
                }
                if (n == 3)
                {
                    Console.WriteLine("enviando al proceso de array de caracteres...");
                    array_char();
                }
                else if (n < 1 || n > 3)
                {
                    Console.WriteLine("Comando invalido, intenta de nuevo...");
                }
            } while (n < 1 || n > 3);
            return n;
        }
        static public void array_numerico()
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
                    Console.WriteLine("Su array" + nameof(a));
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine("elemento nº" + a[i] + ":");
                    }
                    Console.WriteLine("Desea realizar alguna operacion con su array? (s/n)"); string lector = Console.ReadLine();  
                    char lector_char = Convert.ToChar(lector);
                    if (lector_char == 's') 
                    { 
                    }
                    if (lector_char == 'n') 
                    {
                    }
                    int intentos = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        while (!char.TryParse(Console.ReadLine(), out lector_char) || (lector_char != 's' && lector_char != 'n'))
                        {

                            Console.WriteLine("ingrese un dato valido");
                            intentos++;
                            Console.WriteLine("Intento " + intentos + " de 3");
                        }
                    }
                    if (n == 3)
                    {
                        Console.WriteLine("Intentos agotados, saliendo del sistema...");
                        Environment.Exit(20);
                    }
                }
                else if (l == "n")
                {
                    Console.WriteLine("Saliendo del sistema");
                    Environment.Exit(20);
                }
            } while (l == "s");
        }
        static public void array_strings()
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
        static public void array_char()
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
        static public Array funciones_numericas(int[] a)
        {
            

            return a;
        }
    }
}
