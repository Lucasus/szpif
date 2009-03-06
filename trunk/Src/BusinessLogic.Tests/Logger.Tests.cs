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
			lista.Add(new Employee("Jozin", "Bazin", "Jozin z Bazin", new Guid()));
			lista.Add(new Employee("Józek", "Blabla", "Józek Blabla", new Guid()));
			lista.Add(new Employee("Czesiek", "Wiesiek", "Czesiek Wiesiek", new Guid()));
				
			Logger logger = new Logger(lista);
			
			Employee found = logger.checkLogin("Jozin", "Bazin");
			Assert.IsNotNull(found);
			found = logger.checkLogin("Józek", "Bózek");
			Assert.IsNull(found);
			found = logger.checkLogin("Czesiek  ", "Wiesiek");
			Assert.IsNotNull(found);
			found = logger.checkLogin("   Czesiek  ", "  Wiesiek  ");
			Assert.IsNotNull(found);
		}
	}
}
