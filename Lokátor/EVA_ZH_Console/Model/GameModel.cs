using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EVA_ZH_Console.Model
{
    public class GameModel
    {
        #region Fields
        private GameField[,] _gameTable = null!;
        private HashSet<(int, int)> targetLocations = new HashSet<(int, int)>();


        private int _tableSize = 9;

        private int gameTime = 0;
        private int gamePoints = 0;
        #endregion

        #region Getters/Setters
        public int tableSize
        {
            get
            {
                return _tableSize;
            }
            set
            {
                _tableSize = value;
            }
        }
        #endregion

        public GameModel()
        {

        }

        #region menu Methods
        public void modelNewGame()
        {
            createGametable();
            initializeTargets();

            //gets prev tableSize from the prev game
            onNewGame(_tableSize);
            onGameAdvance(_gameTable);
            
        }
        public void modelSetTable9x9()
        {
            tableSize = 9;
            createGametable();
            initializeTargets();
            onNewGame(_tableSize);
            onGameAdvance(_gameTable);
        }
        public void modelSetTable13x13()
        {
            tableSize = 13;
            createGametable();
            initializeTargets();
            onNewGame(_tableSize);
            onGameAdvance(_gameTable);
        }

        public void modelSetTable17x17()
        {
            tableSize = 17;
            createGametable();
            initializeTargets();
            onNewGame(_tableSize);
            onGameAdvance(_gameTable);
        }
        #endregion

        #region public Methods
        public void modelButtonClicked(int row, int col)
        {
            // Kattintásnál robbantson 3x3 mezőben
            gamePoints++;
            var target = _gameTable[row, col];

            if (row+3 <= _tableSize && col+3 <= _tableSize)
            {
                for (int i = row; i < row + 3; i++)
                {
                    for (int j = col; j < col + 3; j++)
                    {
                        if (_gameTable[i, j].isTarget && !_gameTable[i,j].isDestroyed)
                        {

                            _gameTable[i, j].isDestroyed = true;
                            targetLocations.Remove((i, j));
                        }
                        
                    }
                }

                
            }





            onGameAdvance(_gameTable);
            if (checkGameOver())
            {
                onGameOver(true);
            }
        }

        public void modelCalculateGameTime()
        {
            gameTime += 1;
            onCalculateTime();
        }
        #endregion

        #region private Methods         
        //main game logic
        private void createGametable()
        {

            gameTime = 0;
            gamePoints = 0;
            _gameTable = new GameField[_tableSize, _tableSize];
            for (int i = 0; i < _tableSize; i++)
            {
                for (int j = 0; j < _tableSize; j++)
                {
                    _gameTable[i, j] = new GameField(i, j);
                }
            }

            



        }

        private void initializeTargets()
        {
            Random random = new Random();

            while(targetLocations.Count != (2 * _tableSize))
            {
                int i = random.Next(0, _tableSize);
                int j = random.Next(0, _tableSize);

                targetLocations.Add((i, j));
            }

            foreach (var location in targetLocations)
            {
                _gameTable[location.Item1, location.Item2].isTarget = true;
                _gameTable[location.Item1, location.Item2].isDestroyed = false;

            }




        }
        private bool checkGameOver()
        {
            if(targetLocations.Count == 0)
            {
                return true;
            }
            return false;
            /*
            for (int i = 0; i < _tableSize; i++)
            {
                for (int j = 0; j < _tableSize; j++)
                {
                    if (!_gameTable[i, j].isBlack)
                    {
                        return false;
                    }
                }
            }
            return true;
            */
        }
        #endregion

        #region events/event methods
        public event EventHandler<NewGameEventArgs>? NewGame;
        public event EventHandler<GameAdvanceEventArgs>? GameAdvance;
        public event EventHandler<GameOverEventArgs>? GameOver;
        public event EventHandler<int>? CalculateTime;

        private void onNewGame(int size)
        {
            NewGame?.Invoke(this, new NewGameEventArgs(size));
        }
        private void onGameAdvance(GameField[,] gameTable)
        {
            GameAdvance?.Invoke(this, new GameAdvanceEventArgs(gameTable, gamePoints));
        }
        private void onGameOver(bool isWon)
        {
            GameOver?.Invoke(this, new GameOverEventArgs(isWon, gamePoints));
        }
        private void onCalculateTime()
        {
            CalculateTime?.Invoke(this, gameTime);
        }
        #endregion
    }
}
