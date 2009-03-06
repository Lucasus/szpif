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
            Employee e = new Employee();
            Assert.IsNotNull(e);
        }
    }
}
