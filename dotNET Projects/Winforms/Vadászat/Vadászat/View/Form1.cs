using System.Reflection;
using System.Windows.Forms;
using Vadaszat.Model;
using Vadaszat.Persistence;

namespace Vadászat
{
    public partial class Form1 : Form
    {

        #region Fields

        private IVadaszatDataAccess _dataAccess;
        private Game? game;
        private PictureBox? pictureBox1;
        private bool isPlayer2Turn;


        #endregion

        #region Constructor


        public Form1()
        {
            InitializeComponent();
            restartBtn.Enabled = false;
            _dataAccess = new VadaszatFileDataAccess();

        }
        #endregion


        #region Game event handlers

        private void mapSButton_Click(object sender, EventArgs e)
        {
            Button? btn = sender as Button;

            if (btn == null)
            {
                return;
            }

            int mapSize = int.Parse(btn.Text);

            // Create a map with the given size...
            game = Game.SetMap(mapSize, _dataAccess); // It returns an instance of a (3x3 ; 5x5 ; 7x7) game.

            startButton.Enabled = true;
            restartBtn.Enabled = false;
            // If picturebox is not initialized, initialize one...
            if (pictureBox1 == null)
            {
                InitializePictureBox();
            }

            // Update the map view with each click...
            // Will be added, that if user starts the game, buttons will be disabled.
            UpdatePictureBox();
            
            //DEBUG
            //mapMatrix.Text = game.PrintMap();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            Direction dir;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    dir = Direction.Left;
                    break;
                case Keys.Right:
                    dir = Direction.Right;
                    break;
                case Keys.Up:
                    dir = Direction.Up;
                    break;
                case Keys.Down:
                    dir = Direction.Down;
                    break;
                default:
                    return;
            }

            game?.StartTurn(dir);

            // Change player turn and update picturebox

            // Debug
            /*
            player1Position.Text = $"{game.getPlayerLocation(1)}";
            player2Position.Text = $"{game.getPlayerLocation(2)}";
            mapMatrix.Text = game.PrintMap();
            */

        }

        private void RefreshStepCounter(object? sender, StepCounterArgs e)
        {
            UpdatePictureBox();
            remainingStepsLabel.Text = $"Player2 remaining steps: {e.newStepCounter}";
        }

        private void RefreshSelectedCharacter(object? sender, SwitchedCharacterArgs e)
        {
            selectedCharLabel.Text = $"Selected character: {e.selectedCharacter + 1}";

        }

        private void OnNewTurn(object? sender, NewTurnEventArgs e)
        {

            var currentPlayer = e.NewPlayer;
            int player2SelectedChar = e.selectedChar + 1;

            playerturnText.Text = currentPlayer.ToString();

            switch (currentPlayer)
            {
                case PlayerTurn.Player1:
                    this.KeyDown -= CharacterSelect;
                    isPlayer2Turn = false;
                    break;
                case PlayerTurn.Player2:
                    this.KeyDown += CharacterSelect;
                    isPlayer2Turn = true;
                    selectedCharLabel.Text = $"Selected character: {player2SelectedChar}";

                    break;
                default:
                    break;
            }
            UpdatePictureBox();


        }
        private void OnGameWinner(object? sender, CheckEndGameArgs e)
        {
            gotWinnerLabel.Text = $"GotWinner?: {e.isEndGame}";
            if (e.isEndGame)
            {
                restartBtn.Enabled = true;
                EnableMapSelectionButtons();

                winnerLabel.Text = $"Winner: {e.newWinner}";
                string message = "The game is over, and the winner is: " + e.newWinner;
                string caption = "Game Over";
                MessageBoxButtons buttons = MessageBoxButtons.OKCancel;


                DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                {
                    //game.Restart();

                    //UpdatePictureBox();

                }

            }

        }

        #endregion


