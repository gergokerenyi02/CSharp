using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vadaszat.Persistence;

namespace Vadaszat.Model
{
    public class VadaszatEventArgs : EventArgs
    {

    }


    public class MapSizeChangedEventArgs : EventArgs
    {
        public Game Game { get; private set; }

        public IVadaszatDataAccess DataAccess { get; private set; }



        /*public MapSizeChangedEventArgs(Game game)
        {
            Game = game;

        }*/

        public MapSizeChangedEventArgs(Game game, IVadaszatDataAccess dataAccess) {
            Game = game;
            this.DataAccess = dataAccess;

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
}
