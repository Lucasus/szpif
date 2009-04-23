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
            PermissionManager pm = new PermissionManager(Program.Context.Database);
            ICollection<string> permissions = pm.getUserPermissions(username, password);
            if(permissions != null)
            {
				Program.Context.UserLogin = username;
				Program.Context.UserPassword = password;
				Program.Context.UserRoles = permissions;
				Program.Context.FormManager.switchForm("LoginForm", "MainForm");
            }
            else
            {
				Program.Context.FormManager.showMessageBox("Podałeś zły login i/lub hasło");
            }
		}

        private void anulujButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
