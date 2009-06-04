using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;
using Szpif.Controls.ContentControls;

namespace Szpif
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void tryShow(string name)
        {
            this.disableLabel.Visible = false;
            bool sth = false;
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                if (c.Name == name)
                {
                    c.Visible = true;
                    sth = true;
                }
                else
                    c.Visible = false;
            }
            if (sth == false)
                this.disableLabel.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tryShow("Pracownicy");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tryShow("Zadania");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tryShow("Projekty");
        }
    }
}
