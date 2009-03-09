using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinesLogic;
using System.Data.Common;

namespace DatabaseLibrary
{
    public class Database : IDatabase
    {
        static DbProviderFactory factory;
        static DbConnection conn;
        static string provider;
        static string connstr;

        static Database()
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

        ICollection<string> CheckLogin(string login, string password)
        {
            return null;
        }

    }
}
