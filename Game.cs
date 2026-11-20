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
            beallitasok = new DifficultyBeallitasok(Indulas.DifficultyBekeres());
            csapatok = Betoltes.Beolvas();

            foreach (Csapat csapat in csapatok)
                csapat.KezdoBeallit();
            //csapatok = Betoltes.Beolvas();
            sorsolas = Sorsolas.Keszit(csapatok);

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
        private List<List<Meccs>> sorsolas;

        private Edzes valasztottEdzes;
        private DifficultyBeallitasok beallitasok;

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

                        //FittsegFrissitMindenki();
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

        private void EdzestValaszt()
        {
            Console.WriteLine();
            Console.WriteLine("Edzés a forduló előtt:");
            Console.WriteLine("[1] Támadás");
            Console.WriteLine("[2] Védekezés");
            Console.WriteLine("[3] Regeneráció");

            int valasztas = Bevitel.SzamotKer("Edzés: ", 1, 3);

            if (valasztas == 1) valasztottEdzes = Edzes.Tamadas;
            else if (valasztas == 2) valasztottEdzes = Edzes.Vedekezes;
            else valasztottEdzes = Edzes.Regeneracio;
        }

        private void KovetkezoForduloLejatszik()
        {
            if (lejatszottFordulokSzama >= OsszesForduloSzam)
            {
                Console.WriteLine("A szezon véget ért.");
                return;
            }
            EdzestValaszt();
            GepiKezdoBeallit();


            List<Meccs> meccsek = sorsolas[lejatszottFordulokSzama];
            foreach (Meccs meccs in meccsek)
                MeccsJatszik(meccs);

            FittsegFrissitMindenki();

            if (valasztottEdzes == Edzes.Regeneracio)
            {
                foreach (Jatekos jatekos in sajatCsapat.Jatekosok)
                    jatekos.FittsegHozzaad(5);
            }

            lejatszottFordulokSzama++;

            Console.WriteLine();
            Console.WriteLine(lejatszottFordulokSzama + ". forduló eredményei:");
            foreach (Meccs meccs in meccsek)
            {
                string jel = (meccs.Hazai == sajatCsapat || meccs.Vendeg == sajatCsapat) ? ">> " : "   ";
                Console.WriteLine(jel + meccs.Hazai.Nev + " " + meccs.HazaiGol + " - " + meccs.VendegGol + " " + meccs.Vendeg.Nev);
            }
            SerulesekKezel();
            if (lejatszottFordulokSzama == OsszesForduloSzam)
                SzezonVege();
        }

       
        private string StatuszSor()
        {
            int helyezes = TabellaRendez().IndexOf(sajatCsapat) + 1;
            string kovetkezoSzoveg = "";

            if (lejatszottFordulokSzama < OsszesForduloSzam)
            {
                Meccs kovetkezoMeccs = sorsolas[lejatszottFordulokSzama]
                    .First(meccs => meccs.Hazai == sajatCsapat || meccs.Vendeg == sajatCsapat);
                bool hazaiMeccs = kovetkezoMeccs.Hazai == sajatCsapat;
                Csapat ellenfel = hazaiMeccs ? kovetkezoMeccs.Vendeg : kovetkezoMeccs.Hazai;
                kovetkezoSzoveg = " | Következő: " + ellenfel.Nev + (hazaiMeccs ? " (hazai)" : " (idegen)");
            }

            return sajatCsapat.Nev + " | Forduló " + lejatszottFordulokSzama + "/" + OsszesForduloSzam
                + " | " + helyezes + ". hely | " + sajatCsapat.Pont + " pont" + kovetkezoSzoveg;
        }

        private void MeccsJatszik(Meccs meccs)
        {
            Csapat hazai = meccs.Hazai;
            Csapat vendeg = meccs.Vendeg;

            int hazaiModosito = 0;
            int vendegModosito = 0;
            int hazaiEroBonusz = 0;
            int vendegEroBonusz = 0;

            if (hazai == sajatCsapat)
            {
                hazaiEroBonusz = beallitasok.SajatEroBonusz;
                vendegEroBonusz = beallitasok.EllenfelEroBonusz;
                if (valasztottEdzes == Edzes.Tamadas) hazaiModosito = 5;
                else if (valasztottEdzes == Edzes.Vedekezes) vendegModosito = -5;
            }
            else if (vendeg == sajatCsapat)
            {
                vendegEroBonusz = beallitasok.SajatEroBonusz;
                hazaiEroBonusz = beallitasok.EllenfelEroBonusz;
                if (valasztottEdzes == Edzes.Tamadas) vendegModosito = 5;
                else if (valasztottEdzes == Edzes.Vedekezes) hazaiModosito = -5;
            }

            int hazaiEsely = EselySzamol(hazai.Ero+hazaiEroBonusz, vendeg.Ero+vendegEroBonusz, hazaiModosito);
            int vendegEsely = EselySzamol(vendeg.Ero+vendegEroBonusz, hazai.Ero+hazaiEroBonusz, vendegModosito);

            for (int helyzet = 0; helyzet < 6; helyzet++)
            {
                if (random.Next(100) < hazaiEsely) meccs.HazaiGol++;
                if (random.Next(100) < vendegEsely) meccs.VendegGol++;
            }
            hazai.EredmenyRogzit(meccs.HazaiGol, meccs.VendegGol);
            vendeg.EredmenyRogzit(meccs.VendegGol, meccs.HazaiGol);

            // return meccs;
        }

        private int EselySzamol(int sajatEro, int ellenfelEro,int modosito)
        {
            return Math.Clamp(15 + (sajatEro - ellenfelEro) / 2 + modosito, 5, 40);
        }

        private void KezdoPotol()
        {
            int kezdoSzam = sajatCsapat.Jatekosok.Count(jatekos => jatekos.Kezdo);

            List<Jatekos> egeszsegesKispad = sajatCsapat.Jatekosok
                .Where(jatekos => !jatekos.Kezdo && !jatekos.Serult)
                .OrderByDescending(jatekos => jatekos.TenylegesEro)
                .ToList();

            int kispadIndex = 0;
            while (kezdoSzam < 11 && kispadIndex < egeszsegesKispad.Count)
            {
                egeszsegesKispad[kispadIndex].Kezdo = true;
                kispadIndex++;
                kezdoSzam++;
            }
            sajatCsapat.JuniorSzam = 11 - kezdoSzam;
            if (sajatCsapat.JuniorSzam > 0)
                Console.WriteLine("Nincs elég egészséges játékos, a következő meccsen junior áll be (erő " + Csapat.JuniorEro + ").");
        }

        private void SerulesekKezel()
        {
            // A junior csak egy meccsig marad.
            sajatCsapat.JuniorSzam = 0;

            // A már sérültek közeledtek a gyógyuláshoz.
            foreach (Jatekos jatekos in sajatCsapat.Jatekosok)
                jatekos.SerulesCsokkent();

            // Új sérülések: minden kezdő 5% eséllyel. (Normal modeon)
            foreach (Jatekos kezdo in sajatCsapat.Jatekosok.Where(jatekos => jatekos.Kezdo).ToList())
            {
                if (random.Next(100) < beallitasok.SerulesEsely)
                {
                    int fordulok = random.Next(1, 4);
                    kezdo.Megserul(fordulok);
                    Console.WriteLine(kezdo.Nev + " " + Texts.InjuryComments[random.Next(Texts.InjuryComments.Length)]
                        + " (" + fordulok + " forduló kihagyás)");
                }
            }

            KezdoPotol();
        }


        private void KeretKiir()
        {
            Console.WriteLine();
            Console.WriteLine($"{"Név",-22}{"Poszt",-14}{"Erő",5}{"Fittség",9}  {"Kezdő",-7}{"Sérülés",-10}");
            foreach (Jatekos jatekos in sajatCsapat.Jatekosok)
            {
                string kezdoSzoveg = jatekos.Kezdo ? "igen" : "nem";
                string serulesSzoveg = jatekos.Serult ? jatekos.SerulesFordulo + " fordulóig" : "-";
                Console.WriteLine($"{jatekos.Nev,-22}{jatekos.Poszt,-14}{jatekos.Ero,5}{jatekos.Fittseg + "%",9}  {kezdoSzoveg,-7}{serulesSzoveg,-10}");
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

                if (kispad[be - 1].Serult)
                {
                    Console.WriteLine("Sérült játékos nem lehet kezdő.");
                    continue;
                }

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
                    jatekos.FittsegFrissit(jatekos.Kezdo, beallitasok.FittsegVeszteseg);
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

            Console.WriteLine();
            Console.WriteLine("Helyezésed: " + helyezes + ".");
            Console.WriteLine(ertekeles);

        }


    }
}
