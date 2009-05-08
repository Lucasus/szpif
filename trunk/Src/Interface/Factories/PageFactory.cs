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
    public class PageFactory
    {
        public TabPage createTabPage(string kind)
        {
            switch (kind)
            {
                case "SettingsPage":
                   return new UserSettingsPage("Twoje ustawienia");
                case "ProjectsPage":
                   return new TabPage("Projekty");
            }
            return null;
        }

        public static TabPage createViewPage(string viewName)
        {
            TabPage page = new TabPage();
            page.Controls.Add(new ViewControl(viewName));
            return page;
        }
    }
}
