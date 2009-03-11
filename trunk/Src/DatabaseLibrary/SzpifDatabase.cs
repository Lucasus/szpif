using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinesLogic;
using System.Data.Common;

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

        public ICollection<string> CheckLogin(string login, string password)
        {
            string command = "exec checkPermissions @Login='"+
            login + "',@Password='"+password+"'";
            ICollection<string> permissions = new List<string>();
            try
            {
                conn.Open();
                DbCommand cmd = factory.CreateCommand(); // Command object
                cmd.CommandText = command;
                cmd.Connection = conn;
                DbDataReader dr;
                dr = cmd.ExecuteReader();
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

        //private DbDataReader executeCommand(string command)
        //{
        //    try
        //    {
        //        conn.Open();

        //        DbCommand cmd = factory.CreateCommand(); // Command object
        //        cmd.CommandText = command;
        //        cmd.Connection = conn;
        //        DbDataReader dr;
        //        dr = cmd.ExecuteReader();
        //        dr.Read();
        //        conn.Close();
        //        return dr;
        //    }
        //    catch (DbException ex)
        //    {
        //        System.Console.WriteLine(ex.ToString());
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Console.WriteLine(ex.ToString());
        //        return null;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        //public void Add(Employee e)
        //{
        //    try
        //    {
        //        string command = "INSERT INTO [Employees]  VALUES "
        //            + "('" + e.Login + "', '" + e.Password + "', '"
        //            + e.Name + "', '" + e.Rank + "');" +
        //            "; SELECT SCOPE_IDENTITY() ; ";
        //        DbCommand cmd = factory.CreateCommand();
        //        cmd.CommandText = command;
        //        cmd.Connection = conn;
        //        conn.Open();
        //        int Employeeid = Int16.Parse(cmd.ExecuteScalar().ToString());
        //        conn.Close();
        //        e.Id = Employeeid;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}
    }
}
