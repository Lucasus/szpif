using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessLogic;
using DatabaseLibrary;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace DatabaseLibrary.Tests
{

    [TestFixture]
    public class DatabaseTests
    {
        private DatabaseLibrary.Database database;
        private void clearDatabase()
        {
            string sqlConnectionString  = "Data Source=localhost\\SQLEXPRESS;"
                    + "Initial Catalog=szpifDatabase;"
                    + "Integrated Security=SSPI;";
            FileInfo file = new FileInfo("..\\..\\..\\..\\Database\\create.sql");
            string script = file.OpenText().ReadToEnd();
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server  server =  new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(script);
        }
        [TestFixtureSetUp]
        public void makeDatabase()
        {
            database = new DatabaseLibrary.Database();
        }

        [Test()]
        public void constructorTest()
        {
            //IEmployeeRepository lista = new EmployeeRespositoryMock();
            //Logger logger = new Logger(lista);
            //Assert.IsNotNull(logger, "Object was not created");
            //Assert.AreSame(logger.employeeRepository, lista);
        }

        [Test()]
        public void checkLoginTest()
        {
            database.CheckLogin("lukasz", "master");
            Assert.AreEqual(1, 0);
        }



    }
}
