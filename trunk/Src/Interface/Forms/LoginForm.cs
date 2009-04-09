using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;
using Logic;

namespace Interface
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

		private void loginButton_Click(object sender, EventArgs e)
		{
			string username = UserNameTextBox.Text;
			string password = PassWordTextBox.Text;
            Program.PermissionManager.tryLogin(username, password);
		}

        private void anulujButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
