using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLibrary
{	
    public class SzpifDatabase : IDatabase
    {
		private enum EmployeeColumns
		{
			Id,
			CredentialsId,
			Login,
			Password
		}
    
		private enum DatabaseTable
		{
			Employees,
			Credentials,
			Permissions
		}
    
		static SzpifDatabase dataBase;
        static DbProviderFactory factory;
        static DbConnection conn;
        static string provider;
        static string connstr;

		/// <summary>
		/// To się okazuje jest konstruktor statyczny który inicjalizuje zmienne statyczne.
		/// </summary>
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
			dataBase = new SzpifDatabase();
        }
		
		/// <summary>
		/// Funkcja stworzona tylko po to by nie można było tworzyć nowych SzpifDatabase'ów
		/// </summary>
		private SzpifDatabase()
		{
		
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
		
		/// <summary>
		/// Funkcja wywołuje procedure składowaną z bazy danych do zmiany dowlnego wpisu w dowolnej kolumnie z dowolnej tabeli.
		/// Proponuje wogóle ograniczyć możliwość wywoływania tego przez użytkownika i wszystkie funkcjonalności opakować nad wywołaniem tej funkcji.
		/// NOTE: Zakładam póki co, że wszystkie tabele będą miały columne Id. To nie jest konieczne założenie, ale póki co się dobrze sprawdza.
		/// </summary>
		/// <param name="Table">Z której tabeli</param>
		/// <param name="column">Którą kolumne</param>
		/// <param name="value">zamienić na co</param>
		/// <param name="id">dla jakiego Id</param>
		private void changeColumnInTable(DatabaseTable Table, EmployeeColumns column, string value, EmployeeColumns conditionColumn, string conditionValue)
		{
			string command = "exec changeAtributeFromTable '" + Table.ToString() + "', '" + column.ToString() + "', '" + value + "', '" + conditionColumn.ToString() + "', '" + conditionValue + "'";
			try
			{
				executeNonQuerryCommand(command);
			}
			finally
			{
				conn.Close();
			}
		}
		
		private DbDataReader getColumnFromTable(DatabaseTable Table, EmployeeColumns column, EmployeeColumns conditionColumn, string conditionValue)
		{
		
		}
		
		private string getEmployeeIdFromLoginPassword(string login, string password)
		{
			string command = "exec getEmployeeIdByLoginAndPassword '" + login + "', '" + password + "'";
			try
			{
				DbDataReader dr = executeQuerryCommand(command);
				Int32 id = -1;
				if(dr.Read())
				{
					id = dr.GetInt32(dr.GetOrdinal("Id"));
				}
				return id.ToString();
			}
			finally
			{
				conn.Close();
			}
		}
	
		/// <summary>
		/// Funkcja Odpowiada za wywołanie procedury bazy danych checkPermissions
		/// oraz stworzeniu listy uprawnień użytkownika
		/// </summary>
		/// <param name="login">Login</param>
		/// <param name="password">Hasło</param>
		/// <returns>Kolekcje uprawnień</returns>
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
			string id = getEmployeeIdFromLoginPassword(login, password);
			changeColumnInTable(DatabaseTable.Employees, EmployeeColumns.Password, newPassword, EmployeeColumns.Id, id);
        }
        
        /// <summary>
		/// Funkcja Odpowiada za wywołanie procedury bazy danych changeEMail.
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="password">Hasło</param>
        /// <param name="newMail">Nowy E-Mail</param>
        public void ChangeEMail(string login, string password, string newMail)
        {
			string command = "exec changeEMail @Login='" + login + "',@Password='" + password + "', @newEmail='" + newMail + "'";
			try
			{
				executeNonQuerryCommand(command);
			}
			finally
			{
				conn.Close();
			}
        }

		/// <summary>
		/// Funkcja wyciąga informacje z widoku EmployeeAdministrtionView
		/// </summary>
		/// <returns>zwraca obiekt typu DataTable który łatwo włożyć do Gridów</returns>
		public DataTable getEmployeesAdministrationView()
        {
            string command = "SELECT * FROM EmployeeAdministrationView";
            try
            {
				DbDataReader dr = executeQuerryCommand(command);
				DataTable dt = new DataTable("EmployeeAdministrationView");
				dt.Load(dr);
				return dt;
			}
			finally
			{
				conn.Close();
			}
        }

   
    }
}
