using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatabaseLibrary;
using System.Data.SqlClient;

namespace SzpifbaseBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            ViewPermissions permissions = null;
            List<ViewColumn> columns = new List<ViewColumn>();
            IntegratedView employeesView = new IntegratedView(
                "employeesForAdministrationView", columns, permissions); 
            Console.WriteLine("Press Enter to Continue");
            Console.ReadKey();
        }
    }
}
