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
using System.Data;

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
                new FileInfo("..\\..\\..\\..\\Database\\Tests\\szpifDatabaseTests.sql");
            string script = file.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server  server =  new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(script);
            
        }
        [TestFixtureSetUp]
        public void makeDatabase()
        {
            database = SzpifDatabase.DataBase;
        }
        [SetUp]
        public void settinUp()
        {
            //clearDatabase();
        }

        [Test()]
        public void constructorTest()
        {
            SzpifDatabase d = SzpifDatabase.DataBase;
            Assert.IsNotNull(d);
        }

        [Test()]
        public void checkLoginTest()
        {
            bool wynik = database.CheckLogin("lukasz", "Master");
			Assert.IsTrue(wynik);
			wynik = database.CheckLogin("","");
			Assert.IsFalse(wynik);
			wynik = database.CheckLogin("lukasz", "abla");
			Assert.IsFalse(wynik);
			wynik = database.CheckLogin("abla", "Master");
			Assert.IsFalse(wynik);
        }

		/*[Test()]
		public void changePasswordCheckTest()
		{
			database.ChangePassword("lukasz", "Master", "Abla");
			bool wynik = database.CheckLogin("lukasz", "Abla");
            Assert.IsTrue(wynik);
            database.ChangePassword("luka", "Abla", "Master");
            wynik = database.CheckLogin("lukasz", "Master");
            Assert.IsFalse(wynik);
            database.ChangePassword("lukasz", "Abl", "Master");
            wynik = database.CheckLogin("lukasz", "Master");
            Assert.IsFalse(wynik);
            database.ChangePassword("lukasz", "Abla", "Master");
            wynik = database.CheckLogin("lukasz", "Master");
            Assert.IsTrue(wynik);
		}

		[Test()]
		public void changeEmailTest()
		{
//			database.ChangeEMail("lukasz", "master", "lukasus@bablak.pl");
            DataTable dt = database.getView("EmployeesAdministrationView");
			Assert.AreEqual("lukasus@bablak.pl", dt.Rows[0]["EMail"]);
			database.ChangeEMail("lukasz", "mooster", "AblaAbla", "GenericEveryUser");
			dt = database.getView("EmployeesAdministrationView");
			Assert.AreNotEqual("AblaAbla", dt.Rows[0]["EMail"]);
		}

        [Test()]
        public void getEmployeesAdministrationViewTest()
        {
            DataTable dt = database.getView("EmployeesAdministrationView");
            Assert.AreEqual(3, dt.Rows.Count);
            Assert.AreEqual("Losiek Loskowski", dt.Rows[2]["Name"]);
        }*/
    }
}
