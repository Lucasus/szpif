using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using BusinessLogic;

namespace DatabaseLibrary
{
    /// <summary>
    /// Klasa odpowiedzialna za udostępnianie kolekcji pracowników
    /// zawartej w bazie danych.
    /// </summary>
    public class EmployeeRespository : IEmployeeRepository
    {
        static DbProviderFactory factory;
        static DbConnection conn;
        static string provider;
        static string connstr;

        static EmployeeRespository()
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
        ~EmployeeRespository()
        {
           // conn.Close();
        }

        private DbDataReader executeCommand(string command)
        {
            try
            {
                conn.Open();

                DbCommand cmd = factory.CreateCommand(); // Command object
                cmd.CommandText = command;
                cmd.Connection = conn;
                DbDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();
                conn.Close();
                return dr;
            }
            catch (DbException ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// Adds the specified employee to repository.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// 
        public void Add(Employee e)
        {
            try
            {
                string command = "INSERT INTO [Employees]  VALUES "
                    + "('" + e.Login + "', '" + e.Password + "', '"
                    + e.Name + "', '" + e.Rank + "');" +
                    "; SELECT SCOPE_IDENTITY() ; ";
                DbCommand cmd = factory.CreateCommand();
                cmd.CommandText = command;
                cmd.Connection = conn;
                conn.Open();
                int Employeeid = Int16.Parse(cmd.ExecuteScalar().ToString());
                conn.Close();
                e.Id = Employeeid;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Updates the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public void Update(Employee employee)
        {
        }

        /// <summary>
        /// Removes the specified employee.
        /// </summary>
        /// <param name="employee">The employee.</param>
        public void Remove(Employee e)
        {
            string command = 
                "DELETE FROM Employees WHERE Login = " + "'" + e.Login + "'";
            executeCommand(command);
        }

        public Employee GetById(int id)
        {
            string command =
                "SELECT * FROM Employees WHERE Id = " + "'" + id + "'";
            try
            {
                conn.Open();

                DbCommand cmd = factory.CreateCommand(); // Command object
                cmd.CommandText = command;
                cmd.Connection = conn;
                DbDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();
                Employee emp = new Employee((string)dr["Login"], (string)dr["Password"],
                                (string)dr["Name"], (string)dr["Rank"]);
                emp.Id = (int)dr["Id"];
                conn.Close();
                return emp;
            }
            catch (DbException ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public Employee GetByName(string name)
        {
            string command =
                "SELECT * FROM Employees WHERE Name = " + "'" + name + "'";
            try
            {
                conn.Open();

                DbCommand cmd = factory.CreateCommand(); // Command object
                cmd.CommandText = command;
                cmd.Connection = conn;
                DbDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();
                Employee emp = new Employee((string)dr["Login"], (string)dr["Password"],
                                (string)dr["Name"], (string)dr["Rank"]);
                emp.Id = (int)dr["Id"];
                conn.Close();
                return emp;
            }
            catch (DbException ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        public Employee GetByLogin(string login)
        {
            string command =
                "SELECT * FROM Employees WHERE Login = " + "'"+login+"'";
            try
            {
                conn.Open();

                DbCommand cmd = factory.CreateCommand(); // Command object
                cmd.CommandText = command;
                cmd.Connection = conn;
                DbDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();
                Employee emp = new Employee((string)dr["Login"], (string)dr["Password"],
                                (string)dr["Name"], (string)dr["Rank"]);
                emp.Id = (int)dr["Id"];
                conn.Close();
                return emp;
            }
            catch (DbException ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();
            } 
        }


        public ICollection<Employee> GetAll()
        {
            return null;
        }

    }
}
