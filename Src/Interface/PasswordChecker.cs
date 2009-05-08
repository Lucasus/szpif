using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Szpif
{
	public enum Strenght
	{
		None,
		Weak,
		Medium,
		Strong
	}

	public class PasswordChecker
	{
		private string login;
		public string Login
		{
			get { return login; }
			set { login = value; }
		}
		
		public PasswordChecker(string login)
		{
			this.Login = login;
		}
		
		/// <summary>
		/// Docelowo zamierzam wprowadzić pewne ograniczenia na hasła po to jest ta funkcja.
		/// </summary>
		/// <param name="password">hasło które chcemy sprawdzić</param>
		/// <returns>informacje czy hasło jest poprawne</returns>
		public bool isPasswordCorrect(string password)
		{
			if(password == login) return false;
			if(password.Length < 8) return false;
			return true;
		}
		
		/// <summary>
		/// Funkcja zwraca siłe hasła:
		/// None gdy za krótkie lub takie same jak nazwa użytkownika.
		/// Weak gdy składa się z samych znaków.
		/// Medium gdy są w nim liczby.
		/// Strong gdy są w nim znaki inne niż a-zA-Z0-9
		/// </summary>
		/// <param name="password">hasło do sprawdzenia</param>
		/// <returns>Siła hasła</returns>
		public Strenght getPasswordStrenght(string password)
		{
			if(!isPasswordCorrect(password)) return Strenght.None;
			Regex weakRegularExp = new Regex("^[a-zA-Z]*$");
			if(weakRegularExp.IsMatch(password)) return Strenght.Weak;
			Regex mediumEegularExp = new Regex("^[a-zA-Z0-9]*$");
			if(mediumEegularExp.IsMatch(password)) return Strenght.Medium;
			return Strenght.Strong;
		}
	}
}
