using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVA_ZH_Console.Model
{
    public class GameOverEventArgs
    {
        #region Fields
        public bool isWon;
        public int gamePoints;
        #endregion

        public GameOverEventArgs(bool isWon, int gamePoints)
        {
            this.isWon = isWon;
            this.gamePoints = gamePoints;
        }
    }
}
