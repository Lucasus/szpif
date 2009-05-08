using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Szpif;
using Interface;

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
           // t.Size = new System.Drawing.Size(624, 433);
            t.TabIndex = 0;
            t.UseVisualStyleBackColor = true;
        }

        private ICollection<string> getPageNames(ICollection<string> Permissions)
        {
            ICollection<string> pageNames = new List<string>();
            foreach (string perm in Permissions)
            {
                switch (perm)
                {
                    case "W³aœciciel":
                        {
                            pageNames.Add("AdministrationPage");
                            break;
                        }
                    case "Ogólne":
                        {
                            pageNames.Add("SettingsPage");
                            break;
                        }
                    case "PM":
                        {
                            pageNames.Add("ProjectsPage");
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
            ICollection<string> pageNames = getPageNames(Permissions);
            List<TabPage> pages = new List<TabPage>();
            foreach (string pName in pageNames)
            {
                TabPage newPage = null;
                newPage = PageFactory.createTabPage(pName);
                if (newPage != null)
                {
//                    newPage.Width = tc.Width;
                    setupPage(newPage, pName);
                    tc.Controls.Add(newPage);
                    pages.Add(newPage);
                    newPage.Width = tc.Width - 50;
                }
            }
            return pages;
        }

    }
}
