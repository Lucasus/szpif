using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
	/// <summary>
	///	Klasa Zajmująca się Logicznym logowaniem się klienta do systemu. 
	/// </summary>
	public class Logger
	{
		/*
		 * Atrybuty
		 */
		private List<Employee> accounts;
		public List<Employee> Accounts
		{
			get
			{
				return accounts;
			}
		}

		/*
		 *	Metody
		 */
		public Logger(List<Employee> accounts)
		{
			this.accounts = accounts;
		}
		
		public Employee checkLogin(String UserName, String Password)
		{
			Employee found = null;
			if(UserName == null || Password == null) throw new ArgumentException();
			
			foreach(Employee log in accounts)
			{
				if(log.Login == UserName.Trim() && log.Password == Password.Trim())
				{
					found = log;
					break;
				}
			}
			
			return found;
		}
	}
}
