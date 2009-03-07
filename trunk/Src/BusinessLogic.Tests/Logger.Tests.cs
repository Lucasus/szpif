using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BusinessLogic.Tests
{
	[TestFixture]
	public class LoggerTest
	{
		[Test()]
		public void constructorTest()
		{
			List<Employee> lista = new List<Employee>();
			Logger logger = new Logger(lista);
			Assert.IsNotNull(logger, "Object was not created");
			Assert.AreSame(logger.Accounts, lista);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullLogin()
		{
			Logger logger = new Logger(new List<Employee>());
			
			logger.checkLogin(null, "Bazin");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullPassword()
		{
			Logger logger = new Logger(new List<Employee>());

			logger.checkLogin("Jozin", null);
		}
		
		[Test()]
		public void checkLoginReturnValue()
		{
			List<Employee> lista = new List<Employee>();
			lista.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", "Pomywacz" ));
			lista.Add(new Employee("Józek", "Blabla", "Józek Blabla", "Pomywacz"));
			lista.Add(new Employee("Czesiek", "Wiesiek", "Czesiek Wiesiek", "Pomywacz"));
				
			Logger logger = new Logger(lista);
			
			bool found = logger.checkLogin("Jozin", "Bazin");
			Assert.IsTrue(found);
			found = logger.checkLogin("Józek", "Bózek");
			Assert.IsFalse(found);
			found = logger.checkLogin("Czesiek  ", "Wiesiek");
			Assert.IsTrue(found);
			found = logger.checkLogin("   Czesiek  ", "  Wiesiek  ");
			Assert.IsTrue(found);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void changePasswordNullPassword()
		{
			Logger logger = new Logger(new List<Employee>());
			logger.changePassword(null);
		}
		
		[Test()]
		[ExpectedException(typeof(NullReferenceException))]
		public void changePasswordNooneLoggedOn()
		{
			Logger logger = new Logger(new List<Employee>());
		
			logger.changePassword("Bolek");
		}
		
		[Test()]
		public void changePasswordTest()
		{
			List<Employee> lista = new List<Employee>();
			lista.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", "Pomywacz"));
			Logger logger = new Logger(lista);
		
			logger.checkLogin("Jozin", "Bazin");
			logger.changePassword("Józek");
			Assert.AreEqual(logger.currentlyLoggedOn.Password, "Józek");
		}
	}
}
