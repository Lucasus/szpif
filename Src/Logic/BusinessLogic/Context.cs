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
        private string userLogin;
        private string userPassword;
        private ICollection<string> permissions;
		
		/// <summary>
		/// Prywatny Konstruktor. Nic dodać nic ująć.
		/// </summary>
		/// <param name="login">Login użytkownika</param>
		/// <param name="password">Hasło użytkownika</param>
		/// <param name="perm">Kolekcja uprawnień którą ma dany użytkownik</param>
		private Context(string login, string password, ICollection<string> perm)
        {
            this.userLogin = login;
            this.userPassword = password;
            this.permissions = perm;
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
				CurrentContext.UserLogin = login;
				CurrentContext.UserPassword = password;
				CurrentContext.Permissions = permissions;
			}
        }
        
        public static Context CurrentContext
        {
			get { return currentContext; }
        }
		
        public string UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public ICollection<string> Permissions
        {
            get { return permissions; }
            set { permissions = value; }
        }


    }
}
