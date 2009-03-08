using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

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
        /// <summary>
        /// Adds the specified employee to repository.
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// 
        public void Add(Employee employee)
        {
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
        public void Remove(Employee employee)
        {
        }

        public Employee GetById(Guid productId)
        {
            return null;
        }

        public Employee GetByName(string name)
        {
            return null;   
        }

        public Employee GetByLogin(string login)
        {
            Employee emp = null;
            try
            {
                conn.Open();

                DbCommand cmd = factory.CreateCommand(); // Command object
                cmd.CommandText = 
                    "SELECT * FROM Employees WHERE Login = " + "'"+login+"'";
                cmd.Connection = conn;
                DbDataReader dr;
                dr = cmd.ExecuteReader();
                dr.Read();
                emp = new Employee((string)dr["Login"], (string)dr["Password"],
                                    (string)dr["Name"], (string)dr["Rank"]);
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

      
        public ICollection<Employee> GetByCategory(string category)
        {
            return null;
        }

    }
}
