using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public class Jatekos
    {
        public int Fittseg { get; set; } = 100;
        public bool Kezdo { get; set; }

        public string Nev { get; }
        public Poszt Poszt { get; }
        public int Ero { get; }

        public Jatekos(string nev, Poszt poszt, int ero)
        {
            Nev = nev;
            Poszt = poszt;
            Ero = ero;
        }
    }
}
