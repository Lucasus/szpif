using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using System.Data.SqlClient;

namespace SzpifbaseBuilder
{
    public class DatabaseBuilder
    {
        private void InitializeConnection()
        {
            SqlConnection connection = (SqlConnection)SzpifDatabase.getEmptyConnection();
            connection.ConnectionString =
               "Data Source=localhost\\SQLEXPRESS;"
             + "Initial Catalog=szpifDatabase;"
             + "Integrated Security=SSPI;";
            SzpifDatabase.Connection = connection;
        }
    }
}
