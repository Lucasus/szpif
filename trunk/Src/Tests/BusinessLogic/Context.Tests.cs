using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections;
using System.Data;
using Logic;
using Interface;

namespace DatabaseLibrary.Tests
{

	[TestFixture]
	public class ContextTests
	{
		[Test()]
		public void initializationTest()
		{
			FormManager manager = new FormManager(new FormFactory());
			Context temp = new Context(SzpifDatabase.DataBase, manager);
			Assert.IsNotNull(temp);
			Assert.IsNull(temp.UserLogin);
			Assert.IsNull(temp.UserPassword);
			Assert.IsNull(temp.UserRoles);
			
			/*ICollection<string> permissions = new List<string>();
			initializeContext("Moose","Master",permissions,temp);
			Assert.IsNotNull(temp);
			Assert.AreEqual(temp.UserLogin, "Moose");
			Assert.AreEqual(temp.UserPassword, "Master");
			Assert.AreSame(temp.UserRoles, permissions);
			
			ICollection<string> permissions2 = new List<string>();
			initializeContext("Lukasz", "luk123", permissions2,temp);
			Assert.AreEqual(temp.UserLogin, "Lukasz");
			Assert.AreEqual(temp.UserPassword, "luk123");
			Assert.AreSame(temp.UserRoles, permissions2);*/
		}
	}
}
