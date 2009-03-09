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
    public partial class Form1 : Form
    {
        Logger logger;
        
        public Form1()
        {
            InitializeComponent();

			IEmployeeRepository logins = new EmployeeRespository();
			//logins.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", "Pomywacz"));
			logger = new Logger(logins);
        }

		private void button1_Click(object sender, EventArgs e)
		{
			bool current = logger.checkLogin(UserNameTextBox.Text, PassWordTextBox.Text, RankTextBox.Text);
			if(current == false)
			{
				MessageBox.Show("Zły login");
			}
			else
			{
				MessageBox.Show("Witaj \"" + logger.currentlyLoggedOn.Name + "\" Klasy \"" + logger.currentlyLoggedOn.Rank + "\"");
			}
		}
    }
}
