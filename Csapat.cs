using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public class Csapat
    {

        public int Pont { get; set; }

        public string Nev { get; }
        public List<Jatekos> Jatekosok { get; } = new List<Jatekos>();

        public Csapat(string nev)
        {
            Nev = nev;
        }

        public int Ero => Jatekosok.Where(j => j.Kezdo).Sum(j => j.TenylegesEro) / 11;

        public void KezdoBeallit()
        {
            foreach (Jatekos jatekos in Jatekosok)
                jatekos.Kezdo = false;

            foreach (Jatekos jatekos in Jatekosok.OrderByDescending(j => j.TenylegesEro).Take(11))
                jatekos.Kezdo = true;
        }
    }
}
