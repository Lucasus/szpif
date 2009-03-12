using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;
using InterfaceLogic;


namespace Interface
{
    class PageFactory : IPageFactory
    {
        public TabPage createTabPage(string kind)
        {
            switch (kind)
            {
                case "AdministrationPage":
                   return new EmployeeAdministrationPage("Administrowanie pracownikami");
                case "SettingsPage":
                   return new UserSettingsPage("Twoje ustawienia");
                case "ProjectsPage":
                   return new TabPage("Projekty");
            }
            return null;
        }

    }
}
