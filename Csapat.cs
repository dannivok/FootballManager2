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

        public const int JuniorEro = 40;
        public int JuniorSzam { get; set; }

        public int Ero => (Jatekosok.Where(jatekos => jatekos.Kezdo).Sum(jatekos => jatekos.TenylegesEro)
                           + JuniorSzam * JuniorEro) / 11;

        public void KezdoBeallit()
        {
            foreach (Jatekos jatekos in Jatekosok)
                jatekos.Kezdo = false;

            foreach (Jatekos jatekos in Jatekosok.OrderByDescending(j => j.TenylegesEro).Take(11))
                jatekos.Kezdo = true;
        }

         
        public int MeccsSzam { get; private set; }
         
        public int Gyozelem { get; private set; }
         
        public int Dontetlen { get; private set; }
         
        public int Vereseg { get; private set; }
         
        public int LottGol { get; private set; }
         
        public int KapottGol { get; private set; }
         
        public int GolKulonbseg => LottGol - KapottGol;

         
        public void EredmenyRogzit(int lott, int kapott)
         
        {
             
            MeccsSzam++;
             
            LottGol += lott;
             
            KapottGol += kapott;
             
            if (lott > kapott) { Gyozelem++; Pont += 3; }
             
            else if (lott == kapott) { Dontetlen++; Pont += 1; }
             
            else Vereseg++;
             
        }



    }
}
