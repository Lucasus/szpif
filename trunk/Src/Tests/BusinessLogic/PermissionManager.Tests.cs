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
			PermissionManager pm = new PermissionManager(context.Database);
			Assert.IsNotNull(pm);
		}
		
		[Test()]
		public void getPermissionsTest()
		{
			PermissionManager pm = new PermissionManager(context.Database);

			ICollection<string> permissions = pm.getUserPermissions("Lorem", "Ipsum");
			Assert.IsNull(permissions);

			permissions = pm.getUserPermissions("lukasz", "Ipsum");
			Assert.IsNull(permissions);
			
			permissions = pm.getUserPermissions("Lorem", "Maaster");
			Assert.IsNull(permissions);
			
			permissions = pm.getUserPermissions("lukasz", "Master");
			Assert.IsNotNull(permissions);
		}
	}
}
