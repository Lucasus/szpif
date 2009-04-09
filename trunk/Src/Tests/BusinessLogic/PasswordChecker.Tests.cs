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

namespace Logic.Tests
{

	[TestFixture]
	public class PasswordCheckerTests
	{
		[Test()]
		public void initializationTest()
		{
			PasswordChecker pc = new PasswordChecker("abla");
			Assert.IsNotNull(pc);
			Assert.AreEqual(pc.Login, "abla");
		}
		
		[Test()]
		public void passwordCheckTest()
		{
			PasswordChecker pc = new PasswordChecker("Moose");
			bool wynik = pc.isPasswordCorrect("Moose");
			Assert.IsFalse(wynik);
			wynik = pc.isPasswordCorrect("Master");
			Assert.IsFalse(wynik);
			wynik = pc.isPasswordCorrect("MooseMaster");
			Assert.IsTrue(wynik);
			wynik = pc.isPasswordCorrect("Moose123Master456__!asd");
			Assert.IsTrue(wynik);
		}
		
		[Test()]
		public void strenghtCheckTest()
		{
			PasswordChecker pc = new PasswordChecker("Moose");
			Strenght str = pc.getPasswordStrenght("Master");
			Assert.AreEqual(Strenght.None, str);
			str = pc.getPasswordStrenght("1234567");
			Assert.AreEqual(Strenght.None, str);
			str = pc.getPasswordStrenght("MooseMaster");
			Assert.AreEqual(Strenght.Weak, str);
			str = pc.getPasswordStrenght("Moose9Master");
			Assert.AreEqual(Strenght.Medium, str);
			str = pc.getPasswordStrenght("Moose!!9Master");
			Assert.AreEqual(Strenght.Strong, str);
		}
	}
}
