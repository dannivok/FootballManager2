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
    }
}
