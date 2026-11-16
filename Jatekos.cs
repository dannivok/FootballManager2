using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public class Jatekos
    {
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
