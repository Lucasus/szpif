using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;

namespace Interface
{
    public partial class Form1 : Form
    {
        Logger logger;
        
        public Form1()
        {
            InitializeComponent();

			List<Employee> logins = new List<Employee>();
			logins.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", new Guid()));
			logger = new Logger(logins);
        }

		private void button1_Click(object sender, EventArgs e)
		{
			Employee current = logger.checkLogin(UserNameTextBox.Text, PassWordTextBox.Text);
			if(current == null)
			{
				MessageBox.Show("Zły login");
			}
			else
			{
				MessageBox.Show("Jesteś Zalogowany");
			}
		}
    }
}
