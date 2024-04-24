using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vadaszat.Model;
using Vadaszat.Persistence;
using Microsoft.Maui.Controls;

namespace Vadaszat.maui.ViewModel
{
    public class VadaszatViewModel : ViewModelBase
    {

        private Game _model;

        private IVadaszatDataAccess _dataAccess;

        public DelegateCommand LoadGameCommand { get; private set; }

        public DelegateCommand ExitCommand { get; private set; }


        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand MoveCommand { get; private set; }

        public DelegateCommand ChangeMapSize { get; private set; }

        public DelegateCommand SwitchSelectedCharacterCommand { get; private set; }

        public ObservableCollection<VadaszatField> Fields { get; set; }


   


        private int mapSize;
        public int MapSize
        {
            get { return mapSize; }
            set
            {
                mapSize = value;
                RefreshTable();
                OnPropertyChanged(nameof(MapSize));
            }
        }

        /// <summary>
        /// Segédproperty a tábla méretezéséhez
        /// </summary>
        public RowDefinitionCollection GameTableRows
        {
            get => new RowDefinitionCollection(Enumerable.Repeat(new RowDefinition(GridLength.Star), MapSize).ToArray());
        }


        /// <summary>
        /// Segédproperty a tábla méretezéséhez
        /// </summary>
        public ColumnDefinitionCollection GameTableColumns
        {
            get => new ColumnDefinitionCollection(Enumerable.Repeat(new ColumnDefinition(GridLength.Star), MapSize).ToArray());
        }

        private int stepCounter;

        public int StepCounter
        {
            get { return (stepCounter); } //_model.gameTable.maxSteps - _model.gameTable.currentSteps
            private set
            {
                stepCounter = value;
                OnPropertyChanged(nameof(StepCounter));
            }
        }


        private PlayerTurn currentPlayer;

        public PlayerTurn CurrentPlayer
        {
            get { return currentPlayer; } //_model.gameTable.currentPlayer
            private set
            {
                currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
            }
        }





        public event EventHandler<MapSizeChangedEventArgs>? MapSizeChanged;
        public event EventHandler? SwitchCharacter;

        public event EventHandler? NewGame;
        public event EventHandler? LoadGame;
        public event EventHandler? SaveGame;
        public event EventHandler? ExitGame;

        public event EventHandler<MoveEventArgs>? Move;


        public VadaszatViewModel(Game model)
        {
            _model = model;
            _dataAccess = new VadaszatFileDataAccess(FileSystem.AppDataDirectory);



            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            ChangeMapSize = new DelegateCommand(param => OnChangeMapSize(Convert.ToInt32(param)));
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            ExitCommand = new DelegateCommand(param => OnExitGame());
            MoveCommand = new DelegateCommand(param => OnMove(param?.ToString() ?? String.Empty));
            SwitchSelectedCharacterCommand = new DelegateCommand(param => OnSwitchCharacter());

            Fields = new ObservableCollection<VadaszatField>();

            MapSize = 3;

            for (int i = 0; i < _model.GameTable.MapSize; i++) // inicializáljuk a mezőket
            {
                for (int j = 0; j < _model.GameTable.MapSize; j++)
                {
                    Fields.Add(new VadaszatField
                    {
                        Character = Convert.ToString(_model.GameTable.map[i, j]),
                        BackgroundColor = (_model.GameTable.map[i, j] == 1) ? Colors.Red :
                      (_model.GameTable.map[i, j] == 2) ? Colors.Blue :
                      Colors.White,
                        X = i, Y = j

                    });
                }
            }

            StepCounter = _model.GameTable.maxSteps - _model.GameTable.currentSteps;
            CurrentPlayer = _model.GameTable.currentPlayer;



        }


        private void RefreshTable()
        {
            Fields.Clear();

            

            var selectedChar = _model.GetSelectedPlayerLocation();
            int X = selectedChar.X;
            int Y = selectedChar.Y;

            for (int i = 0; i < _model.GameTable.MapSize; i++)
            {
                for (int j = 0; j < _model.GameTable.MapSize; j++)
                {
                    if(X == i && Y == j && _model.GameTable.currentPlayer == PlayerTurn.Player2)
                    {
                        Fields.Add(new VadaszatField
                        {
                            Character = Convert.ToString(_model.GameTable.map[i, j]),
                            BackgroundColor = (_model.GameTable.map[i, j] == 1) ? Colors.Red :
                      (_model.GameTable.map[i, j] == 2) ? Colors.Green :
                      Colors.White,
                            X = i,
                            Y = j

                        });
                    } else
                    {
                        Fields.Add(new VadaszatField
                        {
                            Character = Convert.ToString(_model.GameTable.map[i, j]),
                            BackgroundColor = (_model.GameTable.map[i, j] == 1) ? Colors.Red :
                      (_model.GameTable.map[i, j] == 2) ? Colors.Blue :
                      Colors.White,
                            X = i,
                            Y = j

                        });
                    }

                    
                }
            }

            

            OnPropertyChanged(nameof(StepCounter));
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(GameTableRows));
            OnPropertyChanged(nameof(GameTableColumns));
            OnPropertyChanged(nameof(MapSize));

        }



        private void OnChangeMapSize(int selectedMapSize)
        {
            //_dataAccess = new VadaszatFileDataAccess();
            _model = new Game(selectedMapSize, _dataAccess);//Game.SetMap(selectedMapSize, _dataAccess);
            MapSize = selectedMapSize;


            MapSizeChanged?.Invoke(this, new MapSizeChangedEventArgs(_model, _dataAccess));
            OnPropertyChanged(nameof(GameTableRows));
            OnPropertyChanged(nameof(GameTableColumns));
            OnPropertyChanged(nameof(MapSize));
        }


        public void ReInitalize()
        {
            MapSize = _model.GameTable.MapSize;
            StepCounter = _model.GameTable.maxSteps - _model.GameTable.currentSteps;
            CurrentPlayer = _model.GameTable.currentPlayer;

            OnPropertyChanged(nameof(StepCounter));
            OnPropertyChanged(nameof(CurrentPlayer));
            OnPropertyChanged(nameof(GameTableRows));
            OnPropertyChanged(nameof(GameTableColumns));
            OnPropertyChanged(nameof(MapSize));
        }

        private void OnLoadGame()
        {
            // Update properties
            LoadGame?.Invoke(this, EventArgs.Empty);

            



        }
        /// <summary>
        /// Játék mentése eseménykiváltása.
        /// </summary>
        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnMove(string direction)
        {
            Move?.Invoke(this, new MoveEventArgs(direction));

            StepCounter = _model.GameTable.maxSteps - _model.GameTable.currentSteps;
            CurrentPlayer = _model.GameTable.currentPlayer;
            RefreshTable();
        }

        private void OnSwitchCharacter()
        {
            SwitchCharacter?.Invoke(this, EventArgs.Empty);
            
            RefreshTable();
        }

        private void OnExitGame()
        {
            ExitGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnNewGame()
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }
    }
}
