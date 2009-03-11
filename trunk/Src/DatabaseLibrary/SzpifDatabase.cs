using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic;
using System.Data.Common;
using System.Data;

namespace DatabaseLibrary
{
    public class SzpifDatabase : IDatabase
    {
        static DbProviderFactory factory;
        static DbConnection conn;
        static string provider;
        static string connstr;

        static SzpifDatabase()
        {
            provider = "System.Data.SqlClient";  // data provider
            connstr =   "Data Source=localhost\\SQLEXPRESS;"
                    +   "Initial Catalog=szpifDatabase;"
                    +   "Integrated Security=SSPI;";

            // Get factory object for SQL Server
            factory = DbProviderFactories.GetFactory(provider);

            // Get connection object. using ensures connection is closed.
            conn = factory.CreateConnection();
            conn.ConnectionString = connstr;
        }
		
		/// <summary>
		/// Funkcja upraszczająca wykonywanie zapytań zwracających tabele z bazy.
		/// Funkcja jest prywatna więc tylko ta klasa może z niego korzystać.
		/// </summary>
		/// <param name="command">Rozkaz który ma zostać wywołany przez baze danych</param>
		/// <returns>Zwraca Obiekt pozwalający w łatwy sposób czytać zwróconą tabele.</returns>
		private DbDataReader executeQuerryCommand(string command)
		{
			conn.Open();
			DbCommand cmd = factory.CreateCommand(); // Command object
			cmd.CommandText = command;
			cmd.Connection = conn;
			DbDataReader dr;
			dr = cmd.ExecuteReader();
			return dr;
		}
		
		/// <summary>
		/// Funkcja upraszczająca wykonywanie zapytań nie zwracajacych tabel z bazy.
		/// Funkcja jest prywatna więc tylko ta klasa może z niego korzystać.
		/// </summary>
		/// <param name="command">Rozkaz który ma zostać wywołany przez baze danych</param>
		private void executeNonQuerryCommand(string command)
		{
			conn.Open();
			DbCommand cmd = factory.CreateCommand(); // Command object
			cmd.CommandText = command;
			cmd.Connection = conn;
			cmd.ExecuteNonQuery();
		}

        public ICollection<string> CheckLogin(string login, string password)
        {
            string command = "exec checkPermissions @Login='"+
            login + "',@Password='"+password+"'";
            ICollection<string> permissions = new List<string>();
            try
            {
				DbDataReader dr = executeQuerryCommand(command);
				while (dr.Read())
				{
					string permission = dr.GetString(dr.GetOrdinal("Permission"));
					permissions.Add(permission);
				}
				return permissions;
			}
			finally
			{
				conn.Close();
			}
        }
        
        public void ChangePassword(string login, string password, string newPassword)
        {
			string command = "exec changePassword @Login='" + login + "',@currentPassword='" + password + "', @Password='" + newPassword + "'";
			try
			{
				executeNonQuerryCommand(command);
			}
			finally
			{
				conn.Close();
			}
        }

        public DataTable getEmployeesAdministrationView()
        {
            string command = "SELECT * FROM EmployeeAdministrationView";
            DbDataReader dr = executeQuerryCommand(command);
            DataTable dt = new DataTable("EmployeeAdministrationView");
            dt.Load(dr);
            return dt;
        }
    }
}
