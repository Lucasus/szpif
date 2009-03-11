using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections;

namespace DatabaseLibrary.Tests
{

    [TestFixture]
    public class SzpifDatabaseTests
    {
        private SzpifDatabase database;
        private void clearDatabase()
        {
            string sqlConnectionString  = "Data Source=localhost\\SQLEXPRESS;"
                    + "Initial Catalog=szpifDatabase;"
                    + "Integrated Security=SSPI;";
            FileInfo file = 
                new FileInfo("..\\..\\..\\..\\Database\\Tests\\generateTestData1.sql");
            string script = file.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server  server =  new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(script);
        }
        [TestFixtureSetUp]
        public void makeDatabase()
        {
            database = new SzpifDatabase();
        }
        [SetUp]
        public void settinUp()
        {
            clearDatabase();
        }

        [Test()]
        public void constructorTest()
        {
            SzpifDatabase d = new SzpifDatabase();
            Assert.IsNotNull(d);
        }

        [Test()]
        public void checkLoginTest()
        {
            ICollection<string> permissions =  
                database.CheckLogin("lukasz", "master");
            Assert.IsNotNull(permissions);
            Assert.Contains("Boss", (ICollection)permissions);
            Assert.Contains("Administrator", (ICollection)permissions);
            ICollection<string> permissions2 =
                database.CheckLogin("lukaszz", "master");
            Assert.IsNotNull(permissions2);
            Assert.AreEqual(0, permissions2.Count);

        }



    }
}
