using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessLogic;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections;
using System.Data;

namespace DatabaseLibrary.Tests
{

	[TestFixture]
	public class ContextTests
	{
		[Test()]
		public void initializationTest()
		{
			ICollection<string> permissions = new List<string>();
			Context.initialize("Moose","Master",permissions);
			Assert.IsNotNull(Context.CurrentContext);
			Assert.AreEqual(Context.CurrentContext.UserLogin, "Moose");
			Assert.AreEqual(Context.CurrentContext.UserPassword, "Master");
			Assert.AreSame(Context.CurrentContext.Permissions, permissions);
			
			ICollection<string> permissions2 = new List<string>();
			Context.initialize("Lukasz", "luk123", permissions2);
			Assert.AreEqual(Context.CurrentContext.UserLogin, "Lukasz");
			Assert.AreEqual(Context.CurrentContext.UserPassword, "luk123");
			Assert.AreSame(Context.CurrentContext.Permissions, permissions2);
		}
	}
}
