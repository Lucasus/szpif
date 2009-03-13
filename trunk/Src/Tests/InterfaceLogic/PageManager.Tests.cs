using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessLogic;
using InterfaceLogic;
using Interface;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace InterfaceLogic.Tests
{

    [TestFixture]
    public class PageManagerTests
    {
        private PageManager pageManager;
        [TestFixtureSetUp]
        public void setUpAll()
        {
            pageManager = new PageManager(new PageFactory());
        }


        [Test()]
        public void getPageNamesTest()
        {
            List<string> permissions = new List<string>();
            permissions.Add("Administrator");
            permissions.Add("Ogólne");
            permissions.Add("PM");
            TabControl tabControl =  new TabControl();
            List<TabPage> tabPages = pageManager.makeTabPages(tabControl, permissions);
            Assert.AreEqual(3, tabControl.Controls.Count);
            Assert.AreEqual(3, tabPages.Count);
            foreach (TabPage t in tabPages)
                Assert.Contains(t, tabControl.Controls);

        }
    }
}
