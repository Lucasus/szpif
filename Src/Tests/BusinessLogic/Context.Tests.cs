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

namespace DatabaseLibrary.Tests
{

	[TestFixture]
	public class ContextTests
	{
        void initializeContext(string login, string password, ICollection<string> perm, Context c)
        {
            c.UserLogin = login;
            c.UserPassword = password;
            c.UserPermissions = perm;
        }
		[Test()]
		public void initializationTest()
		{
			Context temp = new Context();
			Assert.IsNotNull(temp);
			Assert.AreEqual("", temp.UserLogin);
			Assert.AreEqual("", temp.UserPassword);
			Assert.AreEqual(0, temp.UserPermissions.Count);
			
			ICollection<string> permissions = new List<string>();
			initializeContext("Moose","Master",permissions,temp);
			Assert.IsNotNull(temp);
			Assert.AreEqual(temp.UserLogin, "Moose");
			Assert.AreEqual(temp.UserPassword, "Master");
			Assert.AreSame(temp.UserPermissions, permissions);
			
			ICollection<string> permissions2 = new List<string>();
			initializeContext("Lukasz", "luk123", permissions2,temp);
			Assert.AreEqual(temp.UserLogin, "Lukasz");
			Assert.AreEqual(temp.UserPassword, "luk123");
			Assert.AreSame(temp.UserPermissions, permissions2);
		}
	}
}
