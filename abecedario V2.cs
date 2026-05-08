using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp23
{
    internal class Program
    {
        public class Abecedario
        {
            string[] letras =
            {
            " █████╗ \n██╔══██╗\n███████║\n██╔══██║\n██║  ██║\n╚═╝  ╚═╝", // A

            "██████╗ \n██╔══██╗\n██████╔╝\n██╔══██╗\n██████╔╝\n╚═════╝ ", // B

            " ██████╗\n██╔════╝\n██║     \n██║     \n╚██████╗\n ╚═════╝", // C

            "██████╗ \n██╔══██╗\n██║  ██║\n██║  ██║\n██████╔╝\n╚═════╝ ", // D

            "███████╗\n██╔════╝\n█████╗  \n██╔══╝  \n███████╗\n╚══════╝", // E

            "███████╗\n██╔════╝\n█████╗  \n██╔══╝  \n██║     \n╚═╝     ", // F

            " ██████╗ \n██╔════╝ \n██║  ███╗\n██║   ██║\n╚██████╔╝\n ╚═════╝ ", // G

            "██╗  ██╗\n██║  ██║\n███████║\n██╔══██║\n██║  ██║\n╚═╝  ╚═╝", // H

            "██╗\n██║\n██║\n██║\n██║\n╚═╝", // I

            "     ██╗\n     ██║\n     ██║\n██   ██║\n╚█████╔╝\n ╚════╝ ", // J

            "██╗  ██╗\n██║ ██╔╝\n█████╔╝ \n██╔═██╗ \n██║  ██╗\n╚═╝  ╚═╝", // K

            "██╗     \n██║     \n██║     \n██║     \n███████╗\n╚══════╝", // L

            "███╗   ███╗\n████╗ ████║\n██╔████╔██║\n██║╚██╔╝██║\n██║ ╚═╝ ██║\n╚═╝     ╚═╝", // M

            "███╗   ██╗\n████╗  ██║\n██╔██╗ ██║\n██║╚██╗██║\n██║ ╚████║\n╚═╝  ╚═══╝", // N

            " ██████╗ \n██╔═══██╗\n██║   ██║\n██║   ██║\n╚██████╔╝\n ╚═════╝ ", // O

            "██████╗ \n██╔══██╗\n██████╔╝\n██╔═══╝ \n██║     \n╚═╝     ", // P

            " ██████╗ \n██╔═══██╗\n██║   ██║\n██║▄▄ ██║\n╚██████╔╝\n ╚══▀▀═╝ ", // Q

            "██████╗ \n██╔══██╗\n██████╔╝\n██╔══██╗\n██║  ██║\n╚═╝  ╚═╝", // R

            "███████╗\n██╔════╝\n███████╗\n╚════██║\n███████║\n╚══════╝", // S

            "████████╗\n╚══██╔══╝\n   ██║   \n   ██║   \n   ██║   \n   ╚═╝   ", // T

            "██╗   ██╗\n██║   ██║\n██║   ██║\n██║   ██║\n╚██████╔╝\n ╚═════╝ ", // U

            "██╗   ██╗\n██║   ██║\n██║   ██║\n╚██╗ ██╔╝\n ╚████╔╝ \n  ╚═══╝  ", // V

            "██╗    ██╗\n██║    ██║\n██║ █╗ ██║\n██║███╗██║\n╚███╔███╔╝\n ╚══╝╚══╝ ", // W

            "██╗  ██╗\n╚██╗██╔╝\n ╚███╔╝ \n ██╔██╗ \n██╔╝ ██╗\n╚═╝  ╚═╝", // X

            "██╗   ██╗\n╚██╗ ██╔╝\n ╚████╔╝ \n  ╚██╔╝  \n   ██║   \n   ╚═╝   ", // Y

            "███████╗\n╚══███╔╝\n  ███╔╝ \n ███╔╝  \n███████╗\n╚══════╝" // Z
        };

            public string Obtener(int num)
            {
                if (num >= 1 && num <= 26)
                {
                    return letras[num - 1];
                }

                return "";
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Ingresa el texto:");
            string texto = Console.ReadLine();
            Console.Clear();

            List<int> numeros = new List<int>();

            // Convertir letras a números
            for (int i = 0; i < texto.Length; i++)
            {
                char letra = char.ToUpper(texto[i]);

                if (letra == ' ')
                {
                    numeros.Add(0);
                }
                else if (letra >= 'A' && letra <= 'Z')
                {
                    numeros.Add(letra - 'A' + 1);
                }
            }

            Console.WriteLine("\nASCII ART:\n");
            

            Abecedario abecedario = new Abecedario();

            // Cada letra tiene 6 filas
            for (int fila = 0; fila < 6; fila++)
            {
                foreach (int numero in numeros)
                {
                    // Espacio entre palabras
                    if (numero == 0)
                    {
                        Console.Write("       ");
                    }
                    else
                    {
                        string letraAscii = abecedario.Obtener(numero);

                        // Separar la letra en líneas
                        string[] lineas = letraAscii.Split('\n');

                        // Imprimir la fila correspondiente
                        Console.Write(lineas[fila] + "  ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
