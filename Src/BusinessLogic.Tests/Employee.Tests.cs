using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BusinessLogic.Tests
{
    [TestFixture()]
    public class EmployeeTest
    {
        [Test()]
        public void EmployeeConstructorTest()
        {
            Employee e = new Employee("lucas", "ala123", "Łukasz Wiatrak", "szef");
            Assert.IsNotNull(e);
            Assert.AreEqual("lucas", e.Login);
            Assert.AreEqual("ala123", e.Password);
            Assert.AreEqual("Łukasz Wiatrak", e.Name);
        }
    }
}
