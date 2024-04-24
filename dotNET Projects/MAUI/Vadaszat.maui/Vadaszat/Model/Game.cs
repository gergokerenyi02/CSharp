using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Vadaszat.Persistence;

namespace Vadaszat.Model
{

    #region VadaszatEventArgs

    public class NewTurnEventArgs : EventArgs
    {
        public PlayerTurn NewPlayer { get; set; }
        public int SelectedChar { get; set; }

    }

    public class SwitchedCharacterArgs : EventArgs
    {
        public int SelectedCharacter { get; set; }

    }

    public class StepCounterArgs : EventArgs
    {
        public int NewStepCounter { get; set; }
    }

    public class CheckEndGameArgs : EventArgs
    {
        public bool IsEndGame { get; set; }
        public Player? NewWinner { get; set; }

    }
    #endregion


    public class Game
    {
        #region Events

        public event EventHandler<NewTurnEventArgs>? NewTurn;
        public event EventHandler<SwitchedCharacterArgs>? SwitchedCharachter;
        public event EventHandler<CheckEndGameArgs>? GotWinner;
        public event EventHandler<StepCounterArgs>? OnStep; // Frisitem a hátralévő lépések számát
        #endregion


        #region Private Event methods

        private void GotGameWinner(Player? winner, bool gotWinner)
        {
            if (gotWinner)
            {
                GotWinner?.Invoke(this, new CheckEndGameArgs { IsEndGame = true, NewWinner = winner });
            }
            else
            {
                GotWinner?.Invoke(this, new CheckEndGameArgs { IsEndGame = false, NewWinner = winner });
            }
        }
        private void OnSelectedCharacterChange()
        {
            SwitchedCharachter?.Invoke(this, new SwitchedCharacterArgs { SelectedCharacter = SelectedCharacterIndex });
        }

        private void RefreshPlayerTurn()
        {
            NewTurn?.Invoke(this, new NewTurnEventArgs { NewPlayer = (PlayerTurn)GameTable.currentPlayer, SelectedChar = SelectedCharacterIndex });
        }

        private void RefreshStepCounter()
        {
            OnStep?.Invoke(this, new StepCounterArgs { NewStepCounter = GameTable.maxSteps - GameTable.currentSteps });
        }

        #endregion
        #region Fields

        IVadaszatDataAccess _dataAccess;


        //private static Game? _game3x3;
        //private static Game? _game5x5;
        //private static Game? _game7x7;

        public bool IsGameEnded { get; private set; } = false;

        public GameTable GameTable { get; private set; }


        // Player 2 character selector
        public int SelectedCharacterIndex { get; private set; } = 0;
        #endregion

        #region Constructor
        public Game(int n, IVadaszatDataAccess dataAccess)
        {
            GameTable = new GameTable(n);
            _dataAccess = dataAccess;

        }


        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, GameTable);
        }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            GameTable = await _dataAccess.LoadAsync(path);
            this.Launch();

        }
        #endregion


        #region Public methods

        public static Game SetMap(int mapSize, IVadaszatDataAccess dataAccess)
        {
            //commentelni
            return mapSize switch
            {
                3 => new Game(3, dataAccess),
                5 => new Game(5, dataAccess),
                7 => new Game(7, dataAccess),
                _ => new Game(3, dataAccess),// return null
            };
        }


        // Launch
        public void Launch()
        {
            RefreshPlayerTurn();
            RefreshStepCounter();
        }


        public void StartTurn(Direction dir)
        {
            bool moved = GameTable.Move(dir, SelectedCharacterIndex);

            if (moved)
            {

                SwitchPlayerTurn();
            }
            else
            {
                // Nem történt mozgás, ide lehetne event invoke, amit TestMethodban igy könnyebben tudok ellenőrizni
            }

        }




        // Switch between player1 and player2...
        private void SwitchPlayerTurn()
        {
            // Check if the game is ended and round switch is even needed...

            bool gotWinner = CheckWinner(out Player? winner);

            if (gotWinner && winner != null)
            {
                // Sending signal to the form that we got a winner, and the game needs to stop.
                RefreshStepCounter();
                GotGameWinner(winner, true);
                IsGameEnded = true;
            }
            else
            {
                //currentPlayer = currentPlayer == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
                RefreshPlayerTurn();
                RefreshStepCounter();
                GotGameWinner(winner, false);
            }



        }

        // Player2 switch between 1-4 character...
        public void SwitchSelectedCharacter()
        {
            SelectedCharacterIndex = (SelectedCharacterIndex + 1) % 4; // Cycle through characters
            OnSelectedCharacterChange();


        }

        // Gives back the selected player location of player2...

        public Point GetSelectedPlayerLocation()
        {
            if (GameTable.player2 != null)
            {

                return GameTable.player2.Location(SelectedCharacterIndex);
            }
            return new Point(-1, -1);
        }

        public void Restart()
        {
            GameTable.ReInitialize();
            RefreshPlayerTurn();
            RefreshStepCounter();
        }

        #endregion


        #region Private methods


        private bool IsCaught()
        {
            if (GameTable.player1 == null)
            {
                return false;
            }
            Point p = GameTable.player1.Location();

            int x = p.X;
            int y = p.Y;

            bool up = false;
            bool down = false;
            bool left = false;
            bool right = false;

            if (x - 1 < 0)
            {
                up = true;
            }
            else if (x - 1 >= 0 && x - 1 < GameTable.MapSize)
            {
                if (GameTable.map[x - 1, y] == 2)
                {
                    up = true;
                }


            }

            if (x + 1 >= GameTable.MapSize)
            {
                down = true;
            }
            else if (x + 1 < GameTable.MapSize && x + 1 >= 0)
            {
                if (GameTable.map[x + 1, y] == 2)
                {
                    down = true;
                }
            }

            if (y - 1 < 0)
            {
                left = true;
            }
            else if (y - 1 >= 0 && y - 1 < GameTable.MapSize)
            {
                if (GameTable.map[x, y - 1] == 2)
                {
                    left = true;
                }
            }

            if (y + 1 >= GameTable.MapSize)
            {
                right = true;
            }
            else if (y + 1 < GameTable.MapSize && y + 1 >= 0)
            {
                if (GameTable.map[x, y + 1] == 2)
                {
                    right = true;
                }
            }

            if (up && down && left && right)
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckWinner(out Player? winner)
        {
            winner = null;

            //Point p = player1.Location();

            //int x = p.X;
            //int y = p.Y;

            bool isSurrounded = IsCaught();

            if (GameTable.currentSteps == GameTable.maxSteps && !isSurrounded)
            {
                // Player1 won
                winner = GameTable.player1;
                return true;

            }
            else if (GameTable.currentSteps <= GameTable.maxSteps && isSurrounded)
            {
                winner = GameTable.player2;
                return true;
            }
            else
            {

                return false;
            }



        }
        #endregion


        #region Debug


        /*
        public string PrintMap()
        {
            string currentMap = "";

            for (int i = 0; i < gameTable.MapSize; i++)
            {
                for (int j = 0; j < gameTable.MapSize; j++)
                {
                    currentMap += $"{gameTable.map[i, j]} ";
                }

                currentMap += "\n";
            }


            return currentMap;

        }
        */
        // Player position
        //DEBUG
        /*
        public string getPlayerLocation(int index)
        {
            switch (index)
            {
                case 1:
                    return gameTable.player1.Location().ToString();
                case 2:
                    return gameTable.player2.Locations();

                default:
                    return "";
            }

        }
        */
    }
    #endregion
}
