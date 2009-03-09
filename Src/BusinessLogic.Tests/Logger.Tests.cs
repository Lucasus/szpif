using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessLogic;
using DatabaseLibrary;

namespace BusinessLogic.Tests
{
	public class EmployeeRespositoryMock : IEmployeeRepository
	{
		List<Employee> employees = new List<Employee>();
		public void Add(Employee employee)
		{
			employees.Add(employee);
		}
		public void Remove(Employee employee)
		{
			employees.Remove(employee);
		}
		public void Update(Employee employee)
		{
		}
		public Employee GetById(int id)
		{
			return null;
		}
		public Employee GetByName(string name)
		{
			return null;
		}
        public ICollection<Employee> GetAll()
		{
			return employees;
		}
        public Employee GetByLogin(string login)
        {
            return null;
        }
		
	}

	[TestFixture]
	public class LoggerTest
	{
		[Test()]
		public void constructorTest()
		{
			//IEmployeeRepository lista = new EmployeeRespositoryMock();
			//Logger logger = new Logger(lista);
			//Assert.IsNotNull(logger, "Object was not created");
			//Assert.AreSame(logger.employeeRepository, lista);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullRank()
		{
			//Logger logger = new Logger(new EmployeeRespositoryMock());

			//logger.CheckLogin("Jozin", "Bazin");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullLogin()
		{
			//Logger logger = new Logger(new EmployeeRespositoryMock());
			
			//logger.CheckLogin(null, "Bazin");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullPassword()
		{
			//Logger logger = new Logger(new EmployeeRespositoryMock());

			//logger.CheckLogin("Jozin", null);
		}

        [Test()]
        public void checkLoginOfExistingUserValue()
        {
            IEmployeeRepository lista = new EmployeeRespository();
            //Logger logger = new Logger(lista);
            //bool found = logger.CheckLogin("Jan", "Kowalski");
            //Assert.IsTrue(found);
        }
		
		[Test()]
		public void checkLoginReturnValue()
		{
			IEmployeeRepository lista = new EmployeeRespositoryMock();
			lista.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", "Pomywacz" ));
			lista.Add(new Employee("Józek", "Blabla", "Józek Blabla", "Pomywacz"));
			lista.Add(new Employee("Czesiek", "Wiesiek", "Czesiek Wiesiek", "Pomywacz"));

			//Logger logger = new Logger(lista);

			//bool found = logger.CheckLogin("Jozin", "Bazin");
			//Assert.IsTrue(found);
			//found = logger.CheckLogin("Jozin", "Bazin");
			//Assert.IsFalse(found);
			//found = logger.CheckLogin("Józek", "Bózek");
			//Assert.IsFalse(found);
			//found = logger.CheckLogin("Czesiek  ", "Wiesiek");
			//Assert.IsTrue(found);
			//found = logger.CheckLogin("   Czesiek  ", "  Wiesiek  ");
			//Assert.IsTrue(found);
		}
		
		
	}
}
