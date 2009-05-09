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
                    case "PM":
                        {
                            pageNames.Add("Projekty", "Projects");
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

        public List<TabPage> makeTabPages(TabControl tc, ICollection<string> Permissions)
        {
            Dictionary<string, string> pageNames = getPageNames(Permissions);
            List<TabPage> pages = new List<TabPage>();
            foreach(string name in pageNames.Keys)
            {
                TabPage newPage = PageFactory.createViewPage(pageNames[name]);
                newPage.Text = name;
                newPage.Name = pageNames[name];
                if (newPage != null)
                {
                    setupPage(newPage, name);
                    tc.Controls.Add(newPage);
                    pages.Add(newPage);
                    newPage.Width = tc.Width - 50;
                }
            }
            return pages;
        }

    }
}
