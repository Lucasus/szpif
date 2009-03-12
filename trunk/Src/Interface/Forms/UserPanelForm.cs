using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLogic;
using InterfaceLogic;

namespace Interface
{
    public partial class UserPanelForm : Form
    {
        private PageManager PageManager; 

        public UserPanelForm(string userName, string password, ICollection<string> permissions)
        {
            InitializeComponent();
            this.PageManager = new PageManager(new PageFactory());
            PageManager.makeTabPages(this.mainTabControl);

        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
