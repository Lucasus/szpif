using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
	/// <summary>
	/// Klasa odpowiadająca za przechowywanie danych użytkownika.
	/// Jest to Singleton w którym zawsze mają być aktualne(!) dane zalogowanego użytkownika.
	/// </summary>
    public class Context
    {
		private static Context currentContext;
        private string currentUserLogin;
        private string currentUserPassword;
        private ICollection<string> currentPermissions;
		
		/// <summary>
		/// Prywatny Konstruktor. Nic dodać nic ująć.
		/// </summary>
		/// <param name="login">Login użytkownika</param>
		/// <param name="password">Hasło użytkownika</param>
		/// <param name="perm">Kolekcja uprawnień którą ma dany użytkownik</param>
		private Context(string login, string password, ICollection<string> perm)
        {
            this.currentUserLogin = login;
            this.currentUserPassword = password;
            this.currentPermissions = perm;
        }
        
        /// <summary>
        /// Czasami zastanawiam się po co tą funkcje napisałem.
		/// Trochę ułatwia uaktualnianie danych.
        /// </summary>
        /// <param name="login">Login użytkownika</param>
        /// <param name="password">Hasło użytkownika</param>
		/// <param name="permissions">Kolekcja uprawnień którą ma dany użytkownik</param>
        public static void initialize(string login, string password, ICollection<string> permissions)
        {
			if(currentContext == null)
			{
				currentContext = new Context(login, password, permissions);
			}
			else
			{
				CurrentContext.CurrentUserLogin = login;
				CurrentContext.CurrentUserPassword = password;
				CurrentContext.CurrentPermissions = permissions;
			}
        }
        
        public static Context CurrentContext
        {
			get { return currentContext; }
        }
		
        public string CurrentUserLogin
        {
            get { return currentUserLogin; }
            set { currentUserLogin = value; }
        }

        public string CurrentUserPassword
        {
            get { return currentUserPassword; }
            set { currentUserPassword = value; }
        }

        public ICollection<string> CurrentPermissions
        {
            get { return currentPermissions; }
            set { currentPermissions = value; }
        }


    }
}
