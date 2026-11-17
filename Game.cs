using System.Text;

namespace FootballManager
{
    internal class Game
    {
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8; // hogy az ékezetes betűk jól jelenjenek meg
            Console.WriteLine(Texts.Title);
            Console.WriteLine();

            string menedzserNev = Indulas.NevBekeres();
            csapatok = Betoltes.Beolvas();

            foreach (Csapat csapat in csapatok)
                csapat.KezdoBeallit();

            sajatCsapat = Indulas.CsapatValasztas(csapatok);

            Console.WriteLine();
            Console.WriteLine(menedzserNev + " csapata: " + sajatCsapat.Nev);
            Fomenu();
            if (lejatszottFordulokSzama < OsszesForduloSzam)
                SzezonVege();

            // TODO: a játék főciklusa (K1-K6)

            Console.WriteLine("Nyomj meg egy billentyűt a kilépéshez.");
            Console.ReadKey(true);
        }

        private const int OsszesForduloSzam = 14;
        private const int KilepesValasztas = 5;

        private Random random = new Random();

        private List<Csapat> csapatok;
        private Csapat sajatCsapat;
        private int lejatszottFordulokSzama;

        private void Fomenu()
        {
            int valasztas = 0;
            while (valasztas != KilepesValasztas)
            {
                Console.WriteLine();
                Console.WriteLine(StatuszSor());
                Console.WriteLine("[1] Következő forduló");
                Console.WriteLine("[2] Kezdőcsapat");
                Console.WriteLine("[3] Tabella");
                Console.WriteLine("[4] Keret");
                Console.WriteLine("[5] Kilépés");

                valasztas = Bevitel.SzamotKer("Választás: ", 1, KilepesValasztas);

                switch (valasztas)
                {
                    case 1:
                        KovetkezoForduloLejatszik();

                        FittsegFrissitMindenki();
                        break;
                    case 2:
                        Kezdocsapat();
                        break;
                    case 3:
                        TabellaKiir();
                        break;
                    case 4:
                        KeretKiir();
                        break;
                }
            }
        }

        private void KovetkezoForduloLejatszik()
        {
            if (lejatszottFordulokSzama >= OsszesForduloSzam)
            {
                Console.WriteLine("A szezon véget ért.");
                return;
            }

            GepiKezdoBeallit();


            var kevert = csapatok.OrderBy(csapat => random.Next()).ToList();
            var meccsek = new List<Meccs>();
            for (int sorszam = 0; sorszam + 1 < kevert.Count; sorszam += 2)
                meccsek.Add(MeccsJatszik(kevert[sorszam], kevert[sorszam + 1]));

            FittsegFrissitMindenki();
            lejatszottFordulokSzama++;

            Console.WriteLine();
            Console.WriteLine(lejatszottFordulokSzama + ". forduló eredményei:");
            foreach (Meccs meccs in meccsek)
            {
                string jel = (meccs.Hazai == sajatCsapat || meccs.Vendeg == sajatCsapat) ? ">> " : "   ";
                Console.WriteLine(jel + meccs.Hazai.Nev + " " + meccs.HazaiGol + " - " + meccs.VendegGol + " " + meccs.Vendeg.Nev);
            }
            if (lejatszottFordulokSzama == OsszesForduloSzam)
                SzezonVege();
        }

        private Meccs MeccsJatszik(Csapat hazai, Csapat vendeg)
        {
            Meccs meccs = new Meccs(hazai, vendeg);

            int hazaiEsely = EselySzamol(hazai.Ero, vendeg.Ero);
            int vendegEsely = EselySzamol(vendeg.Ero, hazai.Ero);

            for (int helyzet = 0; helyzet < 6; helyzet++)
            {
                if (random.Next(100) < hazaiEsely) meccs.HazaiGol++;
                if (random.Next(100) < vendegEsely) meccs.VendegGol++;
            }
            hazai.EredmenyRogzit(meccs.HazaiGol, meccs.VendegGol);
            vendeg.EredmenyRogzit(meccs.VendegGol, meccs.HazaiGol);

            return meccs;
        }

        private int EselySzamol(int sajatEro, int ellenfelEro)
        {
            return Math.Clamp(15 + (sajatEro - ellenfelEro) / 2, 5, 40);
        }


        private string StatuszSor()
        {
            int helyezes = TabellaRendez().IndexOf(sajatCsapat) + 1;
            return sajatCsapat.Nev + " | Forduló " + lejatszottFordulokSzama + "/" + OsszesForduloSzam
                + " | " + helyezes + ". hely | " + sajatCsapat.Pont + " pont";
        }

