using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            login();
            Console.WriteLine("Bienvenido al menu principal");
        }
        public static void login()
        {
            List<Empleados> contratos = new List<Empleados>();
            for (int i = 0; i < 10; i++)
            {
                contratos.Add(new Empleados());
            }
            char []verificador;
            for (int i = 0; i < contratos.Count; i++)
            {
                Console.WriteLine("Ingrese su nombre de usuario: ");
                contratos[i].Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese su rol: ");
                contratos[i].Rol = Console.ReadLine();
                Console.WriteLine("Ingrese su contraseña: ");
                string lector = Console.ReadLine();
                verificador = lector.ToCharArray();
                foreach (char c in verificador)
                {
                    if (c < 8)
                    {
                        Console.WriteLine("La contraseña debe tener al menos 8 caracteres.");
                        i--; // Volver a pedir la contraseña
                        break;
                    }
                }
                contratos[i].Contraseña = verificador.ToString();
            }
        }
    }

    public class Empleados
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string Contraseña { get; set; }
    }
    public class productos
    {

        public string nombre_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}
