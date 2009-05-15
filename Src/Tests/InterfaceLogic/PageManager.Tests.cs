using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Szpif;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace Szpif.Tests
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


    }
}
