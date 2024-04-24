using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Vadaszat.Model;
using Vadaszat.Persistence;
using VadászatWPF.ViewModel;

namespace VadászatWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Game _model = null!;
        private VadaszatViewModel _viewModel = null!;
        private MainWindow _view = null!;

        private IVadaszatDataAccess? _dataAccess;

        
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }

        private void App_Startup(object? sender, StartupEventArgs e)
        {
            _dataAccess = new VadaszatFileDataAccess();
            _model = Game.SetMap(3, _dataAccess);
            _model.GotWinner += model_GotWinner;

            //viewmodel
            _viewModel = new VadaszatViewModel(_model);

            _viewModel.LoadGame += ViewModel_LoadGame;
            _viewModel.SaveGame += ViewModel_SaveGame;
            _viewModel.Move += Form1_KeyDown;
            _viewModel.MapSizeChanged += ViewModel_MapSizeChanged;
            _viewModel.SwitchCharacter += ViewModel_SwitchCharacter;

            //view
            _view = new MainWindow();
            _view.DataContext = _viewModel;
            _view.Show();

        }

        private async void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); // dialógablak
                //saveFileDialog.Title = "Vadászat tábla betöltése";
                //saveFileDialog.Filter = "Vadászat tábla|*.stl";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // játéktábla mentése
                        await _model.SaveGameAsync(saveFileDialog.FileName);
                    }
                    catch (VadaszatDataException)
                    {
                        MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("A fájl mentése sikertelen!", "Vadászat", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // dialógusablak
                //openFileDialog.Title = "Vadászat tábla betöltése";
                //openFileDialog.Filter = "Vadászat tábla|*.stl";
                if (openFileDialog.ShowDialog() == true)
                {
                    // játék betöltése
                    await _model.LoadGameAsync(openFileDialog.FileName);
                    _viewModel.MapSize = _model.GameTable.MapSize;
                    
                                        
                }
            }
            catch (VadaszatDataException)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Vadászat", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void model_GotWinner(object? sender, CheckEndGameArgs e)
        {
            if(e.IsEndGame) { 
                if (MessageBox.Show($"Játék vége! A nyertes: {e.NewWinner} Inditsunk uj jatekot?", "Vadászat", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                {
                    _model.Restart();
                }
                else
                {
                    _view.Close();
                }
            } else
            { return; }
        }

        private void ViewModel_SwitchCharacter(object? sender, EventArgs e)
        {
            _model.SwitchSelectedCharacter();
        }

        private void ViewModel_MapSizeChanged(object? sender, MapSizeChangedEventArgs e)
        {
           
            _model = e.Game;
            _model.GotWinner += model_GotWinner;

        }

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

            _model?.StartTurn(dir);

            

        }
    }
}
