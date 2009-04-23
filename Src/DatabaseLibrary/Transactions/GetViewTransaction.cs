using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseLibrary
{
    public class GetViewTransaction : SzpifTransaction
    {
        string viewName;
        List<string> columnNames;
        DbCommand cmd;
        DataTable view;
        DataTable schema;

        public DataTable View
        {
            get { return view; }
        }
        
        public GetViewTransaction(string viewName)
        {
            this.view = new DataTable();
            this.schema = null;
            this.columnNames = new List<string>();
            this.viewName = viewName;
            string command = "exec get" + viewName + ";";
            cmd = SzpifDatabase.Factory.CreateCommand();
            cmd.CommandText = command;
            cmd.Connection = SzpifDatabase.Connection;
        }

        public GetViewTransaction(string viewName, DataTable schema) :
            this(viewName)
        {
            this.schema = schema;
        }

        protected override void execute()
        {
            SqlDataAdapter oDA = new SqlDataAdapter(cmd.CommandText, (SqlConnection)cmd.Connection);
            if (schema != null)
            {
                oDA.ReturnProviderSpecificTypes = true;  //UseProviderSpecificType = true;
                oDA.FillSchema(schema, SchemaType.Mapped);
            }
            oDA.ReturnProviderSpecificTypes = false;            
            oDA.Fill(view); 
        }
    }
}
