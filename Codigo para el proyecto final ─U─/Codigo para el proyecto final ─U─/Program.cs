using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Codigo_para_el_proyecto_final__U_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al login...");
            Console.WriteLine("Para empezar, registre el numero de trabajadores: "); int n = int.Parse(Console.ReadLine());
            string[,] trabajadores = new string[n + 1, 4];
            trabajadores[0, 0] = "Nombre";
            trabajadores[0, 2] = "contraseña";
            trabajadores[0, 1] = "Cargo";
            trabajadores[0, 3] = "salario";
            for (int i = 0; i < n; i++) 
            {
                Console.WriteLine("Ingrese el " + trabajadores[0, 0] + " del trabajador nº " + (i + 1) + ": ");
                trabajadores[i + 1, 0] = Console.ReadLine();
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Ingrese el " + trabajadores[0, 1] + " del trabajador nº " + (i + 1) + ": ");
                trabajadores[i + 1, 1] = Console.ReadLine();
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Ingrese el " + trabajadores[0, 2] + " del trabajador nº " + (i + 1) + ": ");
                trabajadores[i + 1, 2] = Convert.ToString(Console.ReadLine());
            }
            Console.WriteLine("Lista actual de registros...");
            
            for (int i = 0; i < n; i++) 
            {
                Console.WriteLine("trabajador nº " + (i + 1) + ": " + trabajadores[i + 1, 0]);
                Console.WriteLine("Cargo: " + trabajadores[i + 1, 1]);
                Console.WriteLine("Salario: " + trabajadores[i + 1, 2]); 
            }
            Console.WriteLine("Trabajadores registrados exitosamente...");
            Console.Clear();
            Console.WriteLine("Bienvenido al sistema de consulta de trabajadores...");
            Console.WriteLine("Para empezar su nombre"); string nombre = Console.ReadLine();
            Console.WriteLine("ingrese su rol"); string rol = Console.ReadLine();
            for (int i = 0; i < n; i++) 
            {
                if (trabajadores[i + 1, 0] == nombre) 
                {
                    Console.WriteLine("Bienvenido, " + nombre);
                    Console.WriteLine("Cargo: " + rol);
                    Console.WriteLine("Para proceder con el sistema, por favor, ingrese su contraseña: ");
                    Console.WriteLine("ingrese su contraseña"); string contraseña = Console.ReadLine();
                    if (trabajadores[i + 1, 2] == contraseña)
                    {
                        Console.WriteLine("Contraseña correcta, acceso concedido.");
                    }
                    else
                    {
                        Console.WriteLine("Contraseña incorrecta, acceso denegado.");
                        Environment.Exit(0);
                    }
                    if (rol == "administrador")
                    {
                        sistema_administradores(n, trabajadores);
                    }
                    else if (rol == "vendedor")
                    {
                        sistema_vendedores();
                    }
                    else
                    {
                        Console.WriteLine("Credenciales invalidas, cerrando sistema...");
                        Console.ReadKey();
                    }
                }
            }
            
        }
        static public void sistema_vendedores() 
        { 
        }
        static public void sistema_administradores(int n, string[,] a)
        {
            
            Console.WriteLine("Bienvenido al sistema de administradores...");
            Console.WriteLine("Esta es su lista de trabajadores registrados...");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("trabajador nº " + (i + 1) + ": " + a[i + 1, 0]);
                Console.WriteLine("Cargo: " + a[i + 1, 1]);
                Console.WriteLine("Salario: " + a[i + 1, 2]);
            }
        } 

    }
}
