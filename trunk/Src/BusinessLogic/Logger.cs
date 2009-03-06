using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
	/*
	 *	Jest to klasa przechowująca dane użytkownika, jego nazwe, hasło, uprawnienia, oraz stanowisko.
	 */
	public class Login
	{
		/*
		 * Atrybuty
		 */
		private string userName;
		public string UserName
		{
			get
			{
				return userName;
			}
		}
		
		private string passWord;
		public string PassWord
		{
			get
			{
				return passWord;
			}
		}
		
		/*
		 *	Metody
		 */
		public Login(String UserName, String Password)
		{
			this.userName = UserName;
			this.passWord = Password;
		}
	}

	/*
	 *	Klasa Zajmująca się Logicznym logowaniem się klienta do systemu. 
	 */
	public class Logger
	{
		/*
		 * Atrybuty
		 */
		private List<Login> accounts;
		public List<Login> Accounts
		{
			get
			{
				return accounts;
			}
		}

		/*
		 *	Metody
		 */
		public Logger(List<Login> accounts)
		{
			this.accounts = accounts;
		}
		
		public Login checkLogin(String UserName, String Password)
		{
			Login found = null;
			if(UserName == null || UserName.Length == 0 || Password == null) throw new ArgumentException();
			
			foreach(Login log in accounts)
			{
				if(log.UserName == UserName && log.PassWord == Password)
				{
					found = log;
					break;
				}
			}
			
			return found;
		}
	}
}
