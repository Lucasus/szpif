using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;
using Szpif.Controls.ContentControls;

namespace Szpif
{
    public partial class MainForm : Form
    {
        private PageManager PageManager; 

        public MainForm()
        {
            InitializeComponent();
            this.PageManager = new PageManager(new PageFactory());
			List<TabPage> pages = PageManager.makeTabPages(this.Width, Program.Context.UserRoles);
			TabbedControl tabControl = new TabbedControl(pages);
			tabControl.tabControl.Width = this.Width - 30;
			tabControl.tabControl.Height = this.Height - 60;
            this.Controls.Add(tabControl);
        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
