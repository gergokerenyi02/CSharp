using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using Vadaszat;
using Vadaszat.Model;
using Vadaszat.Persistence;

namespace VadászatWPF.ViewModel
{
    public class VadaszatViewModel : ViewModelBase
    {

        private Game _model;

        private IVadaszatDataAccess _dataAccess;

        public DelegateCommand LoadGameCommand { get; private set; }


        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand MoveCommand { get; private set; }

        public DelegateCommand ChangeMapSize { get; private set; }

        public DelegateCommand SwitchSelectedCharacterCommand { get; private set; }

        public ObservableCollection<VadaszatField> Fields { get; set; }

        


        //public bool IsGameEnded { get { return _model.IsGameEnded; } }

        //public int SelectedCharacterIndex { get { return _model.SelectedCharacterIndex; } }

        

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

        
       


        public event EventHandler? LoadGame;
        public event EventHandler<MapSizeChangedEventArgs>? MapSizeChanged;
        public event EventHandler? SwitchCharacter;
        public event EventHandler? SaveGame;
        
        public event EventHandler<MoveEventArgs>? Move;


        public VadaszatViewModel(Game model)
        {
            _model = model;
            _dataAccess = new VadaszatFileDataAccess();
            

            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            ChangeMapSize = new DelegateCommand(param => OnChangeMapSize(Convert.ToInt32(param)));
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            MoveCommand = new DelegateCommand(param => OnMove(param?.ToString() ?? String.Empty));
            SwitchSelectedCharacterCommand = new DelegateCommand(param => OnSwitchCharacter());

            Fields = new ObservableCollection<VadaszatField>();

            for (int i = 0; i < _model.GameTable.MapSize; i++) // inicializáljuk a mezőket
            {
                for (int j = 0; j < _model.GameTable.MapSize; j++)
                {
                    Fields.Add(new VadaszatField
                    {
                        Character = Convert.ToString(_model.GameTable.map[i, j]),
                        
                    });
                }
            }

            StepCounter = _model.GameTable.maxSteps - _model.GameTable.currentSteps;
            CurrentPlayer = _model.GameTable.currentPlayer;



        }


        private void RefreshTable()
        {
            Fields.Clear();

            for (int i = 0; i < _model.GameTable.MapSize; i++)
            {
                for (int j = 0; j < _model.GameTable.MapSize; j++)
                {
                    Fields.Add(new VadaszatField
                    {
                        Character = Convert.ToString(_model.GameTable.map[i, j]),
                       
                    });
                }
            }

            OnPropertyChanged(nameof(StepCounter));
            OnPropertyChanged(nameof(CurrentPlayer));

            OnPropertyChanged(nameof(MapSize));

        }


       
        private void CharacterSelect(object? sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                _model.SwitchSelectedCharacter();
                
            }
            else
            {
                return;
            }
        }


       private void OnChangeMapSize(int selectedMapSize)
       {
            _model = Game.SetMap(selectedMapSize, _dataAccess);
            MapSize = selectedMapSize;
            

            MapSizeChanged?.Invoke(this, new MapSizeChangedEventArgs(_model));
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
        }

    }
}
