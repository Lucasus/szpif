using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Interface
{
    public partial class UserPanelForm : Form
    {
        private string userName;
        private string password;
        private ICollection<string> permissions;

        public UserPanelForm(string userName, string password, ICollection<string> permissions)
        {
            this.userName = userName;
            this.password = password;
            this.permissions = permissions;
            InitializeComponent();
        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
