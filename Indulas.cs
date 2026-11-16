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

            while (true)
            {
                Console.Write("Válassz csapatot (1-" + csapatok.Count + "): ");
                if (int.TryParse(Console.ReadLine(), out int n) && n >= 1 && n <= csapatok.Count)
                    return csapatok[n - 1];
                Console.WriteLine("Érvénytelen választás.");
            }
        }
    }
}
