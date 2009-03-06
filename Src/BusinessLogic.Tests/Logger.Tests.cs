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
			Logger logger = new Logger();
			Assert.AreNotEqual(logger, null, "Object was not created");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullLogin()
		{
			Logger logger = new Logger();
			
			logger.checkLogin(null, "Abla");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginEmptyLogin()
		{
			Logger logger = new Logger();
			logger.checkLogin("", "Buzek");
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentException))]
		public void checkLoginNullPassword()
		{
			Logger logger = new Logger();

			logger.checkLogin("Józek", null);
		}
	}
}
