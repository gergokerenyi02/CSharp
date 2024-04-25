using EVA_ZH_Console.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml;

namespace EVA_ZH_WPF.ViewModel
{
    internal class ViewModel : ViewModelBase
    {
        #region Fields
        private GameModel _model; // modell

        public DelegateCommand? NewGameCommand { get; private set; }
        public DelegateCommand? Set9x9Command { get; private set; }
        public DelegateCommand? Set13x13Command { get; private set; }

        public DelegateCommand? Set17x17Command { get; private set; }
        public DelegateCommand? ExitCommand { get; private set; }

        public ObservableCollection<ViewModelGameField> Fields { get; set; }

        public int tableSize { get; set; }
        public int buttonSize { get; set; }
        public string gametime { get; set; }

        public int gamepoints { get; set; }
        #endregion

        public ViewModel(GameModel model)
        {
            _model = model;
            _model.NewGame += model_NewGame;
            _model.GameAdvance += model_GameAdvance;
            _model.CalculateTime += model_calculateTime;

            NewGameCommand = new DelegateCommand(param => NewGame());
            Set9x9Command = new DelegateCommand(param => Set9x9C());
            Set13x13Command = new DelegateCommand(param => Set13x13C());
            Set17x17Command = new DelegateCommand(param => Set17x17C());
            ExitCommand = new DelegateCommand(param => OnExitGame());

            Fields = new ObservableCollection<ViewModelGameField>();
            tableSize = _model.tableSize;
            buttonSize = 500 / tableSize; //500: the value of gameTable size in view
            gametime = "MM:SS";
            gamepoints = 0;
            OnPropertyChanged(nameof(gametime));
        }


        #region private helper methods
        private void buttonClicked(object? sender, ButtonClickedEventArgs e)
        {
            _model.modelButtonClicked(e.row, e.col);
        }
        #endregion

        #region Model event handlers
        //A modell eseményeinek kezelői (modell által kezdeményezett események)
        //Ezek segítségéel követi le a viewmodell a modell változásait
        private void model_NewGame(object? sender, NewGameEventArgs e)
        {
            if (Fields.Count != 0)
            {
                Fields.Clear();
            }
            tableSize = e.size;
            OnPropertyChanged(nameof(tableSize));
            for (int i = 0; i < e.size; i++) // inicializáljuk a mezőket
            {
                for (int j = 0; j < e.size; j++)
                {
                    ViewModelGameField vmgf = new ViewModelGameField(i, j, 500 / _model.tableSize);
                    vmgf.ButtonClicked += buttonClicked;
                    Fields.Add(vmgf); //500: the value of gameTable size in view
                }
            }
        }
        private void model_GameAdvance(object? sender, GameAdvanceEventArgs e)
        {
            for (int i = 0; i < e.gameTable.GetLength(0); i++)
            {
                for (int j = 0; j < e.gameTable.GetLength(1); j++)
                {
                    if(e.gameTable[i, j].isTarget && !e.gameTable[i,j].isDestroyed)//if (e.gameTable[i, j].isBlack || e.gameTable[i,j].isVisible)
                    {
                        Fields[i * (e.gameTable.GetLength(1) - 1) + i + j].color = new System.Windows.Media.SolidColorBrush(Colors.Black);
                    } else if (e.gameTable[i,j].isTarget && e.gameTable[i,j].isDestroyed)
                    {
                        Fields[i * (e.gameTable.GetLength(1) - 1) + i + j].color = new System.Windows.Media.SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        Fields[i * (e.gameTable.GetLength(1) - 1) + i + j].color = new System.Windows.Media.SolidColorBrush(Colors.White);
                    }
                }
            }

            gamepoints = e.gamePoints;
            OnPropertyChanged(nameof(gamepoints));

        }

        private void model_calculateTime(object? sender, int time)
        {
            gametime = (time / 60) + ":" + (time % 60);
            OnPropertyChanged(nameof(gametime));
        }
        #endregion

        #region private methods (that communicate with the model)
        private void NewGame()
        {
            _model.modelNewGame();
        }
        private void Set9x9C()
        {
            _model.modelSetTable9x9();
            
        }
        private void Set13x13C()
        {
            _model.modelSetTable13x13();
        }

        private void Set17x17C()
        {
            _model.modelSetTable17x17();
        }
        #endregion

        #region events/event methods
        

        public event EventHandler? ExitGame;
        private void OnExitGame()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }
        #endregion

    }
}
