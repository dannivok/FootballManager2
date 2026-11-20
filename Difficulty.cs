using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }

    // A difficultytől függő értékek. Egyedül itt van switch a difficulty-re.
    public class DifficultyBeallitasok
    {
        public int SajatEroBonusz { get; }
        public int EllenfelEroBonusz { get; }
        public int SerulesEsely { get; }          // százalék
        public int FittsegVeszteseg { get; }      // saját kezdők, meccsenként

        public DifficultyBeallitasok(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    SajatEroBonusz = 5;
                    EllenfelEroBonusz = -3;
                    SerulesEsely = 3;
                    FittsegVeszteseg = 4;
                    break;
                case Difficulty.Hard:
                    SajatEroBonusz = -5;
                    EllenfelEroBonusz = 3;
                    SerulesEsely = 7;
                    FittsegVeszteseg = 10;
                    break;
                default: // Normal
                    SajatEroBonusz = 0;
                    EllenfelEroBonusz = 0;
                    SerulesEsely = 5;
                    FittsegVeszteseg = 6;
                    break;
            }
        }
    }
}
