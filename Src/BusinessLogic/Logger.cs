using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BussinesLogic;

namespace DatabaseLibrary
{	
	/// <summary>
	/// Klasa Zajmująca się Logicznym logowaniem się klienta do systemu.
	/// </summary>
	public class Logger
	{
        private IDatabase database;
		
		public Logger(IDatabase database)
		{
            this.database = database;
		}
		
		public ICollection<string> LogToSystem(String UserName, String Password)
		{
            ICollection<string> permissions
                    = database.CheckLogin(UserName, Password);

            if (permissions == null) return null;
            return permissions;
		}

	}
}
