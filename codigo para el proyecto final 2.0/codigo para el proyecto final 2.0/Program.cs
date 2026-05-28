using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Codigo_para_el_proyecto_final__U_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Bienvenido al sistema de registro de trabajadores");
            Console.WriteLine("Registre el numero de trabajadores: ");
            int numeroDeUsuarios = int.Parse(Console.ReadLine());
            string[] trabajadores = new string[numeroDeUsuarios];
            Console.WriteLine("Registre el nombre de los trabajadores ");
            for (int i = 0; i < numeroDeUsuarios; i++) 
            {               
                Console.WriteLine("Trabajador nº" + (i + 1) + ": ");
                trabajadores[i] = Console.ReadLine();
            }
            Console.WriteLine("Los trabajadores registrados son: ");
            for (int i = 0; i < numeroDeUsuarios; i++) 
            {
                Console.WriteLine("Trabajador nº" + (i + 1) + ": " + trabajadores[i]);
            }
            */
            /*
            Console.WriteLine("Bienvenido al sistema de registro de trabajadores");
            Console.WriteLine("Ingrese el numero de trabajadores: "); int numeroDeUsuarios = int.Parse(Console.ReadLine());
            string[,] trabajadores = new string[numeroDeUsuarios, 3];
            Console.WriteLine("");
            for (int i = 0; i < trabajadores.Length; i++) 
            {
                for (int j = 0; j < 2; j++) 
                {
                    Console.WriteLine("ingrese el nombre del trabajador: ");
                    trabajadores[i, 0] = Console.ReadLine();
                    Console.WriteLine("ingrese el rol del trabajdor: ");
                    trabajadores[i, 1] = Console.ReadLine();
                    Console.WriteLine("ingrese el salario del trabajador: ");
                    trabajadores[i, 2] = Console.ReadLine();
                }
            }
            Console.WriteLine("su lista: ");
            for (int i = 0; i < trabajadores.Length; i++) 
            {
                for (int j = 0; j < 2; j++) 
                {
                    Console.WriteLine("Nombre: " + trabajadores[i, 0]);
                    Console.WriteLine("Rol: " + trabajadores[i, 1]);
                    Console.WriteLine("Salario: " + trabajadores[i, 2]);
                }
            }
            */
            
            string connectionString = "Data Source=localhost;Initial Catalog=TrabajadoresDB;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                Console.WriteLine("Bienvenido al sistema de registro de trabajadores");
                Console.WriteLine("Ingrese el numero de trabajadores: "); int numeroDeUsuarios = int.Parse(Console.ReadLine());
                string nombre, rol;
                decimal salario;
                for (int i = 0; i < numeroDeUsuarios; i++)
                {
                    Console.WriteLine("ingrese el nombre del trabajador: ");
                    nombre = Console.ReadLine();
                    Console.WriteLine("ingrese el rol del trabajador: ");
                    rol = Console.ReadLine();
                    Console.WriteLine("ingrese el salario del trabajador: ");
                    salario = decimal.Parse(Console.ReadLine());
                }
                connection.Open();
                Console.WriteLine("Conexión a la base de datos establecida.");
                connection.Insert("Insert into Trabajadores (Nombre, Rol, Salario) VALUES ('" + nombre + "', '" + rol + "', " + salario + ")");
                connection.Select("SELECT * FROM Trabajadores");
                // Aquí puedes agregar código para interactuar con la base de datos, como insertar, actualizar o consultar datos.
                connection.Close();
                Console.WriteLine("Conexión a la base de datos cerrada.");
            }
        }
    }
