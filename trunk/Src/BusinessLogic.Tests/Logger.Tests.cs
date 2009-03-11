using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DatabaseLibrary;
using BusinessLogic;
using System.Collections;

namespace DatabaseLibrary.Tests
{
	[TestFixture]
	public class LoggerTest
	{
        private IDatabase database;
        private Logger logger;

        [TestFixtureSetUp]
        public void settingUp()
        {
            database = new SzpifDatabase();
            logger = new Logger(database);
        }

		[Test()]
		public void constructorTest()
		{
            Logger l2 = new Logger(database);
			Assert.IsNotNull(logger, "Object was not created");
		}

        [Test()]
        public void LogToSystemTest()
        {
            ICollection<string> permissions =
                logger.LogToSystem("lukasz", "master");
            Assert.IsNotNull(permissions);
            Assert.Contains("Boss", (ICollection)permissions);
            Assert.Contains("Administrator", (ICollection)permissions);
            ICollection<string> permissions2 =
                logger.LogToSystem("lukaszz", "master");
            Assert.IsNotNull(permissions2);
            Assert.AreEqual(0, permissions2.Count);
        }
		
	}
}