        private void KeretKiir()
        {
            Console.WriteLine();
            Console.WriteLine($"{"Név",-22}{"Poszt",-14}{"Erő",5}{"Fittség",9}  Kezdő");
            foreach (Jatekos jatekos in sajatCsapat.Jatekosok)
            {
                string kezdoSzoveg = jatekos.Kezdo ? "igen" : "nem";
                Console.WriteLine($"{jatekos.Nev,-22}{jatekos.Poszt,-14}{jatekos.Ero,5}{jatekos.Fittseg + "%",9}  {kezdoSzoveg}");
            }
        }

        private void Kezdocsapat()
        {
            while (true)
            {
                var kezdok = sajatCsapat.Jatekosok.Where(j => j.Kezdo).ToList();
                var kispad = sajatCsapat.Jatekosok.Where(j => !j.Kezdo).ToList();

                Console.WriteLine();
                Console.WriteLine("Kezdők (csapat ereje: " + sajatCsapat.Ero + ")");
                for (int i = 0; i < kezdok.Count; i++)
                    Console.WriteLine($"{i + 1,2}. {kezdok[i].Nev,-22}{kezdok[i].Poszt,-14}{kezdok[i].TenylegesEro,4}{kezdok[i].Fittseg + "%",7}");

                Console.WriteLine("Kispad");
                for (int i = 0; i < kispad.Count; i++)
                    Console.WriteLine($"{i + 1,2}. {kispad[i].Nev,-22}{kispad[i].Poszt,-14}{kispad[i].TenylegesEro,4}{kispad[i].Fittseg + "%",7}");

                int le = Bevitel.SzamotKer("Ki menjen le? (1-" + kezdok.Count + ", 0 = vissza): ", 0, kezdok.Count);
                if (le == 0) return;

                int be = Bevitel.SzamotKer("Ki jöjjön be? (1-" + kispad.Count + ", 0 = vissza): ", 0, kispad.Count);
                if (be == 0) return;

                kezdok[le - 1].Kezdo = false;
                kispad[be - 1].Kezdo = true;
            }
        }
        private void GepiKezdoBeallit()
        {
            foreach (Csapat csapat in csapatok)
            {
                if (csapat != sajatCsapat)
                    csapat.KezdoBeallit();
            }
        }

        private void FittsegFrissitMindenki()
        {
            foreach (Csapat csapat in csapatok)
            {
                foreach (Jatekos jatekos in csapat.Jatekosok)
                    jatekos.FittsegFrissit(jatekos.Kezdo);
            }
        }

        private List<Csapat> TabellaRendez()
        {
            return csapatok
                .OrderByDescending(csapat => csapat.Pont)
                .ThenByDescending(csapat => csapat.GolKulonbseg)
                .ThenByDescending(csapat => csapat.LottGol)
                .ToList();
        }

        private void TabellaKiir()
        {
            var tabella = TabellaRendez();
            Console.WriteLine();
            Console.WriteLine($"{"Helyezés",9} {"Csapat",-20}{"Meccs",7}{"Győzelem",10}{"Döntetlen",11}{"Vereség",9}{"Lőtt gól",10}{"Kapott gól",12}{"Gólkülönbség",14}{"Pont",6}");
            for (int sorszam = 0; sorszam < tabella.Count; sorszam++)
            {
                Csapat csapat = tabella[sorszam];
                Console.WriteLine($"{sorszam + 1,9} {csapat.Nev,-20}{csapat.MeccsSzam,7}{csapat.Gyozelem,10}{csapat.Dontetlen,11}{csapat.Vereseg,9}{csapat.LottGol,10}{csapat.KapottGol,12}{csapat.GolKulonbseg,14}{csapat.Pont,6}");
            }
        }

        private void SzezonVege()
        {
            Console.WriteLine();
            Console.WriteLine("Végső tabella:");
            TabellaKiir();

            int helyezes = TabellaRendez().IndexOf(sajatCsapat) + 1;
            string ertekeles;
            if (helyezes == 1) ertekeles = Texts.Verdicts[0];
            else if (helyezes <= 3) ertekeles = Texts.Verdicts[1];
            else if (helyezes <= 6) ertekeles = Texts.Verdicts[2];
            else ertekeles = Texts.Verdicts[3];

        }
    }
}
