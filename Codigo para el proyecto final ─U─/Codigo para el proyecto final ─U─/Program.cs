using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Codigo_para_el_proyecto_final__U_
{
    public class productos
    {

        public string nombre_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
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
            Console.WriteLine("ingrese su rol \n1. Administrador\n2. Vendedor"); string rol = Console.ReadLine().ToLower();
            for (int i = 0; i < n; i++)
            {
                if (trabajadores[i + 1, 0] == nombre)
                {
                    Console.WriteLine("Bienvenido, " + nombre);
                    if (rol == "administrador") 
                    {
                        Console.WriteLine("Rol: Administrador");
                    }
                    else if (rol == "vendedor")
                    {
                        Console.WriteLine("Rol: Vendedor");
                    }
                    else
                    {
                        Console.WriteLine("Rol invalido, cerrando sistema...");
                        Environment.Exit(0);
                    }
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
                else if (trabajadores[i + 1, 0] != nombre) 
                {
                    if (trabajadores[i + 1, 1] != rol)
                    {
                        Console.WriteLine("Datos invalidos, acceso denegado."); Console.ReadKey();
                    }
                }
            }
            //Si el admin nunca ingresa al sistema, ¿la lista de productos existira en caso de que el vendedor ingrese al sistema?
            //No, por lo tanto, el vendedor no podra acceder a la lista de productos, lo que es un gran problema,
            //ya que el vendedor necesita esa informacion para vender los productos.
        }
        static public void sistema_vendedores()
        {
            Console.WriteLine();
        }
        static public void sistema_administradores(int n, string[,] a)
        {
            Console.Clear();
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
                //Las dos ultimas funciones son redudantes y podria simplemente ponerlas en la tercera funcion, igualmente, podria renombrar la funcion como gestion de empleados
                //la idea sera esa, reajustar y en base a la valoracion del empleado, asignar salario, posteriormente y en base a la segunda, considerar quienes se quedan
                // y quienes se van.
                int opcionElegida = int.Parse(Console.ReadLine());
                Console.Clear();
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
                    List<productos> estante = new List<productos>();
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        estante.Add(new productos());
                    }
                    for (int i = 0;i < numeroproductos; i++)
                    {
                        Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                        estante[i].nombre_producto = Console.ReadLine();
                        Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                        estante[i].cantidad = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el precio del producto nº " + (i + 1) + ": ");
                        estante[i].precio = decimal.Parse(Console.ReadLine());
                    }
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                        Console.WriteLine("Cantidad: " + estante[i].cantidad);
                        Console.WriteLine("Precio: " + estante[i].precio);
                    }
                    Console.ReadKey();
                    Console.WriteLine("¿Desea agregar o eliminar productos? (agregar/eliminar)"); string reader = Console.ReadLine(); 
                    Console.ReadKey();
                    if (reader == "agregar")
                    {
                        Console.WriteLine("Indique la cantidad de productos a agregar..."); int nuevolargo = int.Parse(Console.ReadLine());
                        for (int i = 0;i < nuevolargo; i++)
                        {
                            estante.Add(new productos());
                        }
                        for (int i = numeroproductos; i < nuevolargo; i++)
                        {
                            Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                            estante[i].nombre_producto = Console.ReadLine();
                            Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                            estante[i].cantidad = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el precio del producto nº " + (i + 1) + ": ");
                            estante[i].precio = decimal.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Productos agregados exitosamente.");
                        Console.WriteLine("Esta es su nueva lista:");
                        for (int i = 0; i < nuevolargo; i++)
                        {
                            Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                            Console.WriteLine("Cantidad: " + estante[i].cantidad);
                            Console.WriteLine("Precio: " + estante[i].precio);
                        }
                    }
                    else if (reader == "eliminar")
                    {
                        Console.WriteLine("Indique la cantidad de productos a eliminar..."); int nuevolargo = int.Parse(Console.ReadLine());
                        for (int i = 0; i < nuevolargo; i++)
                        {
                            estante.RemoveAt(estante.Count - 1);
                        }
                        Console.WriteLine("Productos eliminados exitosamente.");
                        Console.WriteLine("Esta es su nueva lista:");
                        for (int i = 0; i < estante.Count; i++)
                        {
                            Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                            Console.WriteLine("Cantidad: " + estante[i].cantidad);
                            Console.WriteLine("Precio: " + estante[i].precio);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opcion invalida, cerrando sistema...");
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.WriteLine("");
                    Console.WriteLine();
                    break;
                case 3:
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }
    }
}
