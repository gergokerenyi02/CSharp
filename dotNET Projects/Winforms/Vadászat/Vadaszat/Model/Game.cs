using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Vadaszat.Persistence;
using System.Drawing;
using static Vadaszat.Model.Game;

namespace Vadaszat.Model
{

    #region VadaszatEventArgs
    
    public class NewTurnEventArgs : EventArgs
    {
        public PlayerTurn NewPlayer { get; set; }
        public int selectedChar { get; set; }

    }

    public class SwitchedCharacterArgs : EventArgs
    {
        public int selectedCharacter { get; set; }

    }

    public class StepCounterArgs : EventArgs
    {
        public int newStepCounter { get; set; }
    }

    public class CheckEndGameArgs : EventArgs
    {
        public bool isEndGame { get; set; }
        public Player? newWinner { get; set; }

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
                GotWinner?.Invoke(this, new CheckEndGameArgs { isEndGame = true, newWinner = winner });
            } else
            {
                GotWinner?.Invoke(this, new CheckEndGameArgs { isEndGame = false, newWinner = winner });
            }
        }
        private void OnSelectedCharacterChange()
        {
            SwitchedCharachter?.Invoke(this, new SwitchedCharacterArgs { selectedCharacter = selectedCharacterIndex });
        }

        private void RefreshPlayerTurn()
        {
            NewTurn?.Invoke(this, new NewTurnEventArgs { NewPlayer = (PlayerTurn)gameTable.currentPlayer, selectedChar = selectedCharacterIndex });
        }

        private void RefreshStepCounter()
        {
            OnStep?.Invoke(this, new StepCounterArgs { newStepCounter = gameTable.maxSteps - gameTable.currentSteps });
        }

        #endregion
        #region Fields

        IVadaszatDataAccess _dataAccess;


        private static Game? _game3x3;
        private static Game? _game5x5;
        private static Game? _game7x7;

        public bool isGameEnded { get; private set; } = false;

        public GameTable gameTable {get; private set; }

        /*
        public enum PlayerTurn
        {
            Player1,
            Player2
        }
        */




        // Player 2 character selector
        public int selectedCharacterIndex { get; private set; } = 0;
        #endregion

        #region Constructor
        public Game(int n, IVadaszatDataAccess dataAccess)
        {
            gameTable = new GameTable(n);
            _dataAccess = dataAccess;

        }


        public async Task SaveGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, gameTable);
        }

        public async Task LoadGameAsync(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            gameTable = await _dataAccess.LoadAsync(path);
            this.Launch();
            
        }
        #endregion


        #region Public methods

        public static Game SetMap(int mapSize, IVadaszatDataAccess dataAccess)
        {

            switch (mapSize)
            {
                case 3:
                    if (_game3x3 == null || (_game3x3.isGameEnded))
                    {
                        _game3x3 = new Game(3, dataAccess);
                    }
                    return _game3x3;

                case 5:
                    if (_game5x5 == null || _game5x5.isGameEnded)
                    {
                        _game5x5 = new Game(5, dataAccess);
                    }
                    return _game5x5;

                case 7:
                    if (_game7x7 == null || _game7x7.isGameEnded)
                    {
                        _game7x7 = new Game(7, dataAccess);
                    }
                    return _game7x7;

                default:
                    return new Game(3, dataAccess); // return null
            }


        }


        // Launch
        public void Launch()
        {
            RefreshPlayerTurn();
            RefreshStepCounter();
        }


        public void StartTurn(Direction dir)
        {
            bool moved = gameTable.Move(dir, selectedCharacterIndex);

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

            bool gotWinner = checkWinner(out Player? winner);

            if (gotWinner && winner != null)
            {
                // Sending signal to the form that we got a winner, and the game needs to stop.
                RefreshStepCounter();
                GotGameWinner(winner, true);
                isGameEnded = true;
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
            selectedCharacterIndex = (selectedCharacterIndex + 1) % 4; // Cycle through characters
            OnSelectedCharacterChange();


        }

        // Gives back the selected player location of player2...

        public Point getSelectedPlayerLocation()
        {
            if(gameTable.player2 != null)
            {
            
                return gameTable.player2.Location(selectedCharacterIndex);
            }
            return new Point(-1, -1);
        }

        public void Restart()
        {
            gameTable.ReInitialize();
            RefreshPlayerTurn();
            RefreshStepCounter();
        }

        #endregion


        #region Private methods


        private bool isCaught()
        {
            if(gameTable.player1 == null)
            {
                return false;
            }
            Point p = gameTable.player1.Location();

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
            else if (x - 1 >= 0 && x - 1 < gameTable.MapSize)
            {
                if (gameTable.map[x - 1, y] == 2)
                {
                    up = true;
                }


            }

            if (x + 1 >= gameTable.MapSize)
            {
                down = true;
            }
            else if (x + 1 < gameTable.MapSize && x + 1 >= 0)
            {
                if (gameTable.map[x + 1, y] == 2)
                {
                    down = true;
                }
            }

            if (y - 1 < 0)
            {
                left = true;
            }
            else if (y - 1 >= 0 && y - 1 < gameTable.MapSize)
            {
                if (gameTable.map[x, y - 1] == 2)
                {
                    left = true;
                }
            }

            if (y + 1 >= gameTable.MapSize)
            {
                right = true;
            }
            else if (y + 1 < gameTable.MapSize && y + 1 >= 0)
            {
                if (gameTable.map[x, y + 1] == 2)
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

        private bool checkWinner(out Player? winner)
        {
            winner = null;

            //Point p = player1.Location();

            //int x = p.X;
            //int y = p.Y;

            bool isSurrounded = isCaught();

            if (gameTable.currentSteps == gameTable.maxSteps && !isSurrounded)
            {
                // Player1 won
                winner = gameTable.player1;
                return true;

            }
            else if (gameTable.currentSteps <= gameTable.maxSteps && isSurrounded)
            {
                winner = gameTable.player2;
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