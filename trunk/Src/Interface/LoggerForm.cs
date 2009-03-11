using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;

namespace Interface
{
    public partial class LoggerForm : Form
    {
        Logger logger;
        
        public LoggerForm()
        {
            InitializeComponent();
            logger = new Logger(new SzpifDatabase());
        }

		private void button1_Click(object sender, EventArgs e)
		{
            string userName = UserNameTextBox.Text;
            string password = PassWordTextBox.Text;
            ICollection<string> permissions 
                = logger.LogToSystem(userName, password);
  //          permissions.Add("Administrator");
			if(permissions.Count == 0)
			{
 //               this.Hide();
 //               UserPanelForm uPanelForm = new UserPanelForm(userName, password, permissions);
 //               uPanelForm.ShowDialog();
 //               this.Dispose(false);
//                MessageBox.Show("Podałeś zły login i/lub hasło");
			}
			else
			{
                this.Hide();
                UserPanelForm uPanelForm = new UserPanelForm(userName,password,permissions);
                uPanelForm.ShowDialog();
                this.Dispose(false);
			}
		}

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
