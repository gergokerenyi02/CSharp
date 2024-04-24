using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadaszat.Model
{
    public class VadaszatEventArgs : EventArgs
    {
        
    }
    /*
    public class NewTurnEventArgs : EventArgs
    {
        public Game.PlayerTurn NewPlayer { get; set; }
        public int selectedChar { get; set; }

    }

    public class SwitchedCharacterArgs : EventArgs
    {
        public int selectedCharacter { get; set; }

    }

    public class StepCounterArgs : EventArgs
    {
        public int newStepCounter;
    }

    public class CheckEndGameArgs : EventArgs
    {
        public bool isEndGame { get; set; }
        public Player newWinner { get; set; }

    }
    */
}
