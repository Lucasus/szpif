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
            cmd.Parameters.Add("@Id", SqlDbType.Int, 1, "Id");
            cmd.Parameters.Add("@Login", SqlDbType.NVarChar, 40, "Login");
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 40, "Name");
            cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 40, "EMail");
            cmd.Parameters.Add("@Uprawnienia", SqlDbType.NVarChar, 40, "Uprawnienia");
            adapter = new SqlDataAdapter();
            adapter.UpdateCommand = cmd;
        }

        protected override void execute()
        {
            adapter.Update(viewTable);
            //DbConnection connection = SzpifDatabase.getEmptyConnection();

        }
    }
}
