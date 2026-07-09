using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Codigo_para_el_proyecto_final__U_
{
    // Representa un producto del almacén: nombre, stock inicial/final y precios de compra/venta
    public class productos
    {
        public string nombre_producto { get; set; }
        public int cantidad_inicio { get; set; }
        public int cantidad_final { get; set; }
        public decimal precio_venta { get; set; }
        public double precio_compra { get; set; }
    }

    // Los dos roles posibles que puede tener un trabajador dentro del sistema
    public enum Rol
    {
        Administrador,
        Empleado
    }

    // Representa a un trabajador registrado: sus credenciales y sus métricas de desempeño
    public class workers
    {
        public string name { get; set; }
        public Rol Rol { get; set; }
        public string password { get; set; }
        public int sells { get; set; }     // cantidad de ventas registradas para calcular su puntaje
        public int mistakes { get; set; }  // cantidad de errores registrados para calcular su puntaje
        public int score { get; set; }     // puntaje final = sells - mistakes
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== SISTEMA DE LOGIN ==========\n");

            List<workers> trabajadores = new List<workers>();
            // lista en memoria de todos los trabajadores que se registren en esta ejecución

            Console.Write("Ingrese el número de trabajadores: ");
            int numeroDeTrabajadores = int.Parse(Console.ReadLine());
            // se pide cuántos trabajadores se van a registrar, para saber cuántas veces repetir el registro

            // Registro de trabajadores
            for (int i = 0; i < numeroDeTrabajadores; i++)
            {
                workers trabajador = new workers();

                Console.WriteLine($"\nTrabajador #{i + 1}");

                Console.Write("Nombre: ");
                trabajador.name = Console.ReadLine();

                Rol rolIngresado;

                Console.Write("Rol (Administrador/Empleado): ");

                while (!Enum.TryParse(Console.ReadLine(), true, out rolIngresado))
                // intenta convertir el texto ingresado al enum Rol (ignorando mayúsculas/minúsculas);
                // si el texto no coincide con "Administrador" ni "Empleado", vuelve a pedirlo
                {
                    Console.WriteLine("Rol inválido.");
                    Console.Write("Ingrese nuevamente: ");
                }

                trabajador.Rol = rolIngresado;

                Console.Write("Contraseña: ");
                trabajador.password = Console.ReadLine();

                trabajadores.Add(trabajador);
            }

            // Mostrar trabajadores registrados
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"========== TRABAJADORES REGISTRADOS ==========");
            Console.ResetColor();
            // se cambia el color del texto solo para este encabezado y luego se vuelve al color por defecto


            foreach (workers trabajador in trabajadores)
            {
                // se recorre e imprime cada trabajador registrado, a modo de confirmación
                Console.WriteLine("--------------------------------");
                Console.WriteLine($"Nombre: {trabajador.name}");
                Console.WriteLine($"Rol: {trabajador.Rol}");
                Console.WriteLine($"Contraseña: {trabajador.password}");
            }
            Console.WriteLine("\nRegistro completado correctamente.");
            Console.Clear(); // limpia la consola antes de pasar a la pantalla de login

            char continuarSistema;
            do
            {
                // LOGIN
                Console.WriteLine("\n========== LOGIN ==========");

                bool acceso = false;
                workers usuarioLogueado = null;

                for (int intento = 1; intento <= 3; intento++)
                // se permiten hasta 3 intentos de login antes de negar el acceso
                {
                    Console.Write("\nUsuario: ");
                    string usuario = Console.ReadLine();

                    Console.Write("Contraseña: ");
                    string contraseña = Console.ReadLine();

                    foreach (workers trabajador in trabajadores)
                    // busca, entre todos los trabajadores registrados, uno cuyo nombre y contraseña coincidan
                    {
                        if (trabajador.name == usuario &&
                            trabajador.password == contraseña)
                        {
                            usuarioLogueado = trabajador;
                            acceso = true;
                            break; // ya se encontró coincidencia, no hace falta seguir buscando
                        }
                    }

                    if (acceso)
                        break; // si el login fue exitoso, no se consumen más intentos

                    Console.WriteLine("Usuario o contraseña incorrectos.");
                    Console.WriteLine($"Intento {intento} de 3.");
                }

                if (!acceso)
                {
                    // si se agotaron los 3 intentos sin éxito, se corta la ejecución del programa
                    Console.WriteLine("\nDemasiados intentos fallidos.");
                    Console.WriteLine("Acceso denegado.");
                    Console.ReadKey();
                    return;
                }

                // Bienvenida
                Console.WriteLine($"\nBienvenido {usuarioLogueado.name}");
                Console.WriteLine($"Rol detectado: {usuarioLogueado.Rol}");

                // Menú según el rol
                switch (usuarioLogueado.Rol)
                // según el rol del usuario logueado, se lo deriva al sistema de administrador o al de vendedores
                {
                    case Rol.Administrador:
                        Console.WriteLine("\n===== MENÚ ADMINISTRADOR =====");
                        sistema_administradores(trabajadores.Count, trabajadores);
                        break;

                    case Rol.Empleado:
                        Console.WriteLine("\n===== MENÚ EMPLEADO =====");
                        sistema_vendedores(trabajadores.Count, trabajadores);
                        break;

                    default:
                        Console.WriteLine("El rol asignado no existe.");
                        break;
                }

                //Si el admin nunca ingresa al sistema, ¿la lista de productos existira en caso de que el vendedor ingrese al sistema?
                //No, por lo tanto, el vendedor no podra acceder a la lista de productos, lo que es un gran problema,
                //ya que el vendedor necesita esa informacion para vender los productos.

                Console.Write("\n¿Desea volver a ingresar al sistema? (s/n): ");
            }
            while (char.TryParse(Console.ReadLine(), out continuarSistema) && char.ToLower(continuarSistema) == 's');

        }

        // ── Sistema de vendedores: valida credenciales otra vez y entra al menú de funciones de vendedor ──
        static public void sistema_vendedores(int n, List<workers> a)
        {
            Console.WriteLine("Bienvenido al sistema de vendedores:]");
            Console.WriteLine("Ingrese sus credenciales...");
            Console.Write("Nombre: "); string nombre = Console.ReadLine();
            Console.Write("Contraseña: "); string pw = Console.ReadLine();

            // corregido: la versión anterior recorría a todos los trabajadores y, cuando el
            // nombre NO coincidía con el trabajador actual del bucle, igual comparaba la
            // contraseña ingresada contra la de ESE trabajador (uno que no era el que estaba
            // intentando entrar). Ahora primero se busca al trabajador por nombre y recién
            // con ese registro puntual se valida la contraseña.
            workers encontrado = a.FirstOrDefault(w => w.name == nombre);

            if (encontrado == null)
            {
                Console.WriteLine("Nombre equivocado");
                Console.WriteLine("Credenciales invalidas, tenga buen dia");
                return;
            }

            if (encontrado.password == pw)
            {
                Console.WriteLine($"Bienvenido {nombre}");
                funciones_vendedores(n, a);
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta, cerrando el sistema");
                Console.WriteLine("Credenciales invalidas, tenga buen dia");
            }
        }

        // ── Menú de funciones disponibles para un vendedor ──
        static public void funciones_vendedores(int n, List<workers> a)
        {
            Console.WriteLine("¿Que funcion desea ejecutar?\n1. Ventas mensuales\n2. Rendimiento\n3. Almacen disponible");

            int reader;
            // corregido: antes se leía una vez fuera del while (esa lectura quedaba descartada
            // porque el while volvía a leer otra línea) y la condición "reader < 1 && reader > 2"
            // nunca podía cumplirse (un número no puede ser a la vez menor que 1 y mayor que 2),
            // por lo que en la práctica no validaba nada. Además el rango correcto es 1-3, ya que
            // el menú y el switch de abajo tienen 3 opciones, no 2.
            while (!int.TryParse(Console.ReadLine(), out reader) || reader < 1 || reader > 3)
            {
                Console.WriteLine("Opcion invalida, por favor ingrese una opcion valida");
            }
            string RutaArchivo, lector;
            switch (reader)
            {
                case 1:
                    // registra las ventas de los 12 meses y las guarda en un archivo de texto
                    Console.WriteLine("ventas mensuales: ");
                    double[] ventasmensuales = new double[12];
                    for (int i = 0; i < ventasmensuales.Length; i++)
                    {
                        Console.WriteLine($"Venta n° {i + 1}: ");
                        ventasmensuales[i] = double.Parse(Console.ReadLine());
                    }
                    for (int i = 0; i < ventasmensuales.Length; i++)
                    {
                        // vuelve a mostrar todas las ventas ingresadas, a modo de confirmación
                        Console.WriteLine($"Venta n° {i + 1} :" + ventasmensuales[i]);
                    }
                    // corregido: se usa "ventas.txt" (antes era "Venta.txt") para que coincida con
                    // el archivo que después lee el sistema de contabilidad del administrador.
                    RutaArchivo = "ventas.txt";
                    // corregido: Convert.ToString sobre un array no guarda los valores, sino el
                    // nombre del tipo (algo como "System.Double[]"). Ahora se guardan los 12
                    // valores separados por coma en una sola línea.
                    string ElementoAGuardar = string.Join(",", ventasmensuales);
                    File.WriteAllText(RutaArchivo, ElementoAGuardar);
                    break;

                case 2:
                    // registra ventas/errores de cada trabajador y arma un "ranking" comparándolos entre sí
                    List<workers> calificador = new List<workers>();
                    for (int i = 0; i < n; i++)
                    {
                        calificador.Add(new workers());
                    }
                    Console.WriteLine("Sistema dedicado al informe de rendimiento de los trabajadres");
                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine($"Trabajador n° {i + 1}: {a[i].name}");
                        Console.WriteLine($"ventas: ");
                        calificador[i].sells = int.Parse(Console.ReadLine());
                        Console.WriteLine($"errores: ");
                        calificador[i].mistakes = int.Parse(Console.ReadLine());
                    }
                    Console.WriteLine("Este es el ranking de empleados:");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 1; j < n; j++)
                        // compara cada trabajador contra los demás según su cantidad de ventas
                        {
                            if (calificador[i].sells < calificador[j].sells)
                            {
                                Console.WriteLine($"El trabajar numero {i} es: {calificador[i].name}");
                                Console.WriteLine($"El trabajador numero {j} es: {calificador[j].name}");
                            }
                        }
                    }
                    break;

                case 3:
                    // cierre de jornada: registra la cantidad de productos restantes en el almacén
                    Console.WriteLine("Has llegado al final de tu jornada (opcion a/opcion b)");
                    lector = Console.ReadLine();
                    Console.WriteLine("En orden de concluir con tu dia, registra la cantidad de productos restantes");

                    // corregido: antes se leía una línea al declarar "lector1" y otra más dentro
                    // de la condición del while (char.TryParse(Console.ReadLine(), ...)), consumiendo
                    // una entrada de más en cada intento. Ahora se lee una sola vez por vuelta.
                    char lector1;
                    string entradaLector;
                    Console.WriteLine("Has llegado al final de tu jornada");
                    Console.WriteLine("En orden de concluir con tu dia, registra la cantidad de productos restantes");
                    List<productos> estante = new List<productos>();
                    Console.WriteLine("Ingrese la cantidad de productos iniciales: "); int lector2 = int.Parse(Console.ReadLine());
                    for (int i = 0; i < lector2; i++)
                    {
                        estante.Add(new productos());
                    }
                    for (int i = 0; i < lector2; i++)
                    {
                        Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                        estante[i].nombre_producto = Console.ReadLine();
                        Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                        estante[i].cantidad_inicio = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el precio de venta del producto nº " + (i + 1) + ": ");
                        estante[i].precio_venta = decimal.Parse(Console.ReadLine());
                    }
                    Console.WriteLine("Ingrese la cantidad de productos restantes: ");
                    for (int i = 0; i < lector2; i++)
                    {
                        Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                        Console.WriteLine("Cantidad: "); estante[i].cantidad_final = int.Parse(Console.ReadLine());
                        Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        int cantidad_vendida = estante[i].cantidad_inicio - estante[i].cantidad_final;
                        int venta_producto = cantidad_vendida * (int)estante[i].precio_venta;
                        Console.WriteLine($"Cantidad vendida del producto {estante[i].nombre_producto}: {cantidad_vendida}");
                        int venta_total = estante.Sum(p => (p.cantidad_inicio - p.cantidad_final) * (int)p.precio_venta);
                    }
                    Console.WriteLine($"La venta total de todos los productos es: {estante.Sum(p => (p.cantidad_inicio - p.cantidad_final) * (int)p.precio_venta)}");
                    break;
            }
        }

        // ── Menú principal del administrador: lista trabajadores y permite entrar a funciones extra ──
        static public void sistema_administradores(int n, List<workers> a)
        {
            Console.Clear();
            Console.WriteLine("Bienvenido al sistema de administradores...");
            Console.WriteLine("Esta es su lista de trabajadores registrados...");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("trabajador nº " + (i + 1) + ": " + a[i].name);
                Console.WriteLine("Cargo: " + a[i].Rol);
                Console.WriteLine("contraseña: " + a[i].password);
            }

            string entrada;
            char reader = '\0';
            do
            {
                Console.Write("¿Desea ejecutar alguna acción extra? (s/n): ");
                entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Debe ingresar una opción.");
                    continue; // vuelve a pedir la opción sin evaluar el resto del cuerpo del bucle
                }

                reader = char.ToLower(entrada[0]);
                // se toma solo el primer carácter de la respuesta, en minúscula

                // corregido: antes, aunque "reader" ya fuera 's' o 'n' válido, esta misma condición
                // volvía a leer otra línea de consola (char.TryParse(Console.ReadLine(), out reader)),
                // consumiendo una entrada de más y pudiendo pisar un valor ya válido con basura.
                if (reader != 's' && reader != 'n')
                {
                    Console.WriteLine("Opción inválida.");
                }

            } while (reader != 's' && reader != 'n');
            if (reader == 's')
            {
                Console.Clear();
                Console.WriteLine("¿Que operacion desea ejecutar?");
                Console.WriteLine("1. Gestionar almacen. \n2. Ajuste de recibos. \n3. Asignacion de salarios. \n4. Renovacion de contratos.");
                //Las dos últimas funciones son redundantes y podria simplemente ponerlas en la tercera funcion, igualmente, podria renombrar la funcion como gestion de empleados
                //la idea sera esa, reajustar y en base a la valoracion del empleado, asignar salario, posteriormente y en base a la segunda, considerar quienes se quedan
                // y quienes se van.
                int opcionElegida;
                // corregido: mismo patrón que en funciones_vendedores (doble lectura + condición
                // "< 1 && > 4" que nunca puede ser verdadera). Ahora se usa "||" y una sola lectura
                // de consola por intento.
                while (!int.TryParse(Console.ReadLine(), out opcionElegida) || opcionElegida < 1 || opcionElegida > 4)
                {
                    Console.WriteLine("Ingrese una opcion valida y dentro de los parametros");
                }
                Console.Clear();
                funciones_administrador(opcionElegida, a);
            }
            else if (reader == 'n')
            {
                Console.Clear();
                Console.WriteLine("Cerrando el sistema, presione enter para cerrar la consola...");
                Console.ReadKey();
            }
        }

        // ── Funciones exclusivas del administrador: almacén, contabilidad, salarios y desempeño ──
        static public void funciones_administrador(int n, List<workers> a)
        {
            string rutaArchivo;
            string extractor;
            switch (n)
            {
                case 1:
                    // gestión de almacén: carga inicial de productos, muestra el listado y permite agregar/eliminar
                    Console.WriteLine("Indicar la cantidad de productos a su disposicion: ");
                    int numeroproductos = int.Parse(Console.ReadLine());
                    List<productos> estante = new List<productos>();
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        estante.Add(new productos());
                    }
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        // carga los datos de cada producto: nombre, cantidad, precio de venta y de compra
                        Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                        estante[i].nombre_producto = Console.ReadLine();
                        // corregido: la validación original intentaba hacer int.TryParse sobre
                        // "nombre_producto", que es de tipo string, así que no llegaba a compilar.
                        // Ahora solo se valida que el nombre no quede vacío.
                        while (string.IsNullOrWhiteSpace(estante[i].nombre_producto))
                        {
                            Console.WriteLine("El nombre del producto no puede estar vacío. Por favor, ingrese un nombre válido.");
                            estante[i].nombre_producto = Console.ReadLine();
                        }
                        Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                        estante[i].cantidad_inicio = int.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el precio de venta del producto nº " + (i + 1) + ": ");
                        estante[i].precio_venta = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Ingrese el precio de compra del producto nº " + (i + 1) + ": ");
                        estante[i].precio_compra = double.Parse(Console.ReadLine());
                    }
                    for (int i = 0; i < numeroproductos; i++)
                    {
                        // muestra el listado completo de productos cargados, a modo de confirmación
                        Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                        Console.WriteLine("Cantidad: " + estante[i].cantidad_inicio);
                        Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        Console.WriteLine("Precio de compra: " + estante[i].precio_compra);
                    }
                    Console.ReadKey();
                    Console.WriteLine("¿Desea agregar o eliminar productos? (agregar/eliminar)"); string reader = Console.ReadLine();
                    Console.ReadKey();
                    if (reader == "agregar")
                    {
                        // agrega N productos nuevos al final de la lista existente
                        Console.WriteLine("Indique la cantidad de productos a agregar..."); int nuevolargo = int.Parse(Console.ReadLine());
                        for (int i = 0; i < nuevolargo; i++)
                        {
                            estante.Add(new productos());
                        }
                        for (int i = numeroproductos; i < numeroproductos + nuevolargo; i++)
                        {
                            Console.WriteLine("Ingrese el nombre del producto nº " + (i + 1) + ": ");
                            estante[i].nombre_producto = Console.ReadLine();
                            Console.WriteLine("Ingrese la cantidad del producto nº " + (i + 1) + ": ");
                            estante[i].cantidad_inicio = int.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el precio de compra del producto nº " + (i + 1) + ": ");
                            estante[i].precio_compra = double.Parse(Console.ReadLine());
                            Console.WriteLine("Ingrese el precio de venta del producto nº " + (i + 1) + ": ");
                            estante[i].precio_venta = decimal.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("Productos agregados exitosamente.");
                        Console.WriteLine("Esta es su nueva lista:");
                        // corregido: antes este bucle iba de 0 a "nuevolargo", por lo que volvía a
                        // mostrar los primeros productos en vez de los recién agregados. Ahora
                        // recorre desde "numeroproductos" (donde empiezan los nuevos) hasta el
                        // final de la lista.
                        for (int i = numeroproductos; i < estante.Count; i++)
                        {
                            Console.WriteLine("Producto nº " + (i + 1) + ": " + estante[i].nombre_producto);
                            Console.WriteLine("Cantidad: " + estante[i].cantidad_inicio);
                            Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        }
                        rutaArchivo = "productos.txt";
                        File.WriteAllLines(rutaArchivo, estante.Select(p => $"{p.nombre_producto},{p.cantidad_inicio},{p.precio_venta},{p.precio_compra}"));
                        // guarda la lista completa de productos en un archivo de texto, una línea por producto (formato CSV)
                    }
                    else if (reader == "eliminar")
                    {
                        // elimina N productos desde el final de la lista
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
                            Console.WriteLine("Cantidad: " + estante[i].cantidad_inicio);
                            Console.WriteLine("Precio de venta: " + estante[i].precio_venta);
                        }
                        rutaArchivo = "productos.txt";
                        File.WriteAllLines(rutaArchivo, estante.Select(p => $"{p.nombre_producto},{p.cantidad_inicio},{p.precio_venta},{p.precio_compra}"));
                    }
                    else
                    {
                        Console.WriteLine("Opcion invalida, cerrando sistema...");
                        Console.ReadKey();
                    }
                    break;

                case 2:
                    // contabilidad: calcula el IGV y el impuesto a la renta sobre las ventas registradas
                    int uit = 38500; // valor de referencia de la UIT (Unidad Impositiva Tributaria)
                    // corregido: "18 / 100" es una división entre enteros y da 0 (no 0.18), lo que
                    // hacía que "ventaneta = ventatotal / igv" fuera una división por cero. Ahora se
                    // usa división de punto flotante (18.0 / 100 = 0.18).
                    double igv = 18.0 / 100;
                    Console.WriteLine("Bienvenido al sistema de contabilizaciones");
                    if (File.Exists(rutaArchivo = "ventas.txt"))
                    // se asigna "ventas.txt" a rutaArchivo y a la vez se comprueba si ese archivo existe
                    {
                        string recuperador = File.ReadAllText("ventas.txt");
                        // corregido: antes se recorría el texto caracter por caracter (recuperador.Length),
                        // mostrando un "caracter" por cada supuesta "venta". Como en funciones_vendedores
                        // ahora se guardan los valores separados por coma, aquí se separan de la misma forma.
                        string[] valoresVentas = recuperador.Split(',');
                        for (int i = 0; i < valoresVentas.Length; i++)
                        {
                            Console.WriteLine($"Venta n° {i + 1} :" + valoresVentas[i]);
                        }
                        int ventastotales = valoresVentas.Sum(); // aunque se calcula la suma, no se guarda en ninguna variable ni se usa después
                        double ventaneta = ventastotales / igv;
                        Console.Write($"Su venta anual es de: {ventastotales}");
                        Console.WriteLine($"Con IGV: {ventaneta}");
                        Console.WriteLine("Ingrese el coste de inventario: "); int costes = int.Parse(Console.ReadLine());
                        double ganancias = ventaneta - costes;
                        if (ganancias < uit)
                        {
                            Console.WriteLine($"Su ganancia es de: {ganancias}");
                        }
                        else
                        {
                            // si la ganancia supera la UIT, se calcula el impuesto a la renta sobre el excedente
                            Console.WriteLine("Aplicando UIT");
                            double impuesto = (ganancias - uit) * 8 / 100;
                            Console.WriteLine($"El impuesto a la renta anual seria de {impuesto}");
                            Console.WriteLine($"La ganancia anual seria de {ganancias - impuesto}");
                            Console.ReadKey();
                            rutaArchivo = "ganancia.txt";
                            string ElementoAGuardar = Convert.ToString(ganancias - impuesto);
                            File.WriteAllText(rutaArchivo, ElementoAGuardar);
                            // guarda la ganancia neta (ya descontado el impuesto) para usarla luego en salarios
                        }
                    }
                    else
                    {
                        // si no hay ventas registradas todavía, se piden 4 ingresos de venta y se calculan ganancias
                        Console.WriteLine("Registre sus ventas...");
                        int[] ventas = new int[4];
                        for (int i = 0; i < ventas.Length; i++)
                        {
                            // corregido: el mensaje mostraba "ventas[i + 1]", que en la última vuelta
                            // (i = 3) queda fuera de rango del array y además todavía no tenía ningún
                            // valor cargado. Ahora se muestra simplemente el número de ingreso (i + 1).
                            Console.WriteLine($"ingreso n° {i + 1}: ");
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
                            // si la ganancia supera la UIT, se calcula el impuesto a la renta sobre el excedente
                            Console.WriteLine("Aplicando UIT");
                            double impuesto = (ganancias - uit) * 8 / 100;
                            Console.WriteLine($"El impuesto a la renta anual seria de {impuesto}");
                            Console.WriteLine($"La ganancia anual seria de {ganancias - impuesto}");
                            Console.ReadKey();
                            rutaArchivo = "ganancia.txt";
                            string ElementoAGuardar = Convert.ToString(ganancias - impuesto);
                            File.WriteAllText(rutaArchivo, ElementoAGuardar);
                            // guarda la ganancia neta (ya descontado el impuesto) para usarla luego en salarios
                        }
                    }
                    break;
                case 3:
                    // asignación de salarios: reparte la ganancia registrada en partes iguales entre los trabajadores
                    Console.WriteLine("Nienvenido al sistema de salarios");
                    // corregido: antes se comprobaba la existencia de "ganancias.txt" (con "s"), pero
                    // el archivo que se genera en el caso 2 se llama "ganancia.txt" (sin "s"). Ahora
                    // ambos nombres coinciden.
                    if (File.Exists(rutaArchivo = "ganancia.txt"))
                    {
                        extractor = File.ReadAllText("ganancia.txt");
                        double ganancia = Convert.ToDouble(extractor);
                        Console.WriteLine("Estimaremos la cantidad de partes en las que puede dividir sus ganancias entre salarios");
                        Console.WriteLine($"Sueldo anual equitativo: {ganancia / a.Count}");
                    }
                    else
                    {
                        // si no hay un archivo de ganancias previo, se pide el monto manualmente
                        Console.WriteLine("Ingrese el monto de su ganancia: "); int ganancia = int.Parse(Console.ReadLine());
                        Console.WriteLine($"Sueldo anual equitativo entre todos los empleados: {ganancia / a.Count}");
                    }
                    break;

                case 4:
                    // evaluación de desempeño: calcula el puntaje de cada trabajador (ventas - errores) y arma un ranking
                    Console.WriteLine("Bienvenido al sistema calificativo de desempeño");
                        Console.WriteLine("Ingrese el desempeño de cada trabajador:");
                        for (int i = 0; i < a.Count; i++)
                        {
                            Console.WriteLine($"Desempeño de {a[i].name}: ");
                            a[i].sells = int.Parse(Console.ReadLine());
                            Console.WriteLine($"Errores de {a[i].name}: ");
                            a[i].mistakes = int.Parse(Console.ReadLine());
                        }
                        for (int i = 0; i < a.Count; i++)
                        {
                            // calcula y muestra el puntaje de cada trabajador (ventas menos errores)
                            Console.WriteLine($"Trabajador: {a[i].name}\nVentas: {a[i].sells} \nErrores: {a[i].mistakes}");
                            int puntaje = a[i].sells - a[i].mistakes;
                            Console.WriteLine($"Puntaje de {a[i].name}: {puntaje}");
                            a[i].score = puntaje;
                        }
                        // calcular puntajes para cada trabajador
                        // ordenar por puntaje descendente e imprimir ranking
                        var puntajesOrdenados = a.OrderByDescending(s => s.score).ToList();
                        for (int j = 0; j < puntajesOrdenados.Count; j++)
                        {
                            Console.WriteLine($"================Trabajador {(j + 1)}================" +
                                $"\nNombre: {puntajesOrdenados[j].name}\nPuntaje: {puntajesOrdenados[j].score}");
                        }

                        // obtener puntaje mínimo y trabajador con peor desempeño (si necesitas ese dato)
                        int minimo = a.Min(w => w.score);
                        var peor = a.FirstOrDefault(w => w.score == minimo);
                        if (peor != null)
                        {
                            // ofrece la opción de eliminar de la planilla al trabajador con peor desempeño
                            Console.WriteLine($"Empleado con peor desempeño: {peor.name} con puntaje {peor.score}");
                            Console.WriteLine("¿Desea eliminarlo de la planilla de trabajadores? (s/n)");
                            respuesta = Console.ReadLine();
                            while (respuesta.ToLower() != "s" && respuesta.ToLower() != "n")
                            {
                                Console.WriteLine("Respuesta inválida. Ingrese 's' para sí o 'n' para no.");
                                respuesta = Console.ReadLine();
                            }
                            if (respuesta.ToLower() == "s")
                            {
                                a.Remove(peor);
                                Console.WriteLine($"Empleado {peor.name} eliminado de la planilla.");
                            }
                            else
                            {
                                Console.WriteLine("No se eliminó ningún empleado.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Respuesta inválida.");
                    }
                    break;

                default:
                    Console.WriteLine();
                    break;
            }
        }
    }
}
