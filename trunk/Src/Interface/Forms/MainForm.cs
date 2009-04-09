using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Logic;

namespace Interface
{
    public partial class MainForm : Form
    {
        private PageManager PageManager; 

        public MainForm()
        {
            InitializeComponent();
            this.PageManager = new PageManager(new PageFactory());
            PageManager.makeTabPages(this.mainTabControl,Program.Context.UserPermissions);
        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
