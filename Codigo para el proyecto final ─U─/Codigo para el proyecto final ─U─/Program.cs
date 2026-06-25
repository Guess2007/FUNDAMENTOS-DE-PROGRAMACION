using System;
using System.IO;
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
    public class workers
    {
        public string name { get; set; }
        public string rol { get; set; }
        public string password { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al login...");
            List<workers> trabajadores = new List<workers>();
            Console.WriteLine("ingrese el numero de trabajadores"); int numeroDeTrabajadores = int.Parse(Console.ReadLine());
            for (int i = 0; i < numeroDeTrabajadores; i++)
            {
                trabajadores.Add(new workers());
            }
            Console.WriteLine("ingrese los datos de los trabajadores");
            for (int i = 0; i < trabajadores.Count; i++)
            {
                Console.Write("Nombre: ");
                trabajadores[i].name = Console.ReadLine();
                Console.Write("Rol: ");
                trabajadores[i].rol = Console.ReadLine();
                Console.Write("Contraseña: ");
                trabajadores[i].password = Console.ReadLine();
            }
            Console.WriteLine("Lista de trabajadores");
            for (int i = 0; i < trabajadores.Count; i++)
            {
                Console.WriteLine($"Trabajador n° {i + 1}: {trabajadores[i].name}");
                Console.WriteLine($"Rol: {trabajadores[i].rol}");
                Console.WriteLine($"Contraseña: {trabajadores[i].password}");
            }
            Console.WriteLine("Trabajadores registrados exitosamente...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Bienvenido al sistema de consulta de trabajadores...");
            Console.WriteLine("Para empezar su nombre"); string nombre = Console.ReadLine();
            Console.WriteLine("ingrese su rol \n1. Administrador\n2. Vendedor"); string rol = Console.ReadLine().ToLower();
            for (int i = 0; i < trabajadores.Count; i++)
            {
                if (trabajadores[i].name == nombre)
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
                    if (trabajadores[i].password == contraseña)
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
                        sistema_administradores(trabajadores.Count, trabajadores);
                    }
                    else if (rol == "vendedor")
                    {
                        sistema_vendedores(trabajadores);
                    }
                    else
                    {
                        Console.WriteLine("Credenciales invalidas, cerrando sistema...");
                        Console.ReadKey();
                    }
                }
                else if (trabajadores[i].name != nombre) 
                {
                    if (trabajadores[i].rol != rol)
                    {
                        Console.WriteLine("Datos invalidos, acceso denegado."); Console.ReadKey();
                    }
                }
            }
            //Si el admin nunca ingresa al sistema, ¿la lista de productos existira en caso de que el vendedor ingrese al sistema?
            //No, por lo tanto, el vendedor no podra acceder a la lista de productos, lo que es un gran problema,
            //ya que el vendedor necesita esa informacion para vender los productos.
        }
        static public void sistema_vendedores(List<workers> a)
        {
            Console.WriteLine("Bienvenido al sistema de vendedores:]");
            Console.WriteLine("Ingrese sus credenciales...");
            Console.Write("Nombre: ");
            Console.Write("Contraseña: ");
            if () { }
        }
        static public void sistema_administradores(int n, List<workers> a)
        {
            Console.Clear();
            Console.WriteLine("Bienvenido al sistema de administradores...");
            Console.WriteLine("Esta es su lista de trabajadores registrados...");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("trabajador nº " + (i + 1)  + ": " + a[i].name);
                Console.WriteLine("Cargo: " + a[i].rol);
                Console.WriteLine("contraseña: " + a[i].password);
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
                funciones_administrador(opcionElegida, a);
            }
            else if (reader == 'n')
            {
                Console.WriteLine("Cerrando el sistema, presione enter para cerrar la consola...");
                Console.ReadKey();
            }

        }
        static public void funciones_administrador(int n, List<workers> a)
        {
            string rutaArchivo;
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
                    for (int i = 0; i < numeroproductos; i++)
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
                        for (int i = 0; i < nuevolargo; i++)
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
                    int uit = 38500;
                    int igv = 18 / 100;
                    Console.WriteLine("Bienvenido al sistema de contabilizaciones");
                    Console.WriteLine("Registre sus ventas...");
                    int[] ventas = new int[4];
                    for (int i = 0; i < ventas.Length; i++)
                    {
                        Console.WriteLine($"ingreso n° {ventas[i + 1]}: ");
                        ventas[i] = int.Parse(Console.ReadLine());
                    }
                    int ventatotal = ventas.Sum();
                    double ventaneta = ventatotal / igv;
                    Console.Write($"Su venta anual es de: {ventatotal}");
                    Console.WriteLine($"Con IGV: {ventaneta}");
                    Console.WriteLine("Ingrese el coste de inventario: "); int costes = int.Parse(Console.ReadLine());
                    double ganancias = ventaneta - costes;
                    if (ganancias < uit) 
                    {
                        Console.WriteLine($"Su ganancia es de: {ganancias}");
                    }
                    else 
                    {
                        Console.WriteLine("Aplicando UIT");
                        double impuesto = (ganancias - uit) * 8/100;
                        Console.WriteLine($"El impuesto a la renta anual seria de {impuesto}");
                        Console.WriteLine($"La ganancia anual seria de {ganancias - impuesto}");
                        Console.ReadKey();
                        rutaArchivo = "ganancia.txt";
                        string ElementoAGuardar = Convert.ToString(ganancias - impuesto);
                        File.WriteAllText(rutaArchivo, ElementoAGuardar);
                    }
                    break;
                case 3:
                    Console.WriteLine("Nienvenido al sistema de salarios");
                    if (File.Exists(rutaArchivo = "ganancias.txt")) 
                    {
                        string extractor = File.ReadAllText("ganancia.txt");
                        double ganancia = Convert.ToDouble(extractor);
                        Console.WriteLine("Estimaremos la cantidad de partes en las que puede dividir sus ganancias entre salarios");
                        Console.WriteLine($"Sueldo anual equitativo: {ganancia / a.Count}");
                    }
                    else 
                    {
                        Console.WriteLine("Ingrese el monto de su ganancia: "); int ganancia = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Sueldo anual equitativo entre todos los empleados: {ganancia / a.Count}");
                    }
                    break;
                case 4:
                    Console.WriteLine("Bienvenido al sistema calificativo de desempeño");
                    
                    break;
                case 5:
                    Console.WriteLine("");
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }
    }
}
