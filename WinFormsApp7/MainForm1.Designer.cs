using System.Windows.Forms;

namespace TicTacToeApp
{
    partial class MainForm
    {
        private void InitializeComponent()
        {
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Text = "Tic Tac Toe";

            
            gridButtons = new Button[3, 3];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    gridButtons[row, col] = new Button
                    {
                        Name = $"btn_{row}_{col}", 
                        Size = new System.Drawing.Size(100, 100),
                        Location = new System.Drawing.Point(100 * col, 100 * row),
                        Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold),
                        BackColor = System.Drawing.Color.LightGray 
                    };
                    gridButtons[row, col].Click += GridButton_Click;
                    this.Controls.Add(gridButtons[row, col]);
                }
            }

            
            easyMode = new RadioButton
            {
                Text = "Easy",
                Location = new System.Drawing.Point(10, 320),
                Checked = true,
                AutoSize = true 
            };
            this.Controls.Add(easyMode);

            
            hardMode = new RadioButton
            {
                Text = "Hard",
                Location = new System.Drawing.Point(10, 350),
                AutoSize = true 
            };
            this.Controls.Add(hardMode);

            
            playerStartsCheckbox = new CheckBox
            {
                Text = "X goes first",
                Location = new System.Drawing.Point(10, 380),
                Checked = true,
                AutoSize = true 
            };
            this.Controls.Add(playerStartsCheckbox);

            
            restartButton = new Button
            {
                Text = "New Game",
                Location = new System.Drawing.Point(150, 350),
                Size = new System.Drawing.Size(100, 50),
                BackColor = System.Drawing.Color.LightBlue 
            };
            restartButton.Click += RestartButton_Click;
            this.Controls.Add(restartButton);

            
            StartNewGame();
        }
    }
}
