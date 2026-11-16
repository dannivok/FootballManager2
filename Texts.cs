namespace FootballManager
{
    /// <summary>
    /// Kész szövegek a játékhoz. Szabadon használhatod, bővítheted és átírhatod őket.
    /// Véletlen elem egy tömbből: Texts.GoalComments[random.Next(Texts.GoalComments.Length)]
    /// </summary>
    internal static class Texts
    {
        public static readonly string Title = @"
 _____          _                               _
|  ___|__   ___(_)_ __ ___   ___ _ __   ___  __| |_______  ___ _ __
| |_ / _ \ / __| | '_ ` _ \ / _ \ '_ \ / _ \/ _` |_  / __|/ _ \ '__|
|  _| (_) | (__| | | | | | |  __/ | | |  __/ (_| |/ /\__ \  __/ |
|_|  \___/ \___|_|_| |_| |_|\___|_| |_|\___|\__,_/___|___/\___|_|";

        // ---- Gólok (A3) ----

        public static readonly string[] GoalComments =
        {
            "bombagól a felső sarokba!",
            "fejjel csúsztatott a kapuba.",
            "higgadtan lőtt a kapus mellett.",
            "kapott egy kis szerencsét, megpattant a labda.",
            "átemelte a kifutó kapust.",
        };

        // ---- Sérülések (A1) ----

        public static readonly string[] InjuryComments =
        {
            "rosszul érkezett egy fejpárbaj után.",
            "bokája megbicsaklott egy becsúszásnál.",
            "combizomhúzódással állt ki.",
        };

        // ---- A szezon értékelése (K6) ----

        public static readonly string[] Verdicts =
        {
            "Bajnok! Az egész város ünnepel.",
            "Dobogó! Szép szezon volt.",
            "Középmezőny. Van még hová fejlődni.",
            "Kiesés. A szurkolók csalódottak.",
        };
    }
}
