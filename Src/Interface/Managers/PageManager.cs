using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Szpif;

namespace Szpif
{
    public class PageManager
    {
        PageFactory PageFactory;
        public PageManager(PageFactory PageFactory)
        {
            this.PageFactory = PageFactory;
        }

        private void setupPage(TabPage t,  string name)
        {
            t.Location = new System.Drawing.Point(4, 25);
            t.Name = name;
            t.Padding = new System.Windows.Forms.Padding(3);
            t.TabIndex = 0;
            t.UseVisualStyleBackColor = true;
        }

        private Dictionary<string, string> getPageNames(ICollection<string> Permissions)
        {
            Dictionary<string, string> pageNames = new Dictionary<string, string>();
            foreach (string perm in Permissions)
            {
                switch (perm)
                {
                    case "W³aœciciel":
                        {
                            pageNames.Add("Pracownicy","Employees");
                            pageNames.Add("Projekty", "Projects");
                            break;
                        }
                    case "Ogólne":
                        {
                            pageNames.Add("Twoje ustawienia", "EmployeesForUser");
                            break;
                        }
                    case "Project Manager":
                        {
                            pageNames.Add("Twoje Projekty", "ProjectsForPM");
                            pageNames.Add("Zadania w projektach", "TasksForPM");
                            break;
                        }
                    case "Prze³o¿ony":
                        {
                            break;
                        }
                    case "Opiekun handlowy":
                        {
                            break;
                        }
                    case "Pracownik":
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                };
            }
            return pageNames;
        }

        public List<TabPage> makeTabPages(int tabWidth, ICollection<string> Permissions)
        {
            Dictionary<string, string> pageNames = getPageNames(Permissions);
            List<TabPage> pages = new List<TabPage>();
            foreach(string name in pageNames.Keys)
            {
                TabPage newPage = null;
                if (pageNames[name] == "EmployeesForUser")
                    newPage = PageFactory.createTabPage("SettingsPage");  // .createUpdatePage(pageNames[name]);
                else if (pageNames[name] == "TasksForPM")
					newPage = PageFactory.createTabPage("TasksPage");
                else
                    newPage = PageFactory.createViewPage(pageNames[name]);
                newPage.Text = name;
                newPage.Name = pageNames[name];
                if (newPage != null)
                {
                    setupPage(newPage, name);
                    //tc.Controls.Add(newPage);
                    pages.Add(newPage);
                    newPage.Width = tabWidth - 50;
                }
            }
            return pages;
        }

    }
}
