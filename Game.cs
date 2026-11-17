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

            // TODO: a játék főciklusa (K1-K6)

            Console.WriteLine("Nyomj meg egy billentyűt a kilépéshez.");
            Console.ReadKey(true);
        }

        private const int OsszesForduloSzam = 14;
        private const int KilepesValasztas = 5;

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
                        GepiKezdoBeallit();
                        FittsegFrissitMindenki();
                        break;
                    case 2:
                        Kezdocsapat();
                        break;
                    case 3:
                        Console.WriteLine("Ez a funkció még nem elérhető.");
                        break;
                    case 4:
                        KeretKiir();
                        break;
                }
            }
        }

        private string StatuszSor()
        {
            int helyezes = 1 + csapatok.Count(masikCsapat => masikCsapat.Pont > sajatCsapat.Pont);
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


    }
}
