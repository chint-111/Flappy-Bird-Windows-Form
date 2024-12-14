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
        int pipeSpeed = 6;
        int gravity = 10;
        int score = 0;
        int pipeGap = 150; // Default gap size for pipes

        public string GameMode { get; private set; } // Game mode passed from MainMenu

        private Button btnRestart; // Declare the restart button

        public Form1(string mode)
        {
            InitializeComponent();
            GameMode = mode;
            SetGameMode(GameMode);
            InitializeRestartButton(); // Initialize the restart button
        }

        private void InitializeRestartButton()
        {
            this.btnRestart = new System.Windows.Forms.Button(); // Initialize the button
            this.SuspendLayout();

            // 
            // btnRestart
            // 
            this.btnRestart.Location = new System.Drawing.Point(350, 300); // Example position
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(100, 50); // Size of the button
            this.btnRestart.TabIndex = 0;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Visible = false; // Initially hidden
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click); // Button click event

            // Add the button to the form's controls
            this.Controls.Add(this.btnRestart);

            this.ResumeLayout(false);
        }

        private void SetGameMode(string mode)
        {
            // Adjust settings based on the selected game mode
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
        }
        private void CreatePipe()
        {
            int pipeHeight = new Random().Next(100, this.ClientSize.Height - 100);  // Vị trí ngẫu nhiên cho ống
            pipeBottom.Top = pipeHeight;
            pipeTop.Top = pipeHeight - pipeGap;

            pipeBottom.Left = this.ClientSize.Width;  // Đặt ống vào phía bên phải
            pipeTop.Left = this.ClientSize.Width;
        }
        private void SetPipeDesign(string mode)
        {
            switch (mode)
            {
                case "Normal":
                    pipeBottom.BackColor = Color.Green;
                    pipeTop.BackColor = Color.Green;
                    break;
                case "Hard":
                    pipeBottom.BackColor = Color.DarkGreen;
                    pipeTop.BackColor = Color.DarkGreen;
                    break;
                case "Extreme":
                    pipeBottom.BackColor = Color.Orange;
                    pipeTop.BackColor = Color.Orange;
                    // Thêm hiệu ứng phát sáng hoặc hoạt hình cho ống
                    break;
            }
        }
        private void UpdateScore()
        {
            score++;
            scoreText.Text = "Score: " + score;
        }
        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = -8;
            }
        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 10;
            }
        }






        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity;  // Move the bird up/down based on gravity
            pipeBottom.Left -= pipeSpeed;  // Move pipes left
            pipeTop.Left -= pipeSpeed;     // Move top pipe left

            // Create coin and check for collision

            // Update score display
            scoreText.Text = "Score: " + score;

            // Reset pipes when they move off-screen
            if (pipeBottom.Left < -pipeBottom.Width)
            {
                ResetPipes();  // Reposition pipes
                score++;  // Increase score when passing pipes
            }
            if (pipeTop.Left < -pipeTop.Width)
            {
                ResetPipes();  // Reposition pipes
                score++;  // Increase score when passing pipes
            }

            // End the game if the bird hits pipes or the ground
            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds))
            {
                endGame();
            }

            // Update game difficulty based on score milestones
            UpdateDifficulty();
        }

        private void ResetPipes()
        {
            // Reset pipe positions randomly
            pipeBottom.Left = this.ClientSize.Width;
            pipeBottom.Top = new Random().Next(pipeGap, this.ClientSize.Height - ground.Height);

            pipeTop.Left = this.ClientSize.Width;
            pipeTop.Top = pipeBottom.Top - pipeGap;
        }

        private void UpdateDifficulty()
        {
            // Increase difficulty based on score milestones
            if (score == 10)
            {
                pipeSpeed += 2;  // Increase pipe speed
            }
            else if (score == 20)
            {
                pipeGap -= 20;  // Decrease pipe gap
            }
            else if (score == 30)
            {
                pipeTop.Top += new Random().Next(-10, 10);  // Slight variation in pipe height
            }
            else if (score == 50)
            {
                this.BackColor = Color.LightBlue;  // Change background color for added challenge
            }
        }

        private void endGame()
        {
            gameTimer.Stop();
            scoreText.Text += " Game Over!";
            btnRestart.Visible = true; // Show the restart button

            // Ask the player if they want to return to the Main Menu
            DialogResult result = MessageBox.Show("Do you want to return to the Main Menu?", "Game Over", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Close the current game form
                MainMenu mainMenu = new MainMenu(); // Create an instance of the MainMenu form
                mainMenu.Show(); // Show the Main Menu
            }
            else
            {
                // Handle case if the player chooses "No" (e.g., exit or stay on the game over screen)
                Application.Exit(); // Exit the application (optional)
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            // Reset game variables
            score = 0;
            pipeSpeed = 6;
            gravity = 10;
            pipeGap = 150;

            // Reset bird and pipes positions
            flappyBird.Top = this.ClientSize.Height / 2;
            pipeBottom.Left = this.ClientSize.Width;
            pipeTop.Left = this.ClientSize.Width;
            pipeBottom.Top = new Random().Next(pipeGap, this.ClientSize.Height - ground.Height);
            pipeTop.Top = pipeBottom.Top - pipeGap;

            // Hide restart button and start the game again
            btnRestart.Visible = false;
            scoreText.Text = "Score: " + score;
            gameTimer.Start();
        }
    }
}