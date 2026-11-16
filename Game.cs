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
            List<Csapat> csapatok = Betoltes.Beolvas();
            Csapat sajatCsapat = Indulas.CsapatValasztas(csapatok);

            Console.WriteLine();
            Console.WriteLine(menedzserNev + " csapata: " + sajatCsapat.Nev);

            // TODO: a játék főciklusa (K1-K6)

            Console.WriteLine("Nyomj meg egy billentyűt a kilépéshez.");
            Console.ReadKey(true);
        }
    }
}
