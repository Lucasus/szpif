using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace DatabaseLibrary
{
    class UpdateViewTransaction : SzpifTransaction
    {
        string viewName;
        DataTable viewTable;
        SqlCommand cmd;
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
        }

        protected override void execute()
        {
            SqlCommandBuilder.DeriveParameters(cmd);
            foreach (SqlParameter param in cmd.Parameters)
                param.SourceColumn = param.ParameterName.Substring(1);
            adapter.Update(viewTable);
        }
    }
}
