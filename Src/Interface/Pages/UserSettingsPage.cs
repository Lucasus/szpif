using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;
using BusinessLogic;
using Logic.BusinessLogic;

namespace Interface
{
    public partial class UserSettingsPage : TabPage
    {
        public UserSettingsPage(string text) : base(text)
        {
            InitializeComponent();
            this.Controls.Add(passwordChangeBox);
            this.Controls.Add(passwordChangeButton);
            this.Controls.Add(passwordStrenght);
            
            passwordChangeLabel.Location = new Point(10,20);
            passwordChangeBox.Location = new Point(passwordChangeLabel.Location.X + passwordChangeLabel.Width + 10, passwordChangeLabel.Location.Y);
			passwordChangeButton.Location = new Point(passwordChangeBox.Location.X + passwordChangeBox.Width + 10, passwordChangeBox.Location.Y);
			passwordStrenght.Location = new Point(passwordChangeButton.Location.X + passwordChangeButton.Width + 10, passwordChangeButton.Location.Y);
			Context c = Context.CurrentContext;
			PasswordChecker pc = new PasswordChecker(c.UserLogin);
			Strenght str = pc.getPasswordStrenght(c.UserPassword);
			passwordStrenght.Text = str.ToString();
        }

		private void passwordChangeButton_Click(object sender, EventArgs e)
		{
			Context c = Context.CurrentContext;
			
			PasswordChecker pc = new PasswordChecker(c.UserLogin);
			passwordStrenght.Text = pc.getPasswordStrenght(passwordChangeBox.Text).ToString();
			
			SzpifDatabase.DataBase.ChangePassword(c.UserLogin,c.UserPassword,passwordChangeBox.Text);
			c.UserPassword = passwordChangeBox.Text;
			passwordChangeBox.Text = "";
		}

    }
}
