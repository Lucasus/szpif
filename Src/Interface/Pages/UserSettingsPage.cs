using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;

namespace Interface
{
    public partial class UserSettingsPage : TabPage
    {
        IntegratedView view;
        DataGridView employeesForUser;
        List<Control> valueBoxes;
        BindManager bindManager;
        ContentManager contManager;
        public UserSettingsPage(string text) : base(text)
        {
            contManager = Program.Context.ContentManager;
            bindManager = Program.Context.ViewToGridManager;

            InitializeComponent();

            // initializing dataGridView
            employeesForUser = new DataGridView();
            employeesForUser.Name = "EmployeesForUser";
            employeesForUser.Visible = false;
            this.Controls.Add(employeesForUser);
            
            // binding
            view = bindManager.bindToView(employeesForUser);
            valueBoxes = contManager.generateContent(this, employeesForUser, view);
        }

		private void passwordChangeButton_Click(object sender, EventArgs e)
		{
           /* if (Program.Context.UserPassword == passwordOldPassword.Text)
			{
                PasswordChecker pc = new PasswordChecker(Program.Context.UserLogin);
				passwordStrenght.Text = pc.getPasswordStrenght(passwordChangeBox.Text).ToString();
                SzpifDatabase.DataBase.ChangePassword(Program.Context.UserLogin, passwordOldPassword.Text, passwordChangeBox.Text, priviliges);
                Program.Context.UserPassword = passwordChangeBox.Text;
				MessageBox.Show("Zmiana hasła zakończona pomyślnie");
			}
			else
			{
				MessageBox.Show("Zmiana hasła zakończona niepomyślnie");
			}
			passwordChangeBox.Text = "";
			passwordOldPassword.Text = "";*/
		}

		private void eMailChangeButton_Click(object sender, EventArgs e)
		{
			/*Context c = Program.Context;
			SzpifDatabase.DataBase.ChangeEMail(c.UserLogin, c.UserPassword, eMailChangeBox.Text,priviliges);
			eMailChangeBox.Text = "";*/
		}

        private void UserSettingsPage_Enter(object sender, EventArgs e)
        {
            contManager.getDataFromGrid(valueBoxes, employeesForUser, view, 0); 

        }

    }
}
