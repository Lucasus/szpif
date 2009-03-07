using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using BusinessLogic;

namespace BusinessLogic.Tests
{
	public class EmployeeRepositoryMock : IEmployeeRepository
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
		public Employee GetById(Guid guid)
		{
			return null;
		}
		public Employee GetByName(string name)
		{
			return null;
		}
		public ICollection<Employee> GetByCategory(string abla)
		{
			return employees;
		}
		
	}

	[TestFixture]
	public class LoggerTest
	{
		[Test()]
		public void constructorTest()
		{
			IEmployeeRepository lista = new EmployeeRepositoryMock();
			Logger logger = new Logger(lista);
			Assert.IsNotNull(logger, "Object was not created");
			Assert.AreSame(logger.employeeRepository, lista);
		}

		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullRank()
		{
			Logger logger = new Logger(new EmployeeRepositoryMock());

			logger.checkLogin("Jozin", "Bazin", null);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullLogin()
		{
			Logger logger = new Logger(new EmployeeRepositoryMock());
			
			logger.checkLogin(null, "Bazin", "Pomywacz");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullPassword()
		{
			Logger logger = new Logger(new EmployeeRepositoryMock());

			logger.checkLogin("Jozin", null, "Pomywacz");
		}
		
		[Test()]
		public void checkLoginReturnValue()
		{
			IEmployeeRepository lista = new EmployeeRepositoryMock();
			lista.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", "Pomywacz" ));
			lista.Add(new Employee("Józek", "Blabla", "Józek Blabla", "Pomywacz"));
			lista.Add(new Employee("Czesiek", "Wiesiek", "Czesiek Wiesiek", "Pomywacz"));

			Logger logger = new Logger(lista);

			bool found = logger.checkLogin("Jozin", "Bazin", "Pomywacz");
			Assert.IsTrue(found);
			found = logger.checkLogin("Jozin", "Bazin", "Administrator");
			Assert.IsFalse(found);
			found = logger.checkLogin("Józek", "Bózek", "Pomywacz");
			Assert.IsFalse(found);
			found = logger.checkLogin("Czesiek  ", "Wiesiek", "Pomywacz");
			Assert.IsTrue(found);
			found = logger.checkLogin("   Czesiek  ", "  Wiesiek  ", "Pomywacz");
			Assert.IsTrue(found);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void changePasswordNullPassword()
		{
			Logger logger = new Logger(new EmployeeRepositoryMock());
			logger.changePassword(null);
		}
		
		[Test()]
		[ExpectedException(typeof(NullReferenceException))]
		public void changePasswordNooneLoggedOn()
		{
			Logger logger = new Logger(new EmployeeRepositoryMock());
		
			logger.changePassword("Bolek");
		}
		
		[Test()]
		public void changePasswordTest()
		{
			IEmployeeRepository lista = new EmployeeRepositoryMock();
			lista.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", "Pomywacz"));
			Logger logger = new Logger(lista);
		
			logger.checkLogin("Jozin", "Bazin","Pomywacz");
			logger.changePassword("Józek");
			Assert.AreEqual(logger.currentlyLoggedOn.Password, "Józek");
		}
	}
}
