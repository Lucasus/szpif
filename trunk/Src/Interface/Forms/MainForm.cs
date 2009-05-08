using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Szpif;

namespace Szpif
{
    public partial class MainForm : Form
    {
        private PageManager PageManager; 

        public MainForm()
        {
            InitializeComponent();
            this.PageManager = new PageManager(new PageFactory());
            this.mainTabControl.Width = this.Width - 30;
            PageManager.makeTabPages(this.mainTabControl,Program.Context.UserRoles);
        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
