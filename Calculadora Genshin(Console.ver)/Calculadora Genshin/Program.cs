using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_Genshin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            character character, character2, character3, character4 = new character();
            
            Console.Write("Personaje " + i + ": ");
            Console.WriteLine("ingrese el nombre del perosnaje"); string reader = Console.ReadLine();
            character1.name = reader;
            Console.WriteLine("ingrese el hp del perosnaje"); int reader1 = int.Parse(Console.ReadLine());
            character1.hp = reader1;
            Console.WriteLine("ingrese el atk del perosnaje"); reader1 = int.Parse(Console.ReadLine());
            character1.atk = reader1;
            Console.WriteLine("ingrese el def del perosnaje"); reader1 = int.Parse(Console.ReadLine());
            character1.def = reader1;
            Console.WriteLine("ingrese el dominio_elemental del perosnaje"); reader1 = int.Parse(Console.ReadLine());
            character1.dominio_elemental = reader1;
            Console.WriteLine("ingrese el res del perosnaje"); reader1 = int.Parse(Console.ReadLine());
            character1.res = reader1;
            Console.WriteLine("ingrese el tasa_crit del perosnaje"); reader = Console.ReadLine();
            character1.tasa_crit = reader1;
            Console.WriteLine("ingrese el daño_crit del perosnaje"); reader = Console.ReadLine();
            character1.daño_crit = reader1;
            Console.WriteLine("ingrese el elemen_dmg del perosnaje"); reader = Console.ReadLine();
            character1.elemen_dmg = reader1;

            Console.WriteLine($"el nombre de tu personaje es {character1.name} " +
                $"\nSu ataque es {character1.atk} \nSu hp es {character1.hp} \nSu def es {character1.def}" +
                $"\nSu dominio elemental es {character1.dominio_elemental}" +
                $"\nSu res es {character1.res} \nSu tasa critica es {character1.tasa_crit}" +
                $"\nSu daño critico es {character1.daño_crit}\nSu elemen_dmg es {character1.elemen_dmg}");
        }
        public void calcularbuffos() 
        { 
        }
    }
    public class character
    {
        public string name {  get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int dominio_elemental { get; set; }
        public int res { get; set; }
        public double tasa_crit { get; set; }
        public int daño_crit { get; set; }
        public int elemen_dmg { get; set; }
    }
}
