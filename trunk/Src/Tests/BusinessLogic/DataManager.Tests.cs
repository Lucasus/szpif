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
	public class DataManagerTests
	{
		private DataManager dm;
		[TestFixtureSetUp()]
		public void setUp()
		{
			dm = new DataManager(SzpifDatabase.DataBase);
		}
	
		[Test()]
		public void initializationTest()
		{
			Assert.IsNotNull(dm);
		}
		
		[Test()]
		public void gridNameToViewNameTest()
		{
			string wynik = dm.gridNameToViewName("abla");
			Assert.IsNull(wynik);
			wynik = dm.gridNameToViewName("EmployeesForAdministrationGridView");
			Assert.AreEqual("EmployeeViewForAdministration", wynik);
		}
		
		[Test()]
		public void getValuesTest()
		{
			Assert.Fail();
		}
		
		[Test()]
		public void getColumnValuesFromViewTest()
		{
			ICollection<string> wynik = dm.getColumnValuesFromView("EmployeeViewForAdministration", "Login");
			Assert.IsNotNull(wynik);
			Assert.AreEqual(5, wynik.Count);
		}
	}
}
