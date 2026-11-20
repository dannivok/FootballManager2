using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public static class Indulas
    {
        
        public static string NevBekeres()
        {
            while (true)
            {
                Console.Write("Add meg a menedzser nevét: ");
                string nev = (Console.ReadLine() ?? "").Trim();
                if (nev.Length > 0) return nev;
                Console.WriteLine("A név nem lehet üres!");
            }
        }

        public static Csapat CsapatValasztas(List<Csapat> csapatok)
        {
            for (int i = 0; i < csapatok.Count; i++)
                Console.WriteLine((i + 1) + ". " + csapatok[i].Nev);

            int n = Bevitel.SzamotKer("Válassz csapatot (1-" + csapatok.Count + "): ", 1, csapatok.Count);

            return csapatok[n - 1];
        }

        public static Difficulty DifficultyBekeres()
        {
            Console.WriteLine("[1] Easy");
            Console.WriteLine("[2] Normal");
            Console.WriteLine("[3] Hard");
            int difficultybekeres = Bevitel.SzamotKer("Nehézség: ", 1, 3);
            return (Difficulty)(difficultybekeres - 1);
        }
    }
}
