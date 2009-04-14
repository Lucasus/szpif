using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace SzpifbaseBuilder
{
    public class DatabaseBuilder
    {
        string connectionString;
        SqlConnection connection;
        private void InitializeConnection()
        {
            connection = (SqlConnection)SzpifDatabase.getEmptyConnection();
            connectionString =
               "Data Source=localhost\\SQLEXPRESS;"
             + "Initial Catalog=szpifDatabase;"
             + "Integrated Security=SSPI;";
            SzpifDatabase.Connection = connection;
        }

        internal void generate(IntegratedView employeesView)
        {
            connectionString =
               "Data Source=localhost\\SQLEXPRESS;"
             + "Initial Catalog=szpifDatabase;"
             + "Integrated Security=SSPI;";
            string procName = "get" + employeesView.ViewName;
            // Create an instance of the server
            connection = new SqlConnection(connectionString);
            Server server = new Server(new ServerConnection(connection));
            SqlCommand scommand = new SqlCommand();
            scommand.CommandText = "IF OBJECT_ID('" + procName + "') IS NOT NULL " +
            "DROP PROCEDURE " + procName;
            scommand.Connection = connection;
            connection.Open();
            scommand.ExecuteNonQuery();
            connection.Close();

            // I want to add the stored procedure to the "MyDatabase" Database
            Database db = server.Databases["SzpifDatabase"];

            // Create a Stored Procedure 
            StoredProcedure getView = //new StoredProcedure();
               new StoredProcedure(db, "get" + employeesView.ViewName);

            getView.TextMode = false;
            getView.AnsiNullsStatus = false;
            getView.QuotedIdentifierStatus = false;

            // GetClubByID requires the ID of the Club as an Input Parameter
            //StoredProcedureParameter idParam =
            //        new StoredProcedureParameter(getView, "@ID", DataType.Int);
            //getView.Parameters.Add(idParam);

            // The SQL Text
            string command = "SELECT DISTINCT ";
            string columns = "";
            for (int i = 0; i < employeesView.Columns.Count; ++i )
            {
                columns += employeesView.Columns[i].getFullName();
                if(i+1 < employeesView.Columns.Count)columns +=", ";
            }
            command += columns;
            command += " FROM ";
            command += " "+employeesView.TableNames[0]+" " 
                          +employeesView.TableNames[0]+"1"+ " ";
            string joins = "";
            string mainName = employeesView.TableNames[0];
            foreach (ViewJoin vj in employeesView.Joins)
            {
                joins += vj.getText();
            }
            command += joins;
            getView.TextBody = command;

            getView.Create();

            // teraz tworzę procedurę do update'owania
//CREATE PROCEDURE updateEmployeeViewForAdministration
//  @Id			int,
//  @Login		nvarchar(40),
//  @Name			nvarchar(40),
//  @EMail		nvarchar(40)
//AS
//    update Employees set Login = @Login  where Id = @Id    
//    update Credentials set Name = @Name, EMail = @EMail where Id = 
//    (select CredentialsId from Employees where Id = @Id)
//GO

        }
    }
}
