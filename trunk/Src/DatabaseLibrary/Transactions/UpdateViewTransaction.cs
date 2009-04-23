using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DatabaseLibrary
{
    public class UpdateViewTransaction : SzpifTransaction
    {
        string viewName;
        DataTable viewTable;
        SqlCommand cmd;
        SqlCommand cmd2;
        SqlDataAdapter adapter;
        public UpdateViewTransaction(string viewName, DataTable viewTable)
        {
            this.viewName = viewName;
            this.viewTable = viewTable;
            cmd = new SqlCommand();
            cmd.Connection = (SqlConnection)SzpifDatabase.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "update" + viewName;
            adapter = new SqlDataAdapter();
            adapter.UpdateCommand = cmd;

            cmd2 = new SqlCommand();
            cmd2.Connection = (SqlConnection)SzpifDatabase.Connection;
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "insert" + viewName;
            adapter.InsertCommand = cmd2;

        }

        protected override void execute()
        {
            SqlCommandBuilder.DeriveParameters(cmd);
            SqlCommandBuilder.DeriveParameters(cmd2);
            foreach (SqlParameter param in cmd.Parameters)
                param.SourceColumn = param.ParameterName.Substring(1);
            foreach (SqlParameter param in cmd2.Parameters)
                param.SourceColumn = param.ParameterName.Substring(1);
            adapter.Update(viewTable);
        }
    }
}
