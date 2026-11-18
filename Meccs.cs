using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public class Meccs
    {
        public Csapat Hazai { get; }
        public Csapat Vendeg { get; }
        public int HazaiGol { get; set; }
        public int VendegGol { get; set; }

        public Meccs(Csapat hazai, Csapat vendeg)
        {
            Hazai = hazai;
            Vendeg = vendeg;
        }
    }

    public class Gol
    {
        public int Perc { get; }
        public Jatekos Lovo { get; }

        public Gol(int perc, Jatekos lovo)
        {
            Perc = perc;
            Lovo = lovo;
        }
    }
}
