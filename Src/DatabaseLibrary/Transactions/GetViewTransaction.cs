using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLibrary
{
    class GetViewTransaction : SzpifTransaction
    {
        string viewName;
        List<string> columnNames;
        DbCommand cmd;
        DataTable view;

        public DataTable View
        {
            get { return view; }
        }
        
        public GetViewTransaction(string viewName)
        {
            this.view = null;
            this.columnNames = new List<string>();
            this.viewName = viewName;
            string command = "exec get" + viewName + ";";
            cmd = SzpifDatabase.Factory.CreateCommand();
            cmd.CommandText = command;
            cmd.Connection = SzpifDatabase.Connection;
        }
        protected override void execute()
        {
            view = new DataTable();
            SqlDataAdapter oDA = new SqlDataAdapter(cmd.CommandText, (SqlConnection)cmd.Connection);
            oDA.Fill(view); 
        }
    }
}
