using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;
using Logic;

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
            InitializeComponent();
            employeesForUser = new DataGridView();
            employeesForUser.Name = "EmployeesForUser";
            employeesForUser.AllowUserToAddRows = false;
            employeesForUser.AllowUserToDeleteRows = false;
            employeesForUser.AllowUserToResizeColumns = false;
            employeesForUser.AllowUserToResizeRows = false;
            employeesForUser.RowHeadersVisible = false;
            ((System.ComponentModel.ISupportInitialize)(employeesForUser)).EndInit();



            contManager = Program.Context.ContentManager;
            bindManager = Program.Context.ViewToGridManager;
            
            view            = bindManager.bindToView(employeesForUser);
            employeesForUser.Update();
            employeesForUser.Refresh();
            this.Controls.Add(employeesForUser);
            employeesForUser.Visible = false;
            valueBoxes      = contManager.generateContent(this, employeesForUser, view);

          //  
            
            
            
            /*  this.Controls.Remove(passwordChangeLabel);
			
              #region passwordGroup
              this.Controls.Add(passwordGroup);
              passwordGroup.Size = new Size(620,100);
            
              passwordGroup.Controls.Add(passwordChangeLabel);
              passwordGroup.Controls.Add(passwordChangeBox);
              passwordGroup.Controls.Add(passwordOldPassword);
              passwordGroup.Controls.Add(passwordChangeButton);
              passwordGroup.Controls.Add(passwordStrenghtText);
              passwordGroup.Controls.Add(passwordStrenght);
            
              passwordChangeLabel.Location = new Point(10,30);
              passwordOldPassword.Location = new Point(passwordChangeLabel.Location.X + passwordChangeLabel.Width + 10, passwordChangeLabel.Location.Y);
              passwordChangeBox.Location = new Point(passwordOldPassword.Location.X + passwordOldPassword.Width + 10, passwordOldPassword.Location.Y);
              passwordChangeButton.Location = new Point(passwordChangeBox.Location.X + passwordChangeBox.Width + 10, passwordChangeBox.Location.Y);
			
              passwordStrenghtText.Location = new Point(10, passwordChangeLabel.Location.Y + 30);
              passwordStrenght.Location = new Point(passwordStrenghtText.Location.X + passwordStrenghtText.Width + 10, passwordStrenghtText.Location.Y);
              PasswordChecker pc = new PasswordChecker(Program.Context.UserLogin);
              Strenght str = pc.getPasswordStrenght(Program.Context.UserPassword);
              passwordStrenght.Text = str.ToString();
              #endregion
			
              #region eMailGroup
              this.Controls.Add(eMailGroup);
              eMailGroup.Size = new Size(620,100);
              eMailGroup.Location = new Point(0,passwordGroup.Location.Y + passwordGroup.Height + 10);
			
              eMailGroup.Controls.Add(eMailChangeLabel);
              eMailGroup.Controls.Add(eMailChangeBox);
              eMailGroup.Controls.Add(eMailChangeButton);
			
              eMailChangeLabel.Location = new Point(10, 30);
              eMailChangeBox.Location = new Point(eMailChangeLabel.Location.X + eMailChangeLabel.Width + 10, eMailChangeLabel.Location.Y);
              eMailChangeButton.Location = new Point(eMailChangeBox.Location.X + eMailChangeBox.Width + 10, eMailChangeBox.Location.Y);
              #endregion
             */
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
