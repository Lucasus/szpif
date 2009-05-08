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
using Szpif;
using Interface;

namespace Szpif.Tests
{

	[TestFixture]
	public class PermissionManagerTests
	{
		PermissionManager pm;
		[TestFixtureSetUp]
		public void setUpAll()
		{
			pm = new PermissionManager(SzpifDatabase.DataBase);
		}
		
		[Test()]
		public void constructorTest()
		{
			Assert.IsNotNull(pm);
		}
		
		[Test()]
		public void getPermissionsTest()
		{
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
