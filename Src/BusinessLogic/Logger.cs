using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BussinesLogic;

namespace BusinessLogic
{	
	/// <summary>
	/// Klasa Zajmująca się Logicznym logowaniem się klienta do systemu.
	/// </summary>
	public class Logger
	{
        private IDatabase database;

        /*
		 * Atrybuty
		 */
		private string currentlyLoggedOnLogin;
        public string CurrentlyLoggedOnLogin
		{
			get{return currentlyLoggedOnLogin;}
		}
		
		public Logger(IDatabase database)
		{
			currentlyLoggedOnLogin = null;
            this.database = database;
		}
		

		/// <summary>
		///		Funkcja Sprawdza czy logowanie się powiodło.
		/// </summary>
		/// <param name="UserName">Login</param>
		/// <param name="Password">Hasło</param>
		/// <returns>
		///		true w przypadku powodzenia
		///		false w przypadku porażki
		///	</returns>
		public ICollection<string> LogToSystem(String UserName, String Password)
		{
            ICollection<string> permissions
                    = database.CheckLogin(UserName, Password);

            if (permissions == null) return null;

            currentlyLoggedOnLogin = UserName;

            return permissions;
            //            Employee employee = _employeeRep.GetByLogin(UserName);
//            if (employee == null) return false;
//			if(UserName == null || Password == null) throw new ArgumentException();
//			
//			if(employee.Login == UserName.Trim()
//              && employee.Password == Password.Trim())
//			{
//				found = true;
 //               _currentlyLoggedOn = employee;
//			}
//			
		}

	}
}
