using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public static class Sorsolas
    {
        
        // Körbeforgatásos sorsolás: az első csapat rögzített, a többi fordulónként
        // egy hellyel tolódik. A hely-edik csapat a (csapatSzam - 1 - hely)-edikkel játszik.
        // Az első 7 forduló után a második félév ugyanez fordított hazai/vendég szereposztással.
        
        public static List<List<Meccs>> Keszit(List<Csapat> csapatok)
        {
            int csapatSzam = csapatok.Count;
            int elsoFelevForduloSzam = csapatSzam - 1;

            Csapat rogzitettCsapat = csapatok[0];
            List<Csapat> forgoCsapatok = csapatok.Skip(1).ToList();

            var elsoFelev = new List<List<Meccs>>();

            for (int fordulo = 0; fordulo < elsoFelevForduloSzam; fordulo++)
            {
                var helyek = new List<Csapat> { rogzitettCsapat };
                helyek.AddRange(forgoCsapatok);

                var forduloMeccsei = new List<Meccs>();
                for (int hely = 0; hely < csapatSzam / 2; hely++)
                {
                    Csapat elsoCsapat = helyek[hely];
                    Csapat masodikCsapat = helyek[csapatSzam - 1 - hely];

                    // A rögzített csapat ne legyen mindig hazai: minden második fordulóban cserélünk.
                    bool felcserel = hely == 0 && fordulo % 2 == 1;

                    forduloMeccsei.Add(felcserel
                        ? new Meccs(masodikCsapat, elsoCsapat)
                        : new Meccs(elsoCsapat, masodikCsapat));
                }
                elsoFelev.Add(forduloMeccsei);

                // Forgatás egy hellyel: az utolsó előre kerül.
                Csapat utolsoCsapat = forgoCsapatok[forgoCsapatok.Count - 1];
                forgoCsapatok.RemoveAt(forgoCsapatok.Count - 1);
                forgoCsapatok.Insert(0, utolsoCsapat);
            }

            // Második félév: ugyanazok a párosítások, felcserélt hazai/vendég szereppel.
            var sorsolas = new List<List<Meccs>>(elsoFelev);
            foreach (List<Meccs> elsoFelevFordulo in elsoFelev)
            {
                sorsolas.Add(elsoFelevFordulo
                    .Select(meccs => new Meccs(meccs.Vendeg, meccs.Hazai))
                    .ToList());
            }

            return sorsolas;
        }
    }
}
