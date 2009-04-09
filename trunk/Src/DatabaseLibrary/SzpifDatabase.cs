using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLibrary
{
    public class SzpifDatabase : IDatabase
    {
		private static SzpifDatabase dataBase;
        private static DbProviderFactory factory;
        private static DbConnection connection;
        private string provider;

        public static DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        public static DbProviderFactory Factory
        {
            get { return SzpifDatabase.factory; }
            set { SzpifDatabase.factory = value; }
        }
        public static SzpifDatabase DataBase
        {
            get
            {
                if (dataBase == null)
                {
                    dataBase = new SzpifDatabase();
                }
                return dataBase;
            }
        }
		
		/// <summary>
		/// Funkcja stworzona tylko po to by nie można było tworzyć nowych SzpifDatabase'ów
		/// </summary>
		private SzpifDatabase()
		{
            provider = "System.Data.SqlClient";
            factory = DbProviderFactories.GetFactory(provider);
            connection = factory.CreateConnection();		
		}

        public void setupConnectionParameters(string username, string password)
        {
            connection.ConnectionString =
               "Data Source=localhost\\SQLEXPRESS;"
             + "Initial Catalog=szpifDatabase;"
             + "User=" + username + ";Password=" + password;
        }

        public bool CheckLogin(string login, string password)
        {
            string oldConnectionString = connection.ConnectionString;
            setupConnectionParameters(login, password);
            SzpifTransaction checkLogin = new SzpifTransaction();
            checkLogin.tryExecute();
            connection.ConnectionString = oldConnectionString;
            return checkLogin.Failed == false;
        }
	

		/// <summary>
		/// Funkcja Odpowiada za wywołanie procedury bazy danych changePassword.
		/// W wypadku podania błednego loginu, lub hasła, nie będzie żadnych konsekwencji,
		/// gdyż baza danych nie znajdzie rekordów spełniających kryterium do aktualizacji.
		/// </summary>
		/// <param name="login">Login</param>
		/// <param name="password">Hasło</param>
		/// <param name="newPassword">Nowe Hasło</param>
		public void ChangePassword(string login, string password, string newPassword)
        {
            string command = "";// addPriviligesRestriction(priviliges, "exec changePassword @Login='" + login + "',@currentPassword='" + password + "', @Password='" + newPassword + "'");
            (new NonQueryTransaction(command)).tryExecute();
        }
        /// <summary>
		/// Funkcja Odpowiada za wywołanie procedury bazy danych changeEMail.
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Hasło</param>
        /// <param name="newMail">Nowy E-Mail</param>
        public void ChangeEMail(string login, string password, string newMail, string priviliges)
        {
			string command = "exec changeEMail @Login='" + login + "',@Password='" + password + "', @newEmail='" + newMail + "'";
            (new NonQueryTransaction(command)).tryExecute();
        }

		/// <summary>
		/// Funkcja wyciąga informacje z widoku EmployeeAdministrtionView
		/// </summary>
		/// <returns>zwraca obiekt typu DataTable który łatwo włożyć do Gridów</returns>
		public DataTable getEmployeesAdministrationView()
        {
            GetViewTransaction t = new GetViewTransaction("EmployeeViewForAdministration");
            t.tryExecute();
			return t.View;
        }

        public ICollection<string> getUserRoles()
        {
            string login = "";
            string password = "";
            string command = "exec checkPermissions @Login='" + login + "',@Password='" + password + "'";
            GetViewTransaction t = new GetViewTransaction(command);
            ICollection<string> permissions = new List<string>();
            t.tryExecute();
//            while (t.View.Read())
//            {
//                string permission = t.View.GetString(t.View.GetOrdinal("Permission"));
//                permissions.Add(permission);
//            }
            return permissions;
        }

        public DataTable getView(string viewName)
        {
            GetViewTransaction t = new GetViewTransaction(viewName);
            t.tryExecute();
            return t.View;
        }
    }
}
