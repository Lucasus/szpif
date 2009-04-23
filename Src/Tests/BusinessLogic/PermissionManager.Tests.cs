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
using DatabaseLibrary;
using Interface;

namespace Logic.Tests
{

	[TestFixture]
	public class PermissionManagerTests
	{
		private Context context;
		[TestFixtureSetUp]
		public void setUpAll()
		{
			context = new Context(SzpifDatabase.DataBase, new FormManager(new FormFactory()));
		}
		
		[Test()]
		public void constructorTest()
		{
			PermissionManager pm = new PermissionManager(context);
			Assert.IsNotNull(pm);
		}
		
		[Test()]
		public void tryLoginTest()
		{
			PermissionManager pm = new PermissionManager(context);
			
			//pm.tryLogin("Lorem","Ipsum");
			//Assert.AreNotEqual("Lorem", context.UserLogin);
			//Assert.AreNotEqual("Ipsum", context.UserPassword);
			
			pm.tryLogin("lukasz", "Master");
			Assert.AreEqual("lukasz", context.UserLogin);
			Assert.AreEqual("Master", context.UserPassword);
			Assert.Greater(context.UserRoles.Count, 0);
		}
	}
}