        #region Menu event handlers
        private void startButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1 == null)
            {
                return;
            }
            else
            {
                /*EZT KI KELL EXTRACTOLNI*/


                if(game != null)
                {
                    game.GotWinner += OnGameWinner;
                    game.NewTurn += OnNewTurn;
                    game.SwitchedCharachter += RefreshSelectedCharacter;
                    game.OnStep += RefreshStepCounter;
                    game.Launch();
                    disableAllButton();
                }

                
               


                //DEBUG
                /*
                player1Position.Text = $"{game.getPlayerLocation(1)}";
                player2Position.Text = $"{game.getPlayerLocation(2)}";
                */
            }
        }

        private void disableAllButton()
        {
            startButton.Enabled = false;
            restartBtn.Enabled = false;
            mapSButton3.Enabled = false;
            mapSButton5.Enabled = false;
            mapSButton7.Enabled = false;
        }

        private void EnableMapSelectionButtons()
        {
            mapSButton3.Enabled = true;
            mapSButton5.Enabled = true;
            mapSButton7.Enabled = true;
        }


        #endregion


        #region Picturebox event handlers
        private void InitializePictureBox()
        {
            pictureBox1 = new PictureBox();
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Location = new Point(0, 20);
            pictureBox1.Size = new Size(400, 400);

            // Add PictureBox to the form
            this.Controls.Add(pictureBox1);

            // Event handler for Paint event of PictureBox
            pictureBox1.Paint += DrawMap; // Pálya frissitése.
            this.KeyPreview = true;

        }

        private void UpdatePictureBox()
        {
            // Refresh the PictureBox to trigger the paint event.
            // The pain event is subscribed to the DrawMap method. 
            pictureBox1?.Refresh();
        }

        private void DrawMap(object? sender, PaintEventArgs e)
        {
            // Use the Graphics object from PaintEventArgs to draw the map
            Graphics g = e.Graphics;



            // Define colors for player indicators (0, 1, 2)
            Color[] playerColors = new Color[] { Color.Transparent, Color.Red, Color.Blue };

            if(pictureBox1 == null || game == null)
            {
                return;
            }

            int cellSize = pictureBox1.Width / game.gameTable.MapSize; // Adjust the size as needed
            //Brush selectedBrush = new SolidBrush(Color.Green);

            for (int i = 0; i < game.gameTable.MapSize; i++)
            {
                for (int j = 0; j < game.gameTable.MapSize; j++)
                {
                    // Draw rectangles based on the matrix values
                    int playerIndicator = game.gameTable.map[i, j]; // 2
                    Brush brush = new SolidBrush(playerColors[playerIndicator]);

                    g.FillRectangle(brush, j * cellSize, i * cellSize, cellSize, cellSize);

                    brush.Dispose();

                    Point selectedPlayer = game.getSelectedPlayerLocation();

                    if (i == selectedPlayer.X && j == selectedPlayer.Y && isPlayer2Turn)
                    {
                        using (Pen greenPen = new Pen(Color.Green, 10))
                        {
                            g.DrawRectangle(greenPen, j * cellSize, i * cellSize, cellSize, cellSize);

                        }


                        //g.FillRectangle(selectedBrush, j * cellSize, i * cellSize, cellSize, cellSize);

                    }

                }
            }
        }

        private void CharacterSelect(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                game?.SwitchSelectedCharacter();
                UpdatePictureBox();
            }
            else
            {
                // Nem tabot nyom akkor nem vált karaktert
                return;
            }
        }
        #endregion



        private void restartBtn_Click(object sender, EventArgs e)
        {
            game?.Restart();
            disableAllButton();
            
        }

        private async void FileSaveGameStrip_Click(object sender, EventArgs e)
        {
            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if(game != null)
                    {
                        await game.SaveGameAsync(_saveFileDialog.FileName);
                    }
                    else
                    {

                        MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Nincs elkezdett játék amit lementhetnél!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (VadaszatDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                
            }
        }

        private async void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            if (_openFileDialog.ShowDialog() == DialogResult.OK) // ha kiválasztottunk egy fájlt
            {
                try
                {
                    if(game == null)
                    {
                        game = Game.SetMap(3, _dataAccess);
                        if (pictureBox1 == null)
                        {
                            InitializePictureBox();
                        }

                        // Update the map view with each click...
                        // Will be added, that if user starts the game, buttons will be disabled.
                        UpdatePictureBox();
                        
                        //DEBUG
                        //mapMatrix.Text = game.PrintMap();
                    }
                    // játék betöltése
                    
                    await game.LoadGameAsync(_openFileDialog.FileName);

                    //_menuFileSaveGame.Enabled = true;
                }
                catch (VadaszatDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //_model.NewGame();
                    //_menuFileSaveGame.Enabled = true;
                }

                UpdatePictureBox();
                if(game != null)
                { 
                    game.GotWinner -= OnGameWinner;
                    game.NewTurn -= OnNewTurn;
                    game.SwitchedCharachter -= RefreshSelectedCharacter;
                    game.OnStep -= RefreshStepCounter;
                    startButton.Enabled = true;
                    restartBtn.Enabled = false;
                }

            }

            
        }
    }

}