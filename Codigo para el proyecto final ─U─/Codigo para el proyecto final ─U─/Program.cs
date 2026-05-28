using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            trabajadores[0, 1] = "Cargo";
            trabajadores[0, 2] = "contraseña";
            trabajadores[0, 3] = "salario";
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Ingrese el " + trabajadores[0, 0] + " del trabajador nº " + (i+1) + ": ");
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
            Console.ReadKey();
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
                Console.WriteLine("trabajador nº " + (i + 1)  + ": " + a[i + 1, 0]);
                Console.WriteLine("Cargo: " + a[i + 1, 1]);
                Console.WriteLine("contraseña: " + a[i + 1, 2]);
            }
            Console.WriteLine("¿Desea ejecutar alguna accion extra? (s/n)"); char reader = char.Parse(Console.ReadLine());
            if (reader == 's')
            {
                Console.WriteLine("¿Que operacion desea ejecutar?");
                Console.WriteLine("1. Gestionar almacen. \n2. Ajuste de recibos. \n3. Asignacion de salarios. \n4. Vendedor del mes. \n5. Renovacion de contratos.");
                int opcionElegida = int.Parse(Console.ReadLine());
                funciones_administrador(opcionElegida);
            }
            else if (reader == 'n')
            {
                Console.WriteLine("Cerrando el sistema, presione enter para cerrar la consola...");
                Console.ReadKey();
            }

        }
        static public void funciones_administrador(int n)
        {
            switch (n)
            {
                case 1:
                    Console.WriteLine("Indicar la cantidad de productos a su disposicion: ");
                    int numeroproductos = int.Parse(Console.ReadLine());
                    string[, ] estantes = new string [numeroproductos + 1,4];
                    estantes[0, 0] = "producto";
                    estantes[0, 1] = "tipo";
                    estantes[0, 2] = "precio";
                    estantes[0, 3] = "cantidad";
                    Console.WriteLine("Indica los productos que dispondran tus estantes: ");
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        Console.Write("Nombre del producto n° " + (i + 1) + ": "); ;
                        estantes[i + 1, 0] = Console.ReadLine();
                        Console.Write("Tipo de producto n° " + (i + 1) + ": ");
                        estantes[i + 1, 1] = Console.ReadLine();
                        Console.Write("Precio del producto n° " + (i + 1) + ": ");                       
                        estantes[i + 1, 2] = Convert.ToString(Console.ReadLine());
                        Console.Write("Cantidad del producto n° " + (i + 1) + ": ");
                        estantes[i + 1, 3] = Console.ReadLine();
                    }
                    Console.WriteLine("Esta es su lista de productos actual: ");
                    for (int i = 1; i < numeroproductos + 1; i++)
                    {
                        Console.WriteLine($"Producto n° {i}: {estantes[i, 0]}\nTipo: {estantes[i, 1]}\nPrecio: {estantes[i, 2]}\nCantidad: {estantes[i, 3]}");
                    }
                    Console.WriteLine("Lista terminada...");
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine();
                    break;
                case 3:
                    Console.WriteLine();
                    break;
                case 4:
                    Console.WriteLine();
                    break;
                case 5:
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }

    }
}
