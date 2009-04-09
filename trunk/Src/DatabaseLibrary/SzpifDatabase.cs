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
		static SzpifDatabase dataBase;

        static DbProviderFactory factory;

        public static DbProviderFactory Factory
        {
            get { return SzpifDatabase.factory; }
            set { SzpifDatabase.factory = value; }
        }
        static DbConnection connection;
        string provider;

        static public DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
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
		
		public static SzpifDatabase DataBase
		{
			get
			{ 
				if(dataBase == null)
				{
					dataBase = new SzpifDatabase();
				}
				return dataBase;
			}
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
            ITransaction checkLogin = new EmptyTransaction();
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
            NonQueryTransaction t = new NonQueryTransaction(command);
            t.tryExecute();
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
            NonQueryTransaction t = new NonQueryTransaction(command);
            t.tryExecute();
        }

		/// <summary>
		/// Funkcja wyciąga informacje z widoku EmployeeAdministrtionView
		/// </summary>
		/// <returns>zwraca obiekt typu DataTable który łatwo włożyć do Gridów</returns>
		public DataTable getEmployeesAdministrationView(string priviliges)
        {
            string command = "SELECT * FROM EmployeeAdministrationView;";
            QueryTransaction t = new QueryTransaction(command);
            t.tryExecute();
		    DataTable dt = new DataTable("EmployeeAdministrationView");
			dt.Load(t.Table);
			return dt;
        }

        public ICollection<string> getUserPermissions()
        {
            string login = "";
            string password = "";
            string command = "exec checkPermissions @Login='" + login + "',@Password='" + password + "'";
            QueryTransaction t = new QueryTransaction(command);
            ICollection<string> permissions = new List<string>();
            t.tryExecute();
            while (t.Table.Read())
            {
                string permission = t.Table.GetString(t.Table.GetOrdinal("Permission"));
                permissions.Add(permission);
            }
            return permissions;
        }
    }
}
