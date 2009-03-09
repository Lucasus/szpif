using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using BusinessLogic;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
namespace DatabaseLibrary.Tests
{
    /// <summary>
    /// TO DO: dodać sprawdzanie warunków integralnościowych bazy
    /// </summary>
    [TestFixture()]
    public class EmployeeRespositoryTest
    {
        private IEmployeeRepository repository;
        private Employee e, e2, e3;
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
        public void setUpAll()
        {
            clearDatabase();
        }

        [TearDown]
        public void clearAll()
        {
            clearDatabase();
        }
        [SetUp]
        public void SetupContext()
        {
            repository = new EmployeeRespository();
            e = new Employee("lucas", "ala123", "Łukasz Wiatrak", "szef");
            e2 = new Employee("losiek", "llll", "Krzychu", "pracownik");
            e3 = new Employee("marta", "abcd", "Marta", "project manager");
        }

        [Test()]
        public void EmployeeRespositoryConstructorTest()
        {
            EmployeeRespository r = new EmployeeRespository();
            Assert.IsNotNull(r);
        }

        [Test]
        public void addingEmployeeTest()
        {
            repository.Add(e);
            Employee f = repository.GetByLogin("lucas");
            Assert.IsNotNull(f);
            Assert.AreEqual("lucas", f.Login);
            repository.Remove(e);
            Employee e4 = new Employee("xxx", "xxxx", "blabla", "bla");
            Assert.AreEqual(-1, e4.Id);
            repository.Add(e4);
            Assert.AreNotEqual(-1, e4.Id);
            Employee e5 = repository.GetByLogin("xxx");
            Assert.AreEqual(e5.Id, e4.Id);
            repository.Remove(e4);
        }

        [Test]
        public void getByLoginTest()
        {
            repository.Add(e3);
            Employee emp = repository.GetByLogin("marta");
            Assert.IsNotNull(emp);
            Assert.AreEqual("Marta", emp.Name);
            repository.Remove(e3);
        }

        [Test]
        public void UpdateTest()
        {
            repository.Add(e);
            e.Login = "lucas master";
            repository.Update(e);
            Employee f = repository.GetById(e.Id);
            Assert.AreEqual("lucas master", f.Login);
            e.Login = "lucas";
            repository.Remove(e);
        }

        [Test]
        public void RemoveTest()
        {
            repository.Add(e);
            repository.Remove(e);
            Employee f = repository.GetByLogin(e.Login);
            Assert.IsNull(f);
        }

        [Test]
        public void GetByIdTest()
        {
            repository.Add(e2);
            Employee emp = repository.GetById(e2.Id);
            Assert.IsNotNull(emp);
            Assert.AreEqual("losiek", emp.Login);
            repository.Remove(e2);
        }

        [Test]
        public void GetByNameTest()
        {
            repository.Add(e2);
            Employee emp = repository.GetByName("Krzychu");
            Assert.IsNotNull(emp);
            Assert.AreEqual("losiek", emp.Login);
            repository.Remove(e2);
        }

        [Test]
        public void GetAllTest()
        {
            repository.Add(e);
            repository.Add(e2);
            repository.Add(e3);
            ICollection<Employee> coll = repository.GetAll();
            Assert.AreEqual(3, coll.Count);
            repository.Remove(e);
            repository.Remove(e2);
            repository.Remove(e3);
        }

    }

}
