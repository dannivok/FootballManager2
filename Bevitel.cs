using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public static class Bevitel
    {
        public static int SzamotKer(string kerdes, int legkisebb, int legnagyobb)
        {
            while (true)
            {
                Console.Write(kerdes);
                string bevitel = Console.ReadLine();
                if (bevitel == null)
                {
                    Environment.Exit(0);
                }

                if (int.TryParse(bevitel.Trim(), out int szam) && szam >= legkisebb && szam <= legnagyobb)
                {
                    return szam;
                }

                Console.WriteLine("Érvénytelen bevitel. Egy " + legkisebb + " és " + legnagyobb + " közötti számot adj meg.");
            }
        }
    }
}
