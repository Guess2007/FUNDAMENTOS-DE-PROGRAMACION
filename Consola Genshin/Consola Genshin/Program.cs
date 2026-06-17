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
            List<character> personajes = new List<character>();
            int max_characters = 1;

            for (int i = 0; i < max_characters; i++)
            {
                personajes.Add(new character());
            }
            for (int i = 0; i < max_characters; i++)
            {
                Console.Write($"Personaje {i + 1}: {personajes[i].name}"); string reader = Console.ReadLine();
                personajes[i].name = reader;
                Console.Write($"HP: "); reader = Console.ReadLine();
                personajes[i].hp = int.Parse(reader);
                Console.Write($"ATK: "); reader = Console.ReadLine();
                personajes[i].atk = int.Parse(reader);
                Console.Write($"DEF: "); reader = Console.ReadLine();
                personajes[i].def = int.Parse(reader);
                Console.Write($"Dominio Elemental: "); reader = Console.ReadLine();
                personajes[i].dominio_elemental = int.Parse(reader);
                Console.Write($"Tasa de Crítico: "); reader = Console.ReadLine();
                personajes[i].tasa_crit = double.Parse(reader);
                Console.Write($"Daño Crítico: "); reader = Console.ReadLine();
                personajes[i].daño_crit = int.Parse(reader);
                Console.Write($"Daño Elemental: "); reader = Console.ReadLine();
                personajes[i].elemen_dmg = int.Parse(reader);
            }
            for (int i = 0; i < max_characters; i++) 
            {
                Console.WriteLine($"Personaje {i + 1}: {personajes[i].name}");
                Console.WriteLine($"Hp {i + 1}: {personajes[i].hp}");
                Console.WriteLine($"Atk {i + 1}: {personajes[i].atk}");
                Console.WriteLine($"Def {i + 1}: {personajes[i].def}");
                Console.WriteLine($"Dominio Elemental {i + 1}: {personajes[i].dominio_elemental}");
                Console.WriteLine($"Tasa de Crítico {i + 1}: {personajes[i].tasa_crit}");
                Console.WriteLine($"Daño Crítico {i + 1}: {personajes[i].daño_crit}");
                Console.WriteLine($"Daño Elemental {i + 1}: {personajes[i].elemen_dmg}");
            }
        }
        public void calcularbuffos()
        {
        }
    }
    public class character
    {
        public string name { get; set; }
        public int hp { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int dominio_elemental { get; set; }
        public int res { get; set; }
        public double tasa_crit { get; set; }
        public int daño_crit { get; set; }
        public int elemen_dmg { get; set; }
    }
    public class buffos
    {
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
