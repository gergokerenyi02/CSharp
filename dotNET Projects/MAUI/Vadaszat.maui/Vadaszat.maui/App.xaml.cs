using Vadaszat.Persistence;
using Vadaszat.Model;
using Vadaszat.maui.ViewModel;
using Vadaszat.maui.Persistence;

namespace Vadaszat.maui
{
    public partial class App : Application
    {
        /// <summary>
        /// Erre az útvonalra mentjük a félbehagyott játékokat
        /// </summary>
        private const string SuspendedGameSavePath = "SuspendedGame";

        private readonly AppShell _appShell;
        private readonly IVadaszatDataAccess _vadaszatDataAccess;
        private readonly Game _vadaszatGameModel;
        private readonly IStore _vadaszatStore;
        private readonly VadaszatViewModel _vadaszatViewModel;

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();

            _vadaszatStore = new VadaszatStore();
            _vadaszatDataAccess = new VadaszatFileDataAccess(FileSystem.AppDataDirectory);

            _vadaszatGameModel = new Game(3, _vadaszatDataAccess); //Game.SetMap(3, _vadaszatDataAccess);
            _vadaszatViewModel = new VadaszatViewModel(_vadaszatGameModel);

            _appShell = new AppShell(_vadaszatStore, _vadaszatDataAccess, _vadaszatGameModel, _vadaszatViewModel)
            {
                BindingContext = _vadaszatViewModel
            };
            MainPage = _appShell;
        }


        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            // az alkalmazás indításakor
            window.Created += (s, e) =>
            {
            };

            // amikor az alkalmazás fókuszba kerül
            window.Activated += (s, e) =>
            {
                if (!File.Exists(Path.Combine(FileSystem.AppDataDirectory, SuspendedGameSavePath)))
                    return;

                Task.Run(async () =>
                {
                    // betöltjük a felfüggesztett játékot, amennyiben van
                    try
                    {
                        await _vadaszatGameModel.LoadGameAsync(SuspendedGameSavePath);

                        // csak akkor indul az időzítő, ha sikerült betölteni a játékot
                        
                    }
                    catch
                    {
                    }
                });
            };

            // amikor az alkalmazás fókuszt veszt
            window.Deactivated += (s, e) =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        // elmentjük a jelenleg folyó játékot
                        
                        await _vadaszatGameModel.SaveGameAsync(SuspendedGameSavePath);
                    }
                    catch
                    {
                    }
                });
            };

            return window;
        }
    }
}