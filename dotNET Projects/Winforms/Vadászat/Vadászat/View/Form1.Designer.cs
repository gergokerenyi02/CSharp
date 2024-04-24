namespace Vadászat
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mapSButton3 = new Button();
            mapSButton5 = new Button();
            mapSButton7 = new Button();
            mapSizeText = new Label();
            startButton = new Button();
            mapMatrix = new Label();
            label1 = new Label();
            label2 = new Label();
            player1Position = new Label();
            player2Text = new Label();
            player2Position = new Label();
            currentTurnBtn = new Label();
            playerturnText = new Label();
            selectedCharLabel = new Label();
            winnerLabel = new Label();
            gotWinnerLabel = new Label();
            remainingStepsLabel = new Label();
            restartBtn = new Button();
            menuStrip1 = new MenuStrip();
            FileMenuStrip = new ToolStripMenuItem();
            FileSaveGameStrip = new ToolStripMenuItem();
            loadGameToolStripMenuItem = new ToolStripMenuItem();
            _saveFileDialog = new SaveFileDialog();
            _openFileDialog = new OpenFileDialog();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // mapSButton3
            // 
            mapSButton3.Location = new Point(588, 73);
            mapSButton3.Name = "mapSButton3";
            mapSButton3.Size = new Size(75, 23);
            mapSButton3.TabIndex = 2;
            mapSButton3.Text = "3";
            mapSButton3.UseVisualStyleBackColor = true;
            mapSButton3.Click += mapSButton_Click;
            // 
            // mapSButton5
            // 
            mapSButton5.Location = new Point(588, 102);
            mapSButton5.Name = "mapSButton5";
            mapSButton5.Size = new Size(75, 23);
            mapSButton5.TabIndex = 3;
            mapSButton5.Text = "5";
            mapSButton5.UseVisualStyleBackColor = true;
            mapSButton5.Click += mapSButton_Click;
            // 
            // mapSButton7
            // 
            mapSButton7.Location = new Point(588, 131);
            mapSButton7.Name = "mapSButton7";
            mapSButton7.Size = new Size(75, 23);
            mapSButton7.TabIndex = 4;
            mapSButton7.Text = "7";
            mapSButton7.UseVisualStyleBackColor = true;
            mapSButton7.Click += mapSButton_Click;
            // 
            // mapSizeText
            // 
            mapSizeText.AutoSize = true;
            mapSizeText.Location = new Point(588, 40);
            mapSizeText.Name = "mapSizeText";
            mapSizeText.Size = new Size(75, 15);
            mapSizeText.TabIndex = 5;
            mapSizeText.Text = "Set map size:";
            // 
            // startButton
            // 
            startButton.Location = new Point(588, 187);
            startButton.Name = "startButton";
            startButton.Size = new Size(133, 54);
            startButton.TabIndex = 6;
            startButton.Text = "Start Game";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // mapMatrix
            // 
            mapMatrix.AutoSize = true;
            mapMatrix.Location = new Point(557, 446);
            mapMatrix.Name = "mapMatrix";
            mapMatrix.Size = new Size(0, 15);
            mapMatrix.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(557, 419);
            label1.Name = "label1";
            label1.Size = new Size(65, 15);
            label1.TabIndex = 8;
            label1.Text = "GameMap:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 419);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 9;
            label2.Text = "Player1";
            // 
            // player1Position
            // 
            player1Position.AutoSize = true;
            player1Position.Location = new Point(35, 446);
            player1Position.Name = "player1Position";
            player1Position.Size = new Size(0, 15);
            player1Position.TabIndex = 10;
            // 
            // player2Text
            // 
            player2Text.AutoSize = true;
            player2Text.Location = new Point(159, 419);
            player2Text.Name = "player2Text";
            player2Text.Size = new Size(45, 15);
            player2Text.TabIndex = 12;
            player2Text.Text = "Player2";
            // 
            // player2Position
            // 
            player2Position.AutoSize = true;
            player2Position.Location = new Point(159, 446);
            player2Position.Name = "player2Position";
            player2Position.Size = new Size(0, 15);
            player2Position.TabIndex = 13;
            // 
            // currentTurnBtn
            // 
            currentTurnBtn.AutoSize = true;
            currentTurnBtn.Location = new Point(36, 573);
            currentTurnBtn.Name = "currentTurnBtn";
            currentTurnBtn.Size = new Size(75, 15);
            currentTurnBtn.TabIndex = 14;
            currentTurnBtn.Text = "Current turn:";
            // 
            // playerturnText
            // 
            playerturnText.AutoSize = true;
            playerturnText.Location = new Point(119, 574);
            playerturnText.Name = "playerturnText";
            playerturnText.Size = new Size(12, 15);
            playerturnText.TabIndex = 15;
            playerturnText.Text = "-";
            // 
            // selectedCharLabel
            // 
            selectedCharLabel.AutoSize = true;
            selectedCharLabel.Location = new Point(363, 569);
            selectedCharLabel.Name = "selectedCharLabel";
            selectedCharLabel.Size = new Size(89, 15);
            selectedCharLabel.TabIndex = 16;
            selectedCharLabel.Text = "Selected player:";
            selectedCharLabel.UseMnemonic = false;
            // 
            // winnerLabel
            // 
            winnerLabel.AutoSize = true;
            winnerLabel.Location = new Point(690, 573);
            winnerLabel.Name = "winnerLabel";
            winnerLabel.Size = new Size(48, 15);
            winnerLabel.TabIndex = 17;
            winnerLabel.Text = "Winner:";
            // 
            // gotWinnerLabel
            // 
            gotWinnerLabel.AutoSize = true;
            gotWinnerLabel.Location = new Point(550, 573);
            gotWinnerLabel.Name = "gotWinnerLabel";
            gotWinnerLabel.Size = new Size(72, 15);
            gotWinnerLabel.TabIndex = 18;
            gotWinnerLabel.Text = "GotWinner?:";
            // 
            // remainingStepsLabel
            // 
            remainingStepsLabel.AutoSize = true;
            remainingStepsLabel.Location = new Point(832, 575);
            remainingStepsLabel.Name = "remainingStepsLabel";
            remainingStepsLabel.Size = new Size(97, 15);
            remainingStepsLabel.TabIndex = 19;
            remainingStepsLabel.Text = "Remaining steps:";
            // 
            // restartBtn
            // 
            restartBtn.Location = new Point(586, 248);
            restartBtn.Name = "restartBtn";
            restartBtn.Size = new Size(135, 57);
            restartBtn.TabIndex = 20;
            restartBtn.Text = "Restart Game";
            restartBtn.UseVisualStyleBackColor = true;
            restartBtn.Click += restartBtn_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { FileMenuStrip });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(976, 24);
            menuStrip1.TabIndex = 21;
            menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuStrip
            // 
            FileMenuStrip.DropDownItems.AddRange(new ToolStripItem[] { FileSaveGameStrip, loadGameToolStripMenuItem });
            FileMenuStrip.Name = "FileMenuStrip";
            FileMenuStrip.Size = new Size(37, 20);
            FileMenuStrip.Text = "File";
            // 
            // FileSaveGameStrip
            // 
            FileSaveGameStrip.Name = "FileSaveGameStrip";
            FileSaveGameStrip.Size = new Size(180, 22);
            FileSaveGameStrip.Text = "Save Game...";
            FileSaveGameStrip.Click += FileSaveGameStrip_Click;
            // 
            // loadGameToolStripMenuItem
            // 
            loadGameToolStripMenuItem.Name = "loadGameToolStripMenuItem";
            loadGameToolStripMenuItem.Size = new Size(180, 22);
            loadGameToolStripMenuItem.Text = "Load Game...";
            loadGameToolStripMenuItem.Click += loadGameToolStripMenuItem_Click;
            // 
            // _openFileDialog
            // 
            _openFileDialog.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 610);
            Controls.Add(restartBtn);
            Controls.Add(remainingStepsLabel);
            Controls.Add(gotWinnerLabel);
            Controls.Add(winnerLabel);
            Controls.Add(selectedCharLabel);
            Controls.Add(playerturnText);
            Controls.Add(currentTurnBtn);
            Controls.Add(player2Position);
            Controls.Add(player2Text);
            Controls.Add(player1Position);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(mapMatrix);
            Controls.Add(startButton);
            Controls.Add(mapSizeText);
            Controls.Add(mapSButton7);
            Controls.Add(mapSButton5);
            Controls.Add(mapSButton3);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button mapSButton3;
        private Button mapSButton5;
        private Button mapSButton7;
        private Label mapSizeText;
        private Button startButton;
        private Label mapMatrix;
        private Label label1;
        private Label label2;
        private Label player1Position;
        private Label player2Text;
        private Label player2Position;
        private Label currentTurnBtn;
        private Label playerturnText;
        private Label selectedCharLabel;
        private Label winnerLabel;
        private Label gotWinnerLabel;
        private Label remainingStepsLabel;
        private Button restartBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem FileMenuStrip;
        private ToolStripMenuItem FileSaveGameStrip;
        private SaveFileDialog _saveFileDialog;
        private ToolStripMenuItem loadGameToolStripMenuItem;
        private OpenFileDialog _openFileDialog;
    }
}