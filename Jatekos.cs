using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public class Jatekos
    {
        public int Fittseg { get; private set; } = 100;
        public bool Kezdo { get; set; }
        public int TenylegesEro => Ero * (100 + Fittseg) / 200;
        public int Golok { get; set; }

        public string Nev { get; }
        public Poszt Poszt { get; }
        public int Ero { get; }

        public Jatekos(string nev, Poszt poszt, int ero)
        {
            Nev = nev;
            Poszt = poszt;
            Ero = ero;
        }

        public void FittsegFrissit(bool kezdoVolt)
        {
            if (kezdoVolt)
                Fittseg -= 6;
            else
                Fittseg += 15;

            if (Fittseg < 0) Fittseg = 0;
            
            if (Fittseg > 100) Fittseg = 100;
        }

        public void FittsegHozzaad(int mennyiseg)
        {
            Fittseg += mennyiseg;

            if (Fittseg > 100) Fittseg = 100;
        }
    }
}
