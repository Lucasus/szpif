using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Logic;

namespace Logic
{
    public class PageManager
    {
        IPageFactory PageFactory;
        public PageManager(IPageFactory PageFactory)
        {
            this.PageFactory = PageFactory;
        }

        private void setupPage(TabPage t,  string name)
        {
            t.Location = new System.Drawing.Point(4, 25);
            t.Name = name;
            t.Padding = new System.Windows.Forms.Padding(3);
            t.Size = new System.Drawing.Size(624, 433);
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
                    case "Administrator":
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
                    setupPage(newPage, pName);
                    tc.Controls.Add(newPage);
                    pages.Add(newPage);
                }
            }
            return pages;
        }

    }
}
