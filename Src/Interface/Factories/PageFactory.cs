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
                case "TasksPage":
					{
						TabPage page = new TabPage("Zadania");
						List<TabPage> pages = new List<TabPage>();
						TabPage subpage = PageFactory.createViewPage("TasksForPMState1");
						subpage.Name = subpage.Text = "Zadania - W Toku";
						pages.Add(subpage);

						subpage = PageFactory.createViewPage("TasksForPMState2");
						subpage.Name = subpage.Text = "Zadania - Zakończone";
						pages.Add(subpage);
						
						TabbedControl tabControl = new TabbedControl(pages);
						tabControl.tabControl.Width = 725;
						tabControl.tabControl.Height -= 50;
						page.Controls.Add(tabControl);
						return page;
					}
            }
            return null;
        }

        public static TabPage createViewPage(string viewName)
        {
            TabPage page = new TabPage();
            page.Controls.Add(new ViewControl(viewName));
            return page;
        }

        public static TabPage createUpdatePage(string viewName)
        { 
            TabPage page = new TabPage();
            page.Controls.Add(new UpdateControl(viewName, new ViewControl(viewName)));
            return page;
        }

    }
}
