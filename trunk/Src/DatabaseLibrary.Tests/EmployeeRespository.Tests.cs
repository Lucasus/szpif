using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using BusinessLogic;

namespace DatabaseLibrary.Tests
{
    [TestFixture()]
    public class EmployeeRespositoryTest
    {
        private IEmployeeRepository repository;

        [SetUp]
        public void SetupContext()
        {
            repository = new EmployeeRespository();
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
            Guid g = System.Guid.NewGuid();
            Employee e = new Employee("lucas", "ala123", "Łukasz Wiatrak", g);
            repository.Add(e);
            Employee f = repository.GetById(g);
            Assert.IsNotNull(f);
            Assert.AreEqual("lucas", f.Login);
        }

    }

}
