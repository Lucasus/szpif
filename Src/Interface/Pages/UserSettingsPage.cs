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

namespace Interface
{
    public partial class UserSettingsPage : TabPage
    {
        public UserSettingsPage(string text) : base(text)
        {
            InitializeComponent();
            this.Controls.Add(passwordChangeBox);
            this.Controls.Add(passwordChangeButton);
            
            passwordChangeLabel.Location = new Point(10,20);
            passwordChangeBox.Location = new Point(passwordChangeLabel.Location.X + passwordChangeLabel.Width + 10, 20);
            passwordChangeButton.Location = new Point(passwordChangeBox.Location.X + passwordChangeBox.Width + 10, 20);
        }

		private void passwordChangeButton_Click(object sender, EventArgs e)
		{
			Context c = Context.CurrentContext;
			SzpifDatabase.DataBase.ChangePassword(c.CurrentUserLogin,c.CurrentUserPassword,passwordChangeBox.Text);
			c.CurrentUserPassword = passwordChangeBox.Text;
			passwordChangeBox.Text = "";
		}

    }
}
