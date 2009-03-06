using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace BusinessLogic.Tests
{
	[TestFixture]
	public class LoginTest
	{
		[Test()]
		public void constructorTest()
		{
			Login login = new Login("Jozin", "Bazin");
			Assert.IsNotNull(login);
			Assert.AreEqual(login.UserName, "Jozin");
			Assert.AreEqual(login.PassWord, "Bazin");
		}
	}

	[TestFixture]
	public class LoggerTest
	{
		[Test()]
		public void constructorTest()
		{
			List<Login> lista = new List<Login>();
			Logger logger = new Logger(lista);
			Assert.IsNotNull(logger, "Object was not created");
			Assert.AreSame(logger.Accounts, lista);
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullLogin()
		{
			Logger logger = new Logger(new List<Login>());
			
			logger.checkLogin(null, "Abla");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginEmptyLogin()
		{
			Logger logger = new Logger(new List<Login>());
			logger.checkLogin("", "Buzek");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullPassword()
		{
			Logger logger = new Logger(new List<Login>());

			logger.checkLogin("Józek", null);
		}
		
		[Test()]
		public void checkLoginReturnValue()
		{
			List<Login> lista = new List<Login>();
			lista.Add(new Login("Jozin", "Bazin"));
			lista.Add(new Login("Józek", "Blabla"));
			lista.Add(new Login("Czesiek", "Wiesiek"));
				
			Logger logger = new Logger(lista);
			
			Login found = logger.checkLogin("Jozin", "Bazin");
			Assert.IsNotNull(found);
			found = logger.checkLogin("Józek", "Bózek");
			Assert.IsNull(found);
		}
	}
}
