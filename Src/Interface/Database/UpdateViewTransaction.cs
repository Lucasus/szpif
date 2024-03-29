﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Szpif
{
    public class UpdateViewTransaction : SzpifTransaction
    {
        string viewName;
        DataTable viewTable;
        SqlCommand cmd;
        SqlCommand cmd2;
        SqlCommand cmd3;
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

            cmd3 = new SqlCommand();
            cmd3.Connection = (SqlConnection)SzpifDatabase.Connection;
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "delete" + viewName;//"delete from Employees where Id = @original_id";//"delete" + viewName;
            SqlParameter param = cmd3.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");
            param.SourceVersion = DataRowVersion.Original;
            param.Direction = ParameterDirection.Input;
            adapter.DeleteCommand = cmd3;

        }

        protected override void execute()
        {
            SqlCommandBuilder.DeriveParameters(cmd);
            SqlCommandBuilder.DeriveParameters(cmd2);
            //SqlCommandBuilder.DeriveParameters(cmd3);
            foreach (SqlParameter param in cmd.Parameters)
                param.SourceColumn = param.ParameterName.Substring(1);
            foreach (SqlParameter param in cmd2.Parameters)
                param.SourceColumn = param.ParameterName.Substring(1);
            //foreach (SqlParameter param in cmd3.Parameters)
           // {
           //     param.SourceColumn = param.ParameterName.Substring(1);
           //     param.SourceVersion = DataRowVersion.Original;
           // }
           // cmd3.Parameters[1].ParameterName = "@original_id";
           // cmd3.Parameters[1].SourceColumn = "Id";
//            adapter.
           // viewTable
            adapter.Update(viewTable);
        }
    }
}
