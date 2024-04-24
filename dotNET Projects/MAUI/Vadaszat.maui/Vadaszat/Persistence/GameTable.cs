using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vadaszat.Model;

namespace Vadaszat.Persistence
{
    public class GameTable
    {
        #region Fields


        public int MapSize { get; private set; }
        public int[,] map { get; private set; }

        public int maxSteps { get; private set; }
        public int currentSteps { get; private set; }

        public Runner? player1 { get; private set; }
        public Attacker? player2 { get; private set; }

        public PlayerTurn currentPlayer { get; private set; }


        #endregion

        #region Constructor

        public GameTable(int n)
        {
            MapSize = n;
            maxSteps = 4 * n;
            currentSteps = 0;

            map = new int[MapSize, MapSize];

            player1 = (Runner?)Player.createPlayer(1);
            player2 = (Attacker?)Player.createPlayer(2);


            currentPlayer = PlayerTurn.Player1;


            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if ((i == 0 || i == MapSize - 1) && (j == 0 || j == MapSize - 1))
                    {
                        map[i, j] = 2;
                    }
                    else if (i == (MapSize - 1) / 2 && j == (MapSize - 1) / 2)
                    {
                        map[i, j] = 1;


                    }
                    else
                    {
                        map[i, j] = 0;
                    }
                }
            }


            InitializePlayerLocation();
        }
        #endregion

        #region Public methods


        public bool Move(Direction e, int selectedCharacterIndex) // Direction Enum
        {
            bool moved = false;

            switch (currentPlayer)
            {


                case PlayerTurn.Player1:

                    if (player1 == null)
                    {

                        return false;
                    }

                    Point currentLocation = player1.Location();

                    if (canMove(currentLocation, e, out Point newLocation))
                    {

                        player1?.Move(newLocation);
                        map[currentLocation.X, currentLocation.Y] = 0;
                        map[newLocation.X, newLocation.Y] = 1;
                        moved = true;



                    }
                    else
                    {
                        moved = false;

                    }
                    break;


                case PlayerTurn.Player2:

                    if (player2 == null)
                    {
                        return false;
                    }
                    Point currentLocation2 = player2.Location(selectedCharacterIndex);

                    if (canMove(currentLocation2, e, out Point newLocation2) && currentSteps < maxSteps)
                    {
                        player2?.Move(newLocation2, selectedCharacterIndex);
                        map[currentLocation2.X, currentLocation2.Y] = 0;
                        map[newLocation2.X, newLocation2.Y] = 2;

                        incrementSteps();

                        moved = true;

                    }
                    else
                    {
                        moved = false;

                    }
                    break;

                default:
                    break;

            }

            if (moved)
            {
                SwitchPlayer();
            }

            return moved;
        }

        public void SetCurrentPlayer(PlayerTurn player)
        {
            currentPlayer = player;
        }

        public void SetCurrentSteps(int currentSteps)
        {
            this.currentSteps = maxSteps - currentSteps;
        }
        public void ReInitialize()
        {
            currentPlayer = PlayerTurn.Player1;
            currentSteps = 0;

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if ((i == 0 || i == MapSize - 1) && (j == 0 || j == MapSize - 1))
                    {
                        map[i, j] = 2;
                    }
                    else if (i == (MapSize - 1) / 2 && j == (MapSize - 1) / 2)
                    {
                        map[i, j] = 1;
                    }
                    else
                    {
                        map[i, j] = 0;
                    }
                }
            }

            player1 = (Runner?)Player.createPlayer(1);
            player2 = (Attacker?)Player.createPlayer(2);

            InitializePlayerLocation();
            //RefreshPlayerTurn();
            //RefreshStepCounter();
        }
        #endregion

        #region Private methods

        // majd visszairni private-ra
        public void InitializePlayerLocation()
        {
            player2?.ResetSetPositions();

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (map[i, j] == 2)
                    {
                        player2?.SetPlayer(new Point(i, j));
                        // Lerakok ide player2-t
                    }
                    else if (map[i, j] == 1)
                    {
                        player1?.SetPlayer(new Point(i, j));
                    }
                    else
                    {
                        // semmi
                    }
                }
            }
        }

        private bool canMove(Point from, Direction direction, out Point newLocation)
        {

            newLocation = new Point(-1, -1);
            int x = from.X;
            int y = from.Y;
            switch (direction)
            {
                case Direction.Left:
                    if (y != 0)
                    {
                        if (map[x, y - 1] == 0)
                        {
                            newLocation = new Point(x, y - 1);
                            return true;
                        }
                    }
                    return false;

                case Direction.Right:
                    if (y != MapSize - 1)
                    {
                        if (map[x, y + 1] == 0)
                        {
                            newLocation = new Point(x, y + 1);
                            return true;
                        }
                    }
                    return false;


                case Direction.Up:
                    if (x != 0)
                    {
                        if (map[x - 1, y] == 0)
                        {
                            newLocation = new Point(x - 1, y);
                            return true;
                        }
                    }
                    return false;

                case Direction.Down:
                    if (x != MapSize - 1)
                    {
                        if (map[x + 1, y] == 0)
                        {
                            newLocation = new Point(x + 1, y);
                            return true;
                        }
                    }
                    return false;

            }
            return false;

        }



        private void incrementSteps()
        {
            currentSteps++;

        }


        private void SwitchPlayer()
        {
            currentPlayer = currentPlayer == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
        }
        #endregion
    }
}
