using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management;

namespace SzpifbaseBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseBuilder builder = new DatabaseBuilder();
            SqlParameter p = new SqlParameter();
//            p.
            Column c = new Column();
//            c.Properties.
//            c.
//            c.
            ViewPermissions permissions = new ViewPermissions();
            permissions.Add("select", "Owner");
            permissions.Add("insert", "Owner");
            permissions.Add("update", "Owner");
            permissions.Add("delete", "Owner");

            List<ViewColumn> columns = new List<ViewColumn>();
            columns.Add(new ViewColumn("Employees", "Id", true));
            columns.Add(new ViewColumn("Employees", "Login", false));
            columns.Add(new ViewColumn("Credentials", "Name", false));
            columns.Add(new ViewColumn("Credentials", "EMail", false));
            columns.Add(new ComputedViewColumn(
               new ViewColumn("Employees", "Id",false), 
               "dbo.aggregateRolesFunction", "Uprawnienia"));

            List<string> tables = new List<string>();
            tables.Add("Employees");
            tables.Add("Credentials");
            tables.Add("Roles");

            List<ViewJoin> joins = new List<ViewJoin>();
            joins.Add(new ViewJoin("Roles", 
                            new ViewColumn("Roles", "EmployeeId",true),
                            new ViewColumn("Employees", "Id",true)));
            joins.Add(new ViewJoin("Credentials",
                            new ViewColumn("Credentials", "Id",true),
                            new ViewColumn("Employees", "CredentialsId",true)));


            IntegratedView employeesView = new IntegratedView(
                "testView", columns, permissions, tables, joins);

            builder.generate(employeesView);

            Console.WriteLine("Press Enter to Continue");
            Console.ReadKey();
        }
    }
}
