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
                    case 2:
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
    }
}
