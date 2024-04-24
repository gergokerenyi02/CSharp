using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vadaszat.Persistence;

namespace Vadaszat.Model
{
    public class VadaszatEventArgs : EventArgs
    {
        
    }

    
    public class MapSizeChangedEventArgs : EventArgs
    {
        public Game Game { get; private set; }

        


        public MapSizeChangedEventArgs(Game game)
        {
            Game = game;
            
        }
    }

    public class MoveEventArgs : EventArgs
    {
        public Direction Direction { get; private set; }

        public MoveEventArgs(string direction)
        {
            switch (direction)
            {
                case "up":
                    Direction = Direction.Up;
                    break;
                case "down":
                    Direction = Direction.Down;
                    break;
                case "left":

                    Direction = Direction.Left;
                    break;
                case "right":
                    Direction = Direction.Right;
                    break;
                default:
                    return;
            }
        }
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
