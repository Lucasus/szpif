using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
	/// <summary>
	/// Klasa odpowiadająca za przechowywanie danych użytkownika.
	/// Jest to Singleton w którym zawsze mają być aktualne(!) dane zalogowanego użytkownika.
	/// </summary>
    public class Context
    {
        private string userLogin;
        private string userPassword;
        private ICollection<string> roles;
        private static IDatabase database;
        private FormManager formManager;
        private DataManager dataManager;

        public DataManager DataManager
        {
            get { return dataManager; }
            set { dataManager = value; }
        }

        public FormManager FormManager
        {
            get { return formManager; }
            set { formManager = value; }
        }

        public IDatabase Database
        {
            get { return Context.database; }
            set { Context.database = value; }
        }
		
		/// <summary>
		/// Konstruktor. 
		/// </summary>
		public Context()
        {
            this.userLogin = null;
            this.userPassword = null;
            this.roles = null;
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

        public ICollection<string> UserRoles
        {
            get { return roles; }
            set { roles = value; }
        }


    }
}
