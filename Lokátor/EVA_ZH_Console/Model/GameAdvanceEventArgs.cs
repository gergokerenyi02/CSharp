using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVA_ZH_Console.Model
{
    public class GameAdvanceEventArgs : EventArgs
    {
        #region Fields
        public GameField[,] gameTable;
        public int gamePoints;
        #endregion

        public GameAdvanceEventArgs(GameField[,] gameTable, int gamePoints)
        {
            this.gameTable = gameTable;
            this.gamePoints = gamePoints;
        }
    }
}
