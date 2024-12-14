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
        private ComboBox comboBoxEnvironment; // Declare comboBoxEnvironment
        private Button button4; // Declare button4

        public MainMenu()
        {
            InitializeComponent();

            // Initialize button4
            button4 = new Button
            {
                Name = "button4",
                Text = "Select Environment!!!",
                Location = new Point(100, 200), // Adjust as needed
                Size = new Size(150, 40)
            };
            this.Controls.Add(button4);

            // Initialize comboBoxEnvironment
            comboBoxEnvironment = new ComboBox
            {
                Name = "comboBoxEnvironment",
                Location = new Point(100, 150), // Adjust as needed
                Size = new Size(150, 30)
            };
            this.Controls.Add(comboBoxEnvironment);

            comboBoxEnvironment.Items.Add("Day");
            comboBoxEnvironment.Items.Add("Night");
            comboBoxEnvironment.Items.Add("Space");
            comboBoxEnvironment.SelectedIndex = 0; // Default to "Day"

            // Attach event handler
            comboBoxEnvironment.SelectedIndexChanged += ComboBoxEnvironment_SelectedIndexChanged;

            // Other buttons
            button1.Text = "Normal";
            button2.Text = "Hard";
            button3.Text = "Extreme";

            label1.Text = "Flappy Bird";
            label1.Font = new Font("RetroFont", 24, FontStyle.Bold);
            label1.ForeColor = Color.Yellow;
            label1.BackColor = Color.Red;
            label1.TextAlign = ContentAlignment.MiddleCenter;

            label2.Text = "Get Ready!";
            label2.Font = new Font("Arial", 24, FontStyle.Bold);
            label2.ForeColor = Color.Green;
            label2.TextAlign = ContentAlignment.MiddleCenter;

            label3.Text = "Tap to Play";
            label3.Font = new Font("Arial", 18, FontStyle.Bold);
            label3.ForeColor = Color.Blue;
            label3.TextAlign = ContentAlignment.MiddleCenter;

            // Blink effect for label3
            Timer blinkTimer = new Timer();
            blinkTimer.Interval = 500; // 500ms
            blinkTimer.Tick += (s, e) => { label3.Visible = !label3.Visible; };
            blinkTimer.Start();
        }

        private void ComboBoxEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEnvironment = comboBoxEnvironment.SelectedItem.ToString();
            MessageBox.Show("Selected Environment: " + selectedEnvironment);
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
            string environmentStyle = comboBoxEnvironment.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(environmentStyle))
            {
                MessageBox.Show("Please select an environment!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form1 gameForm = new Form1(mode, environmentStyle);
            gameForm.Show();
            this.Hide();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PictureBox4 was clicked!");
            // Add your custom logic here if needed.
        }
        private void MainMenu_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Main Menu Loaded!");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
