using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DatabaseLibrary;

namespace BusinessLogic
{	
	/// <summary>
	/// Klasa Zajmująca się Logicznym logowaniem się klienta do systemu.
	/// </summary>
	public class Logger
	{
		/*
		 * Atrybuty
		 */
		private Employee _currentlyLoggedOn;
		public Employee currentlyLoggedOn
		{
			get{return _currentlyLoggedOn;}
		}
		
		private IEmployeeRepository _employeeRep;
		public IEmployeeRepository employeeRepository
		{
			get{return _employeeRep;}
		}
		
		/*private List<Employee> _accounts;
		public List<Employee> Accounts
		{
			get{return _accounts;}
		}*/

		/*
		 *	Metody
		 */
		public Logger(IEmployeeRepository accounts)
		{
			this._employeeRep = accounts;
			//this._accounts = accounts;
			_currentlyLoggedOn = null;
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
		public bool checkLogin(String UserName, String Password, String Rank)
		{
			ICollection<Employee> employees = _employeeRep.GetByCategory("Name");
			bool found = false;
			if(UserName == null || Password == null || Rank == null) throw new ArgumentException();
			
			foreach(Employee log in employees)
			{
				if(log.Login == UserName.Trim() && log.Password == Password.Trim() && log.Rank == Rank.Trim())
				{
					found = true;
					_currentlyLoggedOn = log;
					break;
				}
			}
			
			return found;	
		}
		
		/// <summary>
		///		Funkcja Pozwala zmienić hasło aktualnie zalogowanego użytkownika
		/// </summary>
		/// <param name="newPassword">Nowe hasło które ma zastąpić stare</param>
		public void changePassword(String newPassword)
		{
			if(newPassword == null) throw new ArgumentException();
			_currentlyLoggedOn.Password = newPassword;
		}
	}
}
