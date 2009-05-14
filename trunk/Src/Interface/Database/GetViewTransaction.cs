using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Szpif;

namespace Szpif
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
        private void getTypeInformation(SzpifType column)
        {
            string ctext = "exec getTypeSchema '" + viewName + "', '" + column.Name + "'";
            SqlCommand com = new SqlCommand(ctext, (SqlConnection)selectCommand.Connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable table = new DataTable();
            adapter.Fill(table);
            column.Subtype = table.Rows[0]["Name"].ToString();
            column.Schema = table.Rows[0]["TypeSchema"].ToString();
            column.Additional = table.Rows[0]["Additional"].ToString();
        }

        protected override void execute()
        {
            DataTable help = new DataTable();
            SqlDataAdapter oDA = new SqlDataAdapter(selectCommand.CommandText, (SqlConnection)selectCommand.Connection);
            oDA.ReturnProviderSpecificTypes = true;  
            oDA.FillSchema(help, SchemaType.Mapped);

            SqlCommandBuilder.DeriveParameters(updateCommand);
            SqlCommandBuilder.DeriveParameters(insertCommand);


            oDA.ReturnProviderSpecificTypes = false;
            foreach (DataColumn column in help.Columns)
            {
                SzpifType c = new SzpifType();
                c.Type = column.DataType.Name;
                c.Name = column.ColumnName;
                if (c.Type == "SqlXml") getTypeInformation(c);
                else
                {
                    c.Subtype = c.Type;
                    c.Schema = "";
                }
                SzpifColumn szpifColumn = ColumnFactory.createSzpifColumn(c);
                szpifColumn.CanView = true;
                szpifColumn.Name = c.Name;
                szpifColumn.Type = c;
                view.Columns.Add(column.ColumnName, szpifColumn);
            }

            foreach (SqlParameter column in updateCommand.Parameters)
            {
                string name = column.ParameterName.Substring(1);
                if (!view.Columns.ContainsKey(name))
                {
                    SzpifType c = new SzpifType();
                    c.Type = column.SqlDbType.ToString();
                    c.Name = name;
                    if (column.SqlDbType.ToString() == "Xml")
                        getTypeInformation(c);
                    else
                    {
                        c.Subtype = c.Type;
                        c.Schema = "";
                    }
                    SzpifColumn szpifColumn = ColumnFactory.createSzpifColumn(c);
                    szpifColumn.Name = c.Name;
                    szpifColumn.Type = c;
                    view.Columns.Add(name, szpifColumn);
                }
                if(name != "Id")view.Columns[name].CanUpdate = true;

            }


            foreach (SqlParameter column in insertCommand.Parameters)
            {
                string name = column.ParameterName.Substring(1);
                if (!view.Columns.ContainsKey(name))
                {
                    SzpifType c = new SzpifType();
                    c.Type = column.SqlDbType.ToString();
                    c.Name = name;
                    if (column.SqlDbType.ToString() == "Xml")
                        getTypeInformation(c);
                    else
                    {
                        c.Subtype = c.Type;
                        c.Schema = "";
                    }
                    SzpifColumn szpifColumn = ColumnFactory.createSzpifColumn(c);
                    szpifColumn.Name = c.Name;
                    szpifColumn.Type = c;
                    view.Columns.Add(name, szpifColumn);
                }
                view.Columns[name].CanInsert = true;
            }
            view.Columns.Remove("RETURN_VALUE");           
            oDA.Fill(view.Table); 
        }
    }
}
