using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public static class Betoltes
    {
        
        public static List<Csapat> Beolvas(string utvonal = "players.txt")
        {
            if (!File.Exists(utvonal))
            {
                Console.WriteLine("A(z) " + utvonal + " fájl nem létezik.");
                Environment.Exit(0);
            }

            var csapatok = new List<Csapat>();
            var szotar = new Dictionary<string, Csapat>();

            foreach (string sor in File.ReadAllLines(utvonal, Encoding.UTF8))
            {
                string[] sorok = sor.Split(';');
                if (sorok.Length != 4) continue;

                string csapatNev = sorok[0].Trim();
                string jatekosNev = sorok[1].Trim();
                if (csapatNev.Length == 0 || jatekosNev.Length == 0) continue;
                if (csapatNev == "Szabad") continue;
                if (!int.TryParse(sorok[3].Trim(), out int ero) || ero <= 0) continue;
                //ne legyen nulla vagy kevesebb az ero az nemjo:((
                if (!PosztBeolvas(sorok[2].Trim(), out Poszt poszt)) continue;
                //elhezelyezi a posztokat a jatekosoknak
                if (!szotar.TryGetValue(csapatNev, out Csapat csapat))
                {
                    csapat = new Csapat(csapatNev);
                    szotar[csapatNev] = csapat;
                    csapatok.Add(csapat);
                }
                //ha nem letetik a csapat akkor megcsinalja es listaba adja
                csapat.Jatekosok.Add(new Jatekos(jatekosNev, poszt, ero));
                //uj jatekos:))
            }
            return csapatok;
        }

        private static bool PosztBeolvas(string szoveg, out Poszt poszt)
        {
            switch (szoveg)
            {
                case "Kapus": poszt = Poszt.Kapus; return true;
                case "Védő": poszt = Poszt.Védő; return true;
                case "Középpályás": poszt = Poszt.Középpályás; return true;
                case "Csatár": poszt = Poszt.Csatár; return true;
                default: poszt = default; return false;
            }
        }
    }
}
