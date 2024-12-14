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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();

            button1.Text = "Normal";
            button2.Text = "Hard";
            button3.Text = "Extreme";

            label1.Text = "Flappy Bird";
            label1.Font = new Font("RetroFont", 24, FontStyle.Bold);
            label1.ForeColor = Color.Yellow;
            label1.BackColor = Color.Red;
            label1.TextAlign = ContentAlignment.TopCenter;

            label2.Text = "Get Ready!";
            label2.Font = new Font("Arial", 24, FontStyle.Bold);
            label2.ForeColor = Color.Green;
            label2.TextAlign = ContentAlignment.MiddleCenter;

            label3.Text = "Tap to Play";
            label3.Font = new Font("Arial", 18, FontStyle.Bold);
            label3.ForeColor = Color.Blue;
            label3.TextAlign = ContentAlignment.MiddleCenter;

            // Start blink effect for "Tap to Play"
            Timer blinkTimer = new Timer();
            blinkTimer.Interval = 500; // 500ms for blink
            blinkTimer.Tick += (s, e) =>
            {
                label3.Visible = !label3.Visible; // Toggle visibility
            };
            blinkTimer.Start();

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PictureBox4 clicked!");
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {
            // Initialize any dynamic content or setup logic here.
            MessageBox.Show("Main Menu Loaded!");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StartGame("Normal");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartGame("Hard");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartGame("Extreme");
        }

        private void StartGame(string mode)
        {
            Form1 gameForm = new Form1(mode);
            gameForm.Show();
            this.Hide();
        }
    }
}
