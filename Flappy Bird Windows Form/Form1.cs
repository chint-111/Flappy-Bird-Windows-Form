using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flappy_Bird_Windows_Form
{
    public partial class Form1 : Form
    {
        private int pipeSpeed = 6;
        private int gravity = 10;
        private int score = 0;
        private int pipeGap = 150;

        public string GameMode { get; private set; } // Game mode passed from MainMenu
        public string EnvironmentStyle { get; private set; } // Environment style passed from MainMenu
        private Button btnRestart; // Restart button

        public Form1(string mode, string environment)
        {
            InitializeComponent();
            GameMode = mode;
            EnvironmentStyle = environment;

            SetGameMode(GameMode);
            SetEnvironmentStyle(EnvironmentStyle);
            InitializeRestartButton();
            // Attach KeyDown and KeyUp event handlers
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gamekeyisdown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gamekeyisup);

            // Enable KeyPreview to handle key events even when other controls are focused
            this.KeyPreview = true;

        }

        private void SetEnvironmentStyle(string style)
        {
            switch (style)
            {
                case "Day":
                    this.BackColor = Color.CornflowerBlue;
                    break;
                case "Night":
                    this.BackColor = Color.DarkSlateBlue;
                    break;
                case "Space":
                    this.BackColor = Color.Black;
                    break;
                default:
                    this.BackColor = Color.CornflowerBlue;
                    break;
            }
        }
        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) // Example: Make the bird jump when pressing Space
            {
                gravity = -10; // Adjust gravity or upward movement logic as needed.
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) // Example: Restore gravity when releasing Space
            {
                gravity = 10; // Adjust gravity or downward movement logic as needed.
            }
        }
        private void InitializeRestartButton()
        {
            btnRestart = new Button
            {
                Location = new Point(350, 300),
                Name = "btnRestart",
                Size = new Size(100, 50),
                Text = "Restart",
                Visible = false
            };
            btnRestart.Click += BtnRestart_Click;
            this.Controls.Add(btnRestart);
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            score = 0;
            pipeSpeed = 6;
            gravity = 10;
            pipeGap = 150;

            ResetPipes();
            btnRestart.Visible = false;
            scoreText.Text = "Score: " + score;
            gameTimer.Start();
        }

        private void SetGameMode(string mode)
        {
            switch (mode)
            {
                case "Normal":
                    pipeSpeed = 6;
                    pipeGap = 150;
                    break;
                case "Hard":
                    pipeSpeed = 8;
                    pipeGap = 120;
                    break;
                case "Extreme":
                    pipeSpeed = 10;
                    pipeGap = 90;
                    break;
            }
            if (pipeGap < 80)
            {
                pipeGap = 80;
            }
        }

        private void ResetPipes()
        {
            Random rand = new Random();
            int pipeHeight = rand.Next(pipeGap, this.ClientSize.Height - pipeGap - ground.Height);

            pipeBottom.Left = this.ClientSize.Width;
            pipeBottom.Top = pipeHeight;

            pipeTop.Left = this.ClientSize.Width;
            pipeTop.Top = pipeHeight - pipeGap;
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;  // Adjust bird's position based on gravity
            pipeBottom.Left -= pipeSpeed;  // Move pipes left
            pipeTop.Left -= pipeSpeed;     // Move top pipe left

            // Check if pipes are off-screen and reset
            if (pipeBottom.Left < -pipeBottom.Width)
            {
                ResetPipes();
                score++;
                scoreText.Text = "Score: " + score; // Update score
            }

            // Collision detection
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||                
                flappyBird.Bounds.IntersectsWith(ground.Bounds))
            {
                endGame();
            }

            // Check if bird flies out of bounds
            if (flappyBird.Top < -25 || flappyBird.Bottom > ClientSize.Height)
            {
                endGame();
            }
            scoreText.Text = "Score: " + score; // Display updated score
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over!";
            btnRestart.Visible = true;

            MessageBox.Show($"Game over! Your final score is {score}", "Game Over", MessageBoxButtons.OK);

            if (MessageBox.Show("Return to Main Menu?", "Game Over", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();
            }
        }
    }
}