using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{

    internal class Program
    {
        public class Abecedario
        { 
            public int a(int num)
            {
                string a = "█████╗ \r\n██╔══██╗\r\n███████║\r\n██╔══██║\r\n██║  ██║\r\n╚═╝  ╚═╝";
                string b = "██████╗ \r\n██╔══██╗\r\n██████╔╝\r\n██╔══██╗\r\n██████╔╝\r\n╚═════╝ ";
                string c = " ██████╗\r\n██╔════╝\r\n██║     \r\n██║     \r\n╚██████╗\r\n ╚═════╝";
                string d = "██████╗ \r\n██╔══██╗\r\n██║  ██║\r\n██║  ██║\r\n██████╔╝\r\n╚═════╝ ";
                string e = "███████╗\r\n██╔════╝\r\n█████╗  \r\n██╔══╝  \r\n███████╗\r\n╚══════╝";
                string f = "███████╗\r\n██╔════╝\r\n█████╗  \r\n██╔══╝  \r\n██║     \r\n╚═╝   ";
                string g = " ██████╗ \r\n██╔════╝ \r\n██║  ███╗\r\n██║   ██║\r\n╚██████╔╝\r\n ╚═════╝ ";
                string h = "██╗  ██╗\r\n██║  ██║\r\n███████║\r\n██╔══██║\r\n██║  ██║\r\n╚═╝  ╚═╝";
                string i = "██╗\r\n██║\r\n██║\r\n██║\r\n██║\r\n╚═╝";
                string j = "    ██╗\r\n     ██║\r\n     ██║\r\n██   ██║\r\n╚█████╔╝\r\n ╚════╝ ";
                string k = "██╗  ██╗\r\n██║ ██╔╝\r\n█████╔╝ \r\n██╔═██╗ \r\n██║  ██╗\r\n╚═╝  ╚═╝";
                string l = "██╗     \r\n██║     \r\n██║     \r\n██║     \r\n███████╗\r\n╚══════╝";
                string m = "███╗   ███╗\r\n████╗ ████║\r\n██╔████╔██║\r\n██║╚██╔╝██║\r\n██║ ╚═╝ ██║\r\n╚═╝     ╚═╝";
                string n = "███╗   ██╗\r\n████╗  ██║\r\n██╔██╗ ██║\r\n██║╚██╗██║\r\n██║ ╚████║\r\n╚═╝  ╚═══╝";
                string o = " ██████╗ \r\n██╔═══██╗\r\n██║   ██║\r\n██║   ██║\r\n╚██████╔╝\r\n ╚═════╝ ";
                string p = "██████╗ \r\n██╔══██╗\r\n██████╔╝\r\n██╔═══╝ \r\n██║     \r\n╚═╝  ";
                string q = " ██████╗ \r\n██╔═══██╗\r\n██║   ██║\r\n██║▄▄ ██║\r\n╚██████╔╝\r\n ╚══▀▀═╝ ";
                string r = "██████╗ \r\n██╔══██╗\r\n██████╔╝\r\n██╔══██╗\r\n██║  ██║\r\n╚═╝  ╚═╝";
                string s = "███████╗\r\n██╔════╝\r\n███████╗\r\n╚════██║\r\n███████║\r\n╚══════╝";
                string t = "████████╗\r\n╚══██╔══╝\r\n   ██║   \r\n   ██║   \r\n   ██║   \r\n   ╚═╝  ";
                string u = "██╗   ██╗\r\n██║   ██║\r\n██║   ██║\r\n██║   ██║\r\n╚██████╔╝\r\n ╚═════╝ ";
                string v = "██╗   ██╗\r\n██║   ██║\r\n██║   ██║\r\n╚██╗ ██╔╝\r\n ╚████╔╝ \r\n  ╚═══╝  ";
                string w = "██╗    ██╗\r\n██║    ██║\r\n██║ █╗ ██║\r\n██║███╗██║\r\n╚███╔███╔╝\r\n ╚══╝╚══╝ ";
                string x = "██╗  ██╗\r\n╚██╗██╔╝\r\n ╚███╔╝ \r\n ██╔██╗ \r\n██╔╝ ██╗\r\n╚═╝  ╚═╝";
                string y = "██╗   ██╗\r\n╚██╗ ██╔╝\r\n ╚████╔╝ \r\n  ╚██╔╝  \r\n   ██║   \r\n   ╚═╝   ";
                string z = "███████╗\r\n╚══███╔╝\r\n  ███╔╝ \r\n ███╔╝  \r\n███████╗\r\n╚══════╝";
                switch (num)
                {
                    case 1:
                        Console.WriteLine(a);
                        break;
                    case 2:
                        Console.WriteLine(b);
                        break;
                    case 3:
                        Console.WriteLine(c);
                        break;
                    case 4:
                        Console.WriteLine(d);
                        break;
                    case 5:
                        Console.WriteLine(e);
                        break;
                    case 6:
                        Console.WriteLine(f);
                        break;
                    case 7:
                        Console.WriteLine(g);
                        break;
                    case 8:
                        Console.WriteLine(h);
                        break;
                    case 9:
                        Console.WriteLine(i);
                        break;
                    case 10:
                        Console.WriteLine(j);
                        break;
                    case 11:
                        Console.WriteLine(k);
                        break;
                    case 12:
                        Console.WriteLine(l);
                        break;
                    case 13:
                        Console.WriteLine(m);
                        break;
                    case 14:
                        Console.WriteLine(n);
                        break;
                    case 15:
                        Console.WriteLine(o);
                        break;
                    case 16:
                        Console.WriteLine(p);
                        break;
                    case 17:
                        Console.WriteLine(q);
                        break;
                    case 18:
                        Console.WriteLine(r);
                        break;
                    case 19:
                        Console.WriteLine(s);
                        break;
                    case 20:
                        Console.WriteLine(t);
                        break;
                    case 21:
                        Console.WriteLine(u);
                        break;
                    case 22:
                        Console.WriteLine(v);
                        break;
                    case 23:
                        Console.WriteLine(w);
                        break;
                    case 24:
                        Console.WriteLine(x);
                        break;
                    case 25:
                        Console.WriteLine(y);
                        break;
                    case 26:
                        Console.WriteLine(z);
                        break;
                    default:
                        Console.WriteLine("Error1");
                        break;
                }
                return num = 0;
            }
        } 
        static void Main(string[] args)
        {

            Console.WriteLine("ingresa el texto");
            string texto = Console.ReadLine();
            List<int> numeros = new List<int>();

            for (int i = 0; i < texto.Length; i++)
            {
                char letra = char.ToUpper(texto[i]);

                if (letra == ' ')
                {
                    numeros.Add(0);
                }
                else if (letra >= 'A' && letra <= 'Z')
                {
                    int valor = letra - 'A' + 1;
                    numeros.Add(valor);
                }
            }

            Console.WriteLine(string.Join(", ", numeros));
            foreach (int numero in numeros)
            {
                Abecedario objeto = new Abecedario();
                objeto.a(numero);
            }

        }
    }
}
