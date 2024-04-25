using System.Configuration;
using System.Data;
using System.Windows;
using EVA_ZH_WPF.View;
using EVA_ZH_Console.Model;
using EVA_ZH_WPF.ViewModel;
using System.Windows.Threading;

namespace EVA_ZH_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        private GameModel _model = null!;
        private ViewModel.ViewModel _viewModel = null!;
        private MainWindow _view = null!;

        private DispatcherTimer _timeCalculatorTimer = null!;
        #endregion

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            // modell létrehozása
            _model = new GameModel();
            _model.GameOver += model_GameOver;
            _model.NewGame += model_newGame;

            // nézemodell létrehozása
            _viewModel = new ViewModel.ViewModel(_model);
            _viewModel.ExitGame += viewModel_ExitGame;

            // nézet létrehozása
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();

            //időzítő létrehozása
            _timeCalculatorTimer = new DispatcherTimer();
            _timeCalculatorTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _timeCalculatorTimer.Tick += CalculateGameTime;
        }

        #region private methods
        private void CalculateGameTime(object? sender, EventArgs e)
        {
            _model.modelCalculateGameTime();
        }
        #endregion

        #region Model event handlers
        //A modell eseményeinek kezelői (modell által kezdeményezett események)
        //Csak azok szükségesek, amelyekhez kell wpf/kornyezet (pl.: savefile, massageBox)

        private void model_newGame(object? sender, NewGameEventArgs e)
        {
            _timeCalculatorTimer.Start();
        }
        private void model_GameOver(object? sender, GameOverEventArgs e)
        {
            if (e.isWon)
            {
                MessageBoxResult result = MessageBox.Show("YOU WON! Your points: " + e.gamePoints + " Do you want to play a new game?", "Game Over", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _model.modelNewGame();
                }
                else
                {
                    // Close the current window
                    _view.Close();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("YOU LOST! Do you want to play a new game?", "Game Over", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _model.modelNewGame();
                }
                else
                {
                    _view.Close();
                }
            }
        }
        #endregion

        #region ViewModel event handlers
        //A view command bindingei által kiváltott események kezelői (felhasználó által kezdeményezett események)
        //Csak azok szükségesek, amelyekhez kell wpf/környezet (pl.: view.Close();)
        private void viewModel_ExitGame(object? sender, EventArgs e)
        {
            _view.Close();
        }
        #endregion
    }

}
