using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public enum Element
    {
        Physical,
        Pyro,
        Hydro,
        Electro,
        Anemo,
        Cryo,
        Geo,
        Dendro
    }

    public enum ReactionType
    {
        None,
        Burning,
        Superconduct,
        Swirl,
        ElectroCharged,
        Shattered,
        Overloaded,
        Bloom,
        Hyperbloom,
        Burgeon
    }

    public class CharacterStats
    {
        public double BaseHP { get; set; }
        public double BaseATK { get; set; }
        public double BaseDEF { get; set; }
        public double CritRate { get; set; }
        public double CritDMG { get; set; }
        public double EnergyRecharge { get; set; }
        public double ElementalMastery { get; set; }
        public double ElementalDMGBonus { get; set; }
        public double PhysicalDMGBonus { get; set; }
        public Element CharacterElement { get; set; }
    }

    public class Skill
    {
        public string Name { get; set; }
        public double MotionValue { get; set; }
        public Element DamageElement { get; set; }
        public double HPScaling { get; set; }
        public double DEFScaling { get; set; }
        public int Hits { get; set; }
        public ReactionType Reaction { get; set; }

        public Skill()
        {
            Name = "";
            HPScaling = 0;
            DEFScaling = 0;
            Hits = 1;
            Reaction = ReactionType.None;
        }
    }

    public class DamageResult
    {
        public string SkillName { get; set; }
        public double BaseDamage { get; set; }
        public double CritDamage { get; set; }
        public double AvgDamage { get; set; }
        public double ReactionBonus { get; set; }
        public double TotalAvgDamage { get; set; }
        public int Hits { get; set; }
        public ReactionType Reaction { get; set; }

        public DamageResult() { SkillName = ""; }
    }

    public class Character
    {
        public string Name { get; set; }
        public CharacterStats Stats { get; set; }
        public List<Skill> Skills { get; set; }

        public Character()
        {
            Name = "";
            Stats = new CharacterStats();
            Skills = new List<Skill>();
        }

        public List<DamageResult> CalculateRotationDamage(int enemyLevel = 90, double enemyResistance = 0.10)
        {
            List<DamageResult> results = new List<DamageResult>();
            foreach (Skill skill in Skills)
                results.Add(CalculateSkillDamage(skill, enemyLevel, enemyResistance));
            return results;
        }

        private DamageResult CalculateSkillDamage(Skill skill, int enemyLevel, double enemyResistance)
        {
            double scalingBase = Stats.BaseATK * skill.MotionValue
                               + Stats.BaseHP * skill.HPScaling
                               + Stats.BaseDEF * skill.DEFScaling;

            double dmgBonus = skill.DamageElement == Element.Physical
                ? Stats.PhysicalDMGBonus
                : Stats.ElementalDMGBonus;

            double characterLevel = 90;
            double defMultiplier = (characterLevel + 100)
                                 / ((characterLevel + 100) + (enemyLevel + 100));

            double resMultiplier;
            if (enemyResistance < 0)
                resMultiplier = 1.0 - enemyResistance / 2.0;
            else if (enemyResistance < 0.75)
                resMultiplier = 1.0 - enemyResistance;
            else
                resMultiplier = 1.0 / (4.0 * enemyResistance + 1.0);

            double baseDamage = scalingBase * (1 + dmgBonus) * defMultiplier * resMultiplier;
            double critDamage = baseDamage * (1 + Stats.CritDMG);

            double cr = Stats.CritRate < 0 ? 0 : (Stats.CritRate > 1 ? 1 : Stats.CritRate);
            double avgDamage = baseDamage * (1 + cr * Stats.CritDMG);

            double reactionBonus = CalculateReactionBonus(skill.Reaction, Stats.ElementalMastery);
            double totalAvg = (avgDamage + reactionBonus) * skill.Hits;

            return new DamageResult
            {
                SkillName = skill.Name,
                BaseDamage = baseDamage * skill.Hits,
                CritDamage = critDamage * skill.Hits,
                AvgDamage = avgDamage * skill.Hits,
                ReactionBonus = reactionBonus * skill.Hits,
                TotalAvgDamage = totalAvg,
                Hits = skill.Hits,
                Reaction = skill.Reaction
            };
        }

        private static double CalculateReactionBonus(ReactionType reaction, double em)
        {
            if (reaction == ReactionType.None) return 0;

            double reactionMult = 0;
            switch (reaction)
            {
                case ReactionType.Burning: reactionMult = 0.25; break;
                case ReactionType.Superconduct: reactionMult = 0.5; break;
                case ReactionType.Swirl: reactionMult = 0.6; break;
                case ReactionType.ElectroCharged: reactionMult = 1.2; break;
                case ReactionType.Shattered: reactionMult = 3.0; break;
                case ReactionType.Overloaded:
                case ReactionType.Bloom: reactionMult = 4.0; break;
                case ReactionType.Hyperbloom:
                case ReactionType.Burgeon: reactionMult = 6.0; break;
            }

            double reactionBaseDmg = 1446.85;
            double emBonus = 16.0 * em / (em + 2000.0);
            return reactionMult * reactionBaseDmg * (1 + emBonus);
        }
    }

    public class Rotation
    {
        public string Name { get; set; }
        public List<Character> Characters { get; set; }

        public Rotation()
        {
            Name = "Mi Rotación";
            Characters = new List<Character>();
        }

        public void Calculate(int enemyLevel = 90, double enemyResistance = 0.10)
        {
            Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
            Console.WriteLine("  🌟 RESULTADO DE LA ROTACIÓN: " + Name);
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine(string.Format("  Nivel enemigo: {0}  |  Resistencia: {1:F0}%", enemyLevel, enemyResistance * 100));
            Console.WriteLine();

            double totalRotationDmg = 0;
            int order = 1;

            foreach (Character character in Characters)
            {
                Console.WriteLine(string.Format("  [{0}] ✦ {1} ({2})", order, character.Name.ToUpper(), character.Stats.CharacterElement));
                Console.WriteLine(string.Format("      ATK: {0:F0} | HP: {1:F0} | CRIT: {2:F1}% / {3:F1}%",
                    character.Stats.BaseATK, character.Stats.BaseHP,
                    character.Stats.CritRate * 100, character.Stats.CritDMG * 100));
                Console.WriteLine(string.Format("      Bonif. Elem: {0:F1}% | EM: {1:F0} | ER: {2:F0}%",
                    character.Stats.ElementalDMGBonus * 100,
                    character.Stats.ElementalMastery,
                    character.Stats.EnergyRecharge * 100));
                Console.WriteLine();

                List<DamageResult> results = character.CalculateRotationDamage(enemyLevel, enemyResistance);
                double charTotal = 0;

                foreach (DamageResult r in results)
                {
                    string reactionStr = r.Reaction != ReactionType.None
                        ? " ⚡ " + r.Reaction.ToString() : "";

                    Console.WriteLine(string.Format("      ┌─ {0}{1}", r.SkillName, reactionStr));
                    Console.WriteLine(string.Format("      │  Hits: {0}  |  Normal: {1:F0}  |  Crit: {2:F0}", r.Hits, r.BaseDamage, r.CritDamage));
                    if (r.Reaction != ReactionType.None)
                        Console.WriteLine(string.Format("      │  Bono Reacción: +{0:F0}", r.ReactionBonus));
                    Console.WriteLine(string.Format("      └─ ➤ Daño Promedio: {0:F0}", r.TotalAvgDamage));
                    Console.WriteLine();
                    charTotal += r.TotalAvgDamage;
                }

                Console.WriteLine(string.Format("      ══ Total {0}: {1:F0}", character.Name, charTotal));
                Console.WriteLine();
                totalRotationDmg += charTotal;
                order++;
            }

            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine(string.Format("  💥 DAÑO TOTAL DE LA ROTACIÓN: {0:F0}", totalRotationDmg));
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
        }
    }

   

    static class ConsoleHelper
    {
        public static double ReadDouble(string prompt, double defaultValue = 0)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            double value;
            if (string.IsNullOrWhiteSpace(input))
                return defaultValue;
            while (!double.TryParse(input.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out value))
            {
                Console.Write("  Valor inválido. Intenta de nuevo: ");
                input = Console.ReadLine();
            }
            return value;
        }

        public static int ReadInt(string prompt, int defaultValue = 1)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            int value;
            if (string.IsNullOrWhiteSpace(input))
                return defaultValue;
            while (!int.TryParse(input, out value) || value < 1)
            {
                Console.Write("  Valor inválido (debe ser un número entero ≥ 1): ");
                input = Console.ReadLine();
            }
            return value;
        }

        public static T ReadEnum<T>(string prompt) where T : struct
        {
            string[] names = Enum.GetNames(typeof(T));
            Console.WriteLine(prompt);
            for (int i = 0; i < names.Length; i++)
                Console.WriteLine(string.Format("    {0}. {1}", i, names[i]));
            Console.Write("  Ingresa el número: ");

            string input = Console.ReadLine();
            int index;
            while (!int.TryParse(input, out index) || index < 0 || index >= names.Length)
            {
                Console.Write("  Opción inválida. Ingresa un número entre 0 y " + (names.Length - 1) + ": ");
                input = Console.ReadLine();
            }
            return (T)Enum.Parse(typeof(T), names[index]);
        }

        public static void Separator(char c = '─', int width = 63)
        {
            Console.WriteLine(new string(c, width));
        }
    }

    // ─── Lector de personajes ────────────────────────────────────────────────

    static class CharacterInput
    {
        public static Character ReadCharacter(int index)
        {
            ConsoleHelper.Separator('─');
            Console.WriteLine(string.Format("  PERSONAJE {0}", index));
            ConsoleHelper.Separator('─');

            Console.Write("  Nombre del personaje: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name)) name = "Personaje " + index;

            Console.WriteLine("\n  — Estadísticas Base —");
            double hp = ConsoleHelper.ReadDouble("  HP Máximo         : ");
            double atk = ConsoleHelper.ReadDouble("  ATK Base          : ");
            double def = ConsoleHelper.ReadDouble("  DEF Base          : ");

            Console.WriteLine("\n  — Estadísticas Ofensivas —");
            double critRate = ConsoleHelper.ReadDouble("  Prob. de Crítico  (ej: 65 para 65%): ");
            double critDmg = ConsoleHelper.ReadDouble("  Daño Crítico      (ej: 140 para +140%): ");
            double er = ConsoleHelper.ReadDouble("  Recarga de Energía (ej: 130 para 130%): ", 100);
            double em = ConsoleHelper.ReadDouble("  Maestría Elemental: ");

            Console.WriteLine("\n  — Bonificaciones de Daño —");
            double elemBonus = ConsoleHelper.ReadDouble("  Bonif. Daño Elemental (ej: 46.6 para 46.6%): ");
            double physBonus = ConsoleHelper.ReadDouble("  Bonif. Daño Físico    (ej: 0 si no aplica): ");

            Element element = ConsoleHelper.ReadEnum<Element>("\n  — Elemento del personaje —");

            CharacterStats stats = new CharacterStats
            {
                BaseHP = hp,
                BaseATK = atk,
                BaseDEF = def,
                CritRate = critRate / 100.0,
                CritDMG = critDmg / 100.0,
                EnergyRecharge = er / 100.0,
                ElementalMastery = em,
                ElementalDMGBonus = elemBonus / 100.0,
                PhysicalDMGBonus = physBonus / 100.0,
                CharacterElement = element
            };

            // Leer habilidades
            Console.WriteLine();
            int numSkills = ConsoleHelper.ReadInt(
                string.Format("  ¿Cuántas habilidades tiene {0} en esta rotación? (1-5): ", name));
            if (numSkills > 5) numSkills = 5;

            List<Skill> skills = new List<Skill>();
            for (int s = 1; s <= numSkills; s++)
            {
                Console.WriteLine();
                Console.WriteLine(string.Format("  · Habilidad {0} de {1}", s, name));

                Console.Write("    Nombre de la habilidad : ");
                string skillName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(skillName)) skillName = "Habilidad " + s;

                double motionValue = ConsoleHelper.ReadDouble("    Motion Value (ej: 2.5 = 250% ATK, 0 si escala HP/DEF): ");
                double hpScaling = ConsoleHelper.ReadDouble("    Escala de HP  (ej: 3.8 para 3.8%, 0 si no aplica): ");
                double defScaling = ConsoleHelper.ReadDouble("    Escala de DEF (ej: 0 si no aplica): ");
                int hits = ConsoleHelper.ReadInt("    Número de hits: ");

                Element dmgElem = ConsoleHelper.ReadEnum<Element>("    Elemento del daño —");
                ReactionType reaction = ConsoleHelper.ReadEnum<ReactionType>("    ¿Produce alguna reacción? —");

                skills.Add(new Skill
                {
                    Name = skillName,
                    MotionValue = motionValue / 100.0,  
                    HPScaling = hpScaling / 100.0,
                    DEFScaling = defScaling / 100.0,
                    Hits = hits,
                    DamageElement = dmgElem,
                    Reaction = reaction
                });
            }

            return new Character { Name = name, Stats = stats, Skills = skills };
        }
    }

    // ─── Punto de entrada ─────────────────────────────────────────────────────

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("╔═══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║     CALCULADORA DE ROTACIÓN — GENSHIN IMPACT                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════╝");
            Console.WriteLine("  Ingresa los datos de tus 4 personajes uno a uno.");
            Console.WriteLine("  Todos los porcentajes se ingresan como números enteros (ej: 65).");
            Console.WriteLine("  Motion Value: porcentaje del ATK (250 = 250% ATK).");
            Console.WriteLine();

            // Nombre de la rotación
            Console.Write("  Nombre de tu rotación (opcional): ");
            string rotationName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rotationName)) rotationName = "Mi Rotación";

            // Configuración del enemigo
            Console.WriteLine();
            ConsoleHelper.Separator('─');
            Console.WriteLine("  CONFIGURACIÓN DEL ENEMIGO");
            ConsoleHelper.Separator('─');
            int enemyLevel = ConsoleHelper.ReadInt("  Nivel del enemigo       (def. 90): ", 90);
            double enemyResistance = ConsoleHelper.ReadDouble("  Resistencia del enemigo (ej: 10 para 10%): ", 10);
            enemyResistance /= 100.0;

            // Lectura de los 4 personajes
            Rotation rotation = new Rotation { Name = rotationName };
            rotation.Characters = new List<Character>();

            Console.WriteLine();
            Console.WriteLine("  Ahora ingresa los datos de cada personaje en el orden de la rotación.");

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine();
                rotation.Characters.Add(CharacterInput.ReadCharacter(i));
            }

            // Calcular y mostrar resultado
            rotation.Calculate(enemyLevel, enemyResistance);

            Console.WriteLine();
            Console.WriteLine("  ℹ  Nota: Vaporize/Melt son reacciones amplificadoras. Multiplica");
            Console.WriteLine("     el daño final por x1.5 (Vaporize débil) o x2.0 (Melt/Vap. fuerte).");
            Console.WriteLine();
            Console.Write("  Presiona ENTER para salir...");
            Console.ReadLine();
        }
    }
}
