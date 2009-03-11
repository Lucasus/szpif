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
        private ICollection<TabPage> pages;

        public UserPanelForm(string userName, string password, ICollection<string> permissions)
        {
            this.userName = userName;
            this.password = password;
            this.permissions = permissions;
            this.permissions.Add("Ogolne");
            InitializeComponent();
            this.pages = new List<TabPage>();
            foreach (string perm in this.permissions)
            {
                TabPage newPage = null;
                switch (perm)
                {
                    case "Administrator":
                        {
                            newPage = new TabPage("Administrowanie pracownikami");
                            break;
                        }
                    case "Boss":
                        {
                            break;
                        }
                    case "Project Manager":
                        {
                            newPage = new TabPage("Projekty");
                            break;
                        }
                    case "Ogolne":
                        {
                            newPage = new TabPage("Twoje ustawienia");
                            break;
                        }
                };
                if(newPage != null)
                {
                    newPage.Location = new System.Drawing.Point(4, 25);
                    newPage.Name =  perm + "Page";
                    newPage.Padding = new System.Windows.Forms.Padding(3);
                    newPage.Size = new System.Drawing.Size(624, 433);
                    newPage.TabIndex = 0;
                    newPage.UseVisualStyleBackColor = true;
                    this.mainTabControl.Controls.Add(newPage);
                }
            }
        }

        private void UserPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
