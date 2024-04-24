
using Vadaszat.maui.View;
using Vadaszat.maui.ViewModel;
using Vadaszat.Model;
using Vadaszat.Persistence;

namespace Vadaszat.maui
{
    public partial class AppShell : Shell
    {
        #region Fields

        private IVadaszatDataAccess _vadaszatDataAccess;
        private Game _vadaszatGameModel;
        private readonly VadaszatViewModel _vadaszatViewModel;

        

        private readonly IStore _store;
        private readonly StoredGameBrowserModel _storedGameBrowserModel;
        private readonly StoredGameBrowserViewModel _storedGameBrowserViewModel;

        #endregion

        #region Application methods

        public AppShell(IStore vadaszatStore, IVadaszatDataAccess vadaszatDataAccess, Game vadaszatGameModel, VadaszatViewModel vadaszatViewModel)
        {
            InitializeComponent();

            // játék összeállítása
            _store = vadaszatStore;
            _vadaszatDataAccess = vadaszatDataAccess;
            _vadaszatGameModel = vadaszatGameModel;
            _vadaszatViewModel = vadaszatViewModel;

 

            _vadaszatGameModel.GotWinner += VadaszatGameModel_GameOver;

            //_vadaszatViewModel.NewGame += VadaszatViewModel_NewGame;
            _vadaszatViewModel.LoadGame += VadaszatViewModel_LoadGame;
            _vadaszatViewModel.SaveGame += VadaszatViewModel_SaveGame;
            _vadaszatViewModel.ExitGame += VadaszatViewModel_ExitGame;

            _vadaszatViewModel.Move += Form1_KeyDown;
            _vadaszatViewModel.MapSizeChanged += ViewModel_MapSizeChanged;
            _vadaszatViewModel.SwitchCharacter += ViewModel_SwitchCharacter;

            // a játékmentések kezelésének összeállítása
            _storedGameBrowserModel = new StoredGameBrowserModel(_store);
            _storedGameBrowserViewModel = new StoredGameBrowserViewModel(_storedGameBrowserModel);
            _storedGameBrowserViewModel.GameLoading += StoredGameBrowserViewModel_GameLoading;
            _storedGameBrowserViewModel.GameSaving += StoredGameBrowserViewModel_GameSaving;
        }

        #endregion

        #region Model event handlers

        /// <summary>
        ///     Játék végének eseménykezelője.
        /// </summary>
        private async void VadaszatGameModel_GameOver(object? sender, CheckEndGameArgs e)
        {
            
            if(e.IsEndGame)
            {
                await DisplayAlert("Vadászat",
                    "Játék vége! A nyertes: " + e.NewWinner,
                    "OK");
            }
        }

        #endregion

        private void ViewModel_SwitchCharacter(object? sender, EventArgs e)
        {
            _vadaszatGameModel.SwitchSelectedCharacter();
        }

        private void ViewModel_MapSizeChanged(object? sender, MapSizeChangedEventArgs e)
        {

            _vadaszatGameModel = e.Game;
            _vadaszatDataAccess = e.DataAccess;
            _vadaszatGameModel.GotWinner -= VadaszatGameModel_GameOver;
            _vadaszatGameModel.GotWinner += VadaszatGameModel_GameOver;

        }
        #region ViewModel event handlers
        private void Form1_KeyDown(object? sender, MoveEventArgs e)
        {

            Direction dir;

            switch (e.Direction)
            {

                case Direction.Left:
                    dir = Direction.Left;
                    break;
                case Direction.Right:
                    dir = Direction.Right;
                    break;
                case Direction.Up:
                    dir = Direction.Up;
                    break;
                case Direction.Down:
                    dir = Direction.Down;
                    break;
                default:
                    return;
            }

            _vadaszatGameModel?.StartTurn(dir);



        }


        /// <summary>
        ///     Új játék indításának eseménykezelője.
        /// </summary>
        private void VadaszatViewModel_NewGame(object? sender, MapSizeChangedEventArgs e)
        {
            //_vadaszatGameModel.SetMap(e.);

        }

        /// <summary>
        ///     Játék betöltésének eseménykezelője.
        /// </summary>
        private async void VadaszatViewModel_LoadGame(object? sender, EventArgs e)
        {
            await _storedGameBrowserModel.UpdateAsync(); // frissítjük a tárolt játékok listáját
            await Navigation.PushAsync(new LoadGamePage
            {
                BindingContext = _storedGameBrowserViewModel
            }); // átnavigálunk a lapra

            

        }

        /// <summary>
        ///     Játék mentésének eseménykezelője.
        /// </summary>
        private async void VadaszatViewModel_SaveGame(object? sender, EventArgs e)
        {
            await _storedGameBrowserModel.UpdateAsync(); // frissítjük a tárolt játékok listáját
            await Navigation.PushAsync(new SaveGamePage
            {
                BindingContext = _storedGameBrowserViewModel
            }); // átnavigálunk a lapra
        }

        private async void VadaszatViewModel_ExitGame(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage
            {
                BindingContext = _vadaszatViewModel
            }); // átnavigálunk a beállítások lapra
        }


        /// <summary>
        ///     Betöltés végrehajtásának eseménykezelője.
        /// </summary>
        private async void StoredGameBrowserViewModel_GameLoading(object? sender, StoredGameEventArgs e)
        {
            await Navigation.PopAsync(); // visszanavigálunk

            // betöltjük az elmentett játékot, amennyiben van
            try
            {
                await _vadaszatGameModel.LoadGameAsync(e.Name);

                // sikeres betöltés
                await Navigation.PopAsync(); // visszanavigálunk a játék táblára
                await DisplayAlert("Vadászat játék", "Sikeres betöltés.", "OK");

                _vadaszatViewModel.ReInitalize();
                _vadaszatGameModel.GotWinner -= VadaszatGameModel_GameOver;
                _vadaszatGameModel.GotWinner += VadaszatGameModel_GameOver;

            }
            catch
            {
                await DisplayAlert("Vadászat játék", "Sikertelen betöltés.", "OK");
            }
        }

        /// <summary>
        ///     Mentés végrehajtásának eseménykezelője.
        /// </summary>
        private async void StoredGameBrowserViewModel_GameSaving(object? sender, StoredGameEventArgs e)
        {
            await Navigation.PopAsync(); // visszanavigálunk

            try
            {
                // elmentjük a játékot
                await _vadaszatGameModel.SaveGameAsync(e.Name);
                await DisplayAlert("Vadászat játék", "Sikeres mentés.", "OK");
            }
            catch
            {
                await DisplayAlert("Vadászat játék", "Sikertelen mentés.", "OK");
            }
        }

        #endregion

    }
}