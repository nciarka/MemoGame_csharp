using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MemoGame
{
    public partial class Form1 : Form
    {
        Label nc_72791_firstClicked = null;

        Label nc_72791_secondClicked = null;

        bool gameStopped = false;

        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        private void AssignIconsToSquares()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {

                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            if (gameStopped)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            { 
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (nc_72791_firstClicked == null)
                {
                    nc_72791_firstClicked = clickedLabel;
                    nc_72791_firstClicked.ForeColor = Color.Black;

                    return;
                }

                nc_72791_secondClicked = clickedLabel;
                nc_72791_secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (nc_72791_firstClicked.Text == nc_72791_secondClicked.Text)
                {
                    nc_72791_firstClicked = null;
                    nc_72791_secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            nc_72791_firstClicked.ForeColor = nc_72791_firstClicked.BackColor;
            nc_72791_secondClicked.ForeColor = nc_72791_secondClicked.BackColor;

            nc_72791_firstClicked = null;
            nc_72791_secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (string.IsNullOrEmpty(iconLabel.Text))
                        return;

                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Wygrałeś!");
            Close();
        }
        private void startbutton_Click(object sender, EventArgs e)
        {
            if (gameStopped)
            {
                gameStopped = false;
                MessageBox.Show("Gra wznowiona.");
                return;
            }

            icons = new List<string>()
            {
                "!", "!", "N", "N", ",", ",", "k", "k",
                "b", "b", "v", "v", "w", "w", "z", "z"
            };
        
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.ForeColor = iconLabel.BackColor;
                    iconLabel.Text = "";
                }
            }

            AssignIconsToSquares();
            nc_72791_firstClicked = null;
            nc_72791_secondClicked = null;
        }

        private void stopButton_Click(object sender, EventArgs e)
        {

            timer1.Stop();

            if (nc_72791_firstClicked != null && nc_72791_secondClicked == null)
            {
                nc_72791_firstClicked.ForeColor = nc_72791_firstClicked.BackColor;
                nc_72791_firstClicked = null;
            }

            if (nc_72791_firstClicked != null && nc_72791_secondClicked != null)
            {
                nc_72791_firstClicked.ForeColor = nc_72791_firstClicked.BackColor;
                nc_72791_secondClicked.ForeColor = nc_72791_secondClicked.BackColor;
                nc_72791_firstClicked = null;
                nc_72791_secondClicked = null;
            }

            gameStopped = true;

            MessageBox.Show("Gra została zatrzymana. Aby wznowić naciśnij Start.");
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            timer1.Stop();

            gameStopped = false;

            icons = new List<string>()
            {
                "!", "!", "N", "N", ",", ",", "k", "k",
                "b", "b", "v", "v", "w", "w", "z", "z"
            };

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    iconLabel.Text = "";
                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }

            AssignIconsToSquares();

            nc_72791_firstClicked = null;
            nc_72791_secondClicked = null;

            MessageBox.Show("Gra została zresetowana.");
        }

        private void Show(object sender, EventArgs e)
        {
            MessageBox.Show("Naciśnij Start, aby rozpocząć grę.");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
