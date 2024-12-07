using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacToeApp
{
    public partial class MainForm : Form
    {
        private Button[,] gridButtons = new Button[3, 3];
        private bool isXPlayerTurn;
        private RadioButton easyMode;
        private RadioButton hardMode;
        private CheckBox playerStartsCheckbox;
        private Button restartButton;

        public MainForm()
        {
            InitializeComponent();
        }

        private void StartNewGame()
        {
            isXPlayerTurn = playerStartsCheckbox.Checked;

            foreach (var button in gridButtons)
            {
                button.Text = string.Empty;
                button.Enabled = true;
                button.BackColor = SystemColors.Control;
            }

            if (!isXPlayerTurn)
            {
                AIPlay();
            }
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            Button selectedButton = sender as Button;
            if (selectedButton != null && string.IsNullOrEmpty(selectedButton.Text))
            {
                selectedButton.Text = isXPlayerTurn ? "X" : "O";
                selectedButton.Enabled = false;

                if (CheckForWin())
                {
                    MessageBox.Show($"{(isXPlayerTurn ? "X" : "O")} wins!", "Game Over");
                    DisableAllButtons();
                    return;
                }

                if (CheckForDraw())
                {
                    MessageBox.Show("It's a Draw!", "Game Over");
                    return;
                }

                isXPlayerTurn = !isXPlayerTurn;

                if (!isXPlayerTurn)
                {
                    AIPlay();
                }
            }
        }

        private void AIPlay()
        {
            if (hardMode.Checked)
            {
                // Strategic move - try to win or block player
                if (!MakeWinningMove("O"))
                {
                    MakeWinningMove("X"); // Block player
                }
            }
            else
            {
                MakeRandomMove();
            }

            if (CheckForWin())
            {
                MessageBox.Show("O wins!", "Game Over");
                DisableAllButtons();
                return;
            }

            if (CheckForDraw())
            {
                MessageBox.Show("It's a Draw!", "Game Over");
                return;
            }

            isXPlayerTurn = true;
        }

        private bool MakeWinningMove(string playerSymbol)
        {
            // Check for possible winning move
            for (int i = 0; i < 3; i++)
            {
                // Check rows and columns
                if (TryCompleteLine(playerSymbol, gridButtons[i, 0], gridButtons[i, 1], gridButtons[i, 2]))
                    return true;
                if (TryCompleteLine(playerSymbol, gridButtons[0, i], gridButtons[1, i], gridButtons[2, i]))
                    return true;
            }

            // Check diagonals
            if (TryCompleteLine(playerSymbol, gridButtons[0, 0], gridButtons[1, 1], gridButtons[2, 2]))
                return true;
            if (TryCompleteLine(playerSymbol, gridButtons[0, 2], gridButtons[1, 1], gridButtons[2, 0]))
                return true;

            return false;
        }

        private bool TryCompleteLine(string playerSymbol, Button b1, Button b2, Button b3)
        {
            if (b1.Text == playerSymbol && b2.Text == playerSymbol && string.IsNullOrEmpty(b3.Text))
            {
                b3.Text = "O";
                b3.Enabled = false;
                return true;
            }

            if (b1.Text == playerSymbol && string.IsNullOrEmpty(b2.Text) && b3.Text == playerSymbol)
            {
                b2.Text = "O";
                b2.Enabled = false;
                return true;
            }

            if (b2.Text == playerSymbol && b3.Text == playerSymbol && string.IsNullOrEmpty(b1.Text))
            {
                b1.Text = "O";
                b1.Enabled = false;
                return true;
            }

            return false;
        }

        private void MakeRandomMove()
        {
            var availableButtons = gridButtons.Cast<Button>().Where(b => string.IsNullOrEmpty(b.Text)).ToList();
            if (availableButtons.Any())
            {
                Random rng = new Random();
                var buttonToClick = availableButtons[rng.Next(availableButtons.Count)];
                buttonToClick.Text = "O";
                buttonToClick.Enabled = false;
            }
        }

        private bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (gridButtons[i, 0].Text == gridButtons[i, 1].Text && gridButtons[i, 1].Text == gridButtons[i, 2].Text && !string.IsNullOrEmpty(gridButtons[i, 0].Text))
                    return true;
                if (gridButtons[0, i].Text == gridButtons[1, i].Text && gridButtons[1, i].Text == gridButtons[2, i].Text && !string.IsNullOrEmpty(gridButtons[0, i].Text))
                    return true;
            }

            if (gridButtons[0, 0].Text == gridButtons[1, 1].Text && gridButtons[1, 1].Text == gridButtons[2, 2].Text && !string.IsNullOrEmpty(gridButtons[0, 0].Text))
                return true;

            if (gridButtons[0, 2].Text == gridButtons[1, 1].Text && gridButtons[1, 1].Text == gridButtons[2, 0].Text && !string.IsNullOrEmpty(gridButtons[0, 2].Text))
                return true;

            return false;
        }

        private bool CheckForDraw()
        {
            return gridButtons.Cast<Button>().All(b => !string.IsNullOrEmpty(b.Text)) && !CheckForWin();
        }

        private void DisableAllButtons()
        {
            foreach (var button in gridButtons)
            {
                button.Enabled = false;
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
