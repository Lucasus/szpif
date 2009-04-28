using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Logic;

namespace DatabaseLibrary
{
    public class GetViewTransaction : SzpifTransaction
    {
        string viewName;
        List<string> columnNames;
        DbCommand selectCommand;
        SqlCommand updateCommand;
        SqlCommand insertCommand;
        IntegratedView view;

        public IntegratedView View
        {
            get { return view; }
        }
        
        public GetViewTransaction(string viewName)
        {
            this.view = new IntegratedView();
            this.view.Table = new DataTable();
            this.columnNames = new List<string>();
            this.viewName = viewName;
            string selectCommandString = "exec get" + viewName + ";";
            selectCommand = SzpifDatabase.Factory.CreateCommand();
            selectCommand.CommandText = selectCommandString;
            selectCommand.Connection = SzpifDatabase.Connection;

            updateCommand = new SqlCommand();
            updateCommand.Connection = (SqlConnection)SzpifDatabase.Connection;
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.CommandText = "update" + viewName;

            insertCommand = new SqlCommand();
            insertCommand.Connection = (SqlConnection)SzpifDatabase.Connection;
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.CommandText = "insert" + viewName;
        }

        protected override void execute()
        {
            DataTable help = new DataTable();
            SqlDataAdapter oDA = new SqlDataAdapter(selectCommand.CommandText, (SqlConnection)selectCommand.Connection);
            oDA.ReturnProviderSpecificTypes = true;  
            oDA.FillSchema(help, SchemaType.Mapped);
            view.VisibleColumns = help.Columns;

            SqlCommandBuilder.DeriveParameters(updateCommand);
            SqlCommandBuilder.DeriveParameters(insertCommand);

            view.CanInsert = insertCommand.Parameters;
            view.CanUpdate = updateCommand.Parameters;

            oDA.ReturnProviderSpecificTypes = false;            
            oDA.Fill(view.Table); 
        }
    }
}
