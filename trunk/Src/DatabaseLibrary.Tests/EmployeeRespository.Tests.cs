using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using BusinessLogic;

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
        private Guid g, h, j;

        [SetUp]
        public void SetupContext()
        {
            repository = new EmployeeRespository();
            g = System.Guid.NewGuid();
            h = System.Guid.NewGuid();
            j = System.Guid.NewGuid();
            e = new Employee("lucas", "ala123", "Łukasz Wiatrak", "szef", g);
            e2 = new Employee("losiek", "llll", "Krzychu", "pracownik", h);
            e3 = new Employee("marta", "abcd", "Marta", "project manager", j);
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
            Employee f = repository.GetById(g);
            Assert.IsNotNull(f);
            Assert.AreEqual("lucas", f.Login);
        }

        [Test]
        public void UpdateTest()
        {
            repository.Add(e);
            e.Login = "lucas master";
            repository.Update(e);
            Employee f = repository.GetById(g);
            Assert.AreEqual("lucas master", f.Login);
        }

        [Test]
        public void RemoveTest()
        {
            repository.Add(e);
            repository.Remove(e);
            Employee f = repository.GetById(g);
            Assert.IsNull(f);
        }

        [Test]
        public void GetByIdTest()
        {
            Assert.AreEqual(1, 0);
        }

        [Test]
        public void GetByNameTest()
        {
            Assert.AreEqual(1, 0);
        }

        [Test]
        public void GetByCategoryTest()
        {
            Assert.AreEqual(1, 0);
        }

    }

}
