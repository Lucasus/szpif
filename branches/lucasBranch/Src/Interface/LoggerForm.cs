using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;
using BusinessLogic.Tests;
using DatabaseLibrary;

namespace Interface
{
    public partial class LoggerForm : Form
    {
        Logger logger;
        
        public LoggerForm()
        {
            InitializeComponent();
            logger = new Logger(new Database());
        }

		private void button1_Click(object sender, EventArgs e)
		{
            ICollection<string> permissions 
			      = logger.LogToSystem(UserNameTextBox.Text, PassWordTextBox.Text);
			if(permissions == null)
			{
                MessageBox.Show("Podałeś zły login i/lub hasło");
			}
			else
			{
                this.Hide();
                Program.UserPanelForm = new UserPanel();
                Program.UserPanelForm.ShowDialog();
                this.Dispose(false);
			}
		}

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
