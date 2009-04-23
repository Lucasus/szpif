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
        private static string provider;
        private static string connString;

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

        public static DbConnection getEmptyConnection()
        {
            DbConnection newConnection =  factory.CreateConnection();
            newConnection.ConnectionString = connString;
            return newConnection;
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
            SzpifDatabase.connString =
            connection.ConnectionString =
               "Data Source="     + System.Configuration.ConfigurationSettings.AppSettings["Data Source"] + ";"
             + "Initial Catalog=" + System.Configuration.ConfigurationSettings.AppSettings["Initial Catalog"]+";"
             + "User=" + username + ";"
             + "Password=" + password;
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
            string command = "exec changePassword @Login='" + login + "',@currentPassword='" + password + "', @Password='" + newPassword + "'";
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

        public DataTable getView(string viewName)
        {
            GetViewTransaction t = new GetViewTransaction(viewName);
            t.tryExecute();
            return t.View;
        }

        public DataTable getView(string viewName, DataTable schema)
        {
            GetViewTransaction t = new GetViewTransaction(viewName,schema);
            t.tryExecute();
            return t.View;
        }


        public void updateView(string viewName, DataTable view)
        {
            UpdateViewTransaction t = new UpdateViewTransaction(viewName, view);
            t.tryExecute();
        }
        public List<string> getWriteableAttributes(string viewName)
        {
            GetParametersTransaction t = new GetParametersTransaction(viewName);
            t.tryExecute();
            return t.Parameters;
        }

    }
}