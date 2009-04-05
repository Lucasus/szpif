﻿using System;
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
            clearDatabase();
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
            ICollection<string> permissions =  
                database.CheckLogin("lukasz", "master", "GenericEveryUser");
            Assert.IsNotNull(permissions);
            Assert.Contains("Boss", (ICollection)permissions);
            Assert.Contains("Administrator", (ICollection)permissions);
            ICollection<string> permissions2 =
				database.CheckLogin("lukaszz", "master", "GenericEveryUser");
            Assert.IsNotNull(permissions2);
            Assert.AreEqual(0, permissions2.Count);
        }

		[Test()]
		public void changePasswordCheckTest()
		{
			ICollection<string> permissions = database.CheckLogin("lukasz", "master", "GenericEveryUser");
			Assert.IsNotNull(permissions);
			database.ChangePassword("lukasz", "master", "mooster", "GenericEveryUser");
			ICollection<string> permissions2 = database.CheckLogin("lukasz", "mooster", "GenericEveryUser");
			Assert.IsNotNull(permissions2);
			database.ChangePassword("lukasz", "mooster", "master", "GenericEveryUser");
			ICollection<string> permissions3 = database.CheckLogin("lukasz", "master", "GenericEveryUser");
			Assert.IsNotNull(permissions3);
			database.ChangePassword("lukasz", "bublak", "mooster", "GenericEveryUser");
			ICollection<string> permissions4 = database.CheckLogin("lukasz", "master", "GenericEveryUser");
			Assert.IsNotNull(permissions4);
		}

		[Test()]
		public void changeEmailTest()
		{
			database.ChangeEMail("lukasz", "master", "lukasus@bablak.pl", "GenericEveryUser");
			DataTable dt = database.getEmployeesAdministrationView("GenericEmployer");
			Assert.AreEqual("lukasus@bablak.pl", dt.Rows[0]["EMail"]);
			database.ChangeEMail("lukasz", "mooster", "AblaAbla", "GenericEveryUser");
			dt = database.getEmployeesAdministrationView("GenericEmployer");
			Assert.AreNotEqual("AblaAbla", dt.Rows[0]["EMail"]);
		}

        [Test()]
        public void getEmployeesAdministrationViewTest()
        {
			DataTable dt = database.getEmployeesAdministrationView("GenericEmployer");
            Assert.AreEqual(3, dt.Rows.Count);
            Assert.AreEqual("Losiek Loskowski", dt.Rows[2]["Name"]);
        }
    }
}