using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;
namespace Logic
{
    public class SchemedDataTable
    {
        DataTable schema;
        DataTable table;

        public DataTable Table
        {
            get { return table; }
            set { table = value; }
        }

        public DataTable Schema
        {
            get { return schema; }
            set { schema = value; }
        }

        public SchemedDataTable(DataTable t, DataTable s)
        {
            this.table = t;
            this.schema = s;
        }


    }
    public class DataManager
    {
        Context Context;
        Dictionary<string, SchemedDataTable> views;

        private string gridNameToViewName(string gridName)
        {
            switch (gridName)
            {
                case "EmployeesForAdministrationGridView":
                    return "EmployeeViewForAdministration";
            };
            return null;
        }

        public static List<string> getValues(DataTable dt, string columnName)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < dt.Rows.Count; ++i)
                values.Add(dt.Rows[i][columnName].ToString());
            return values;
        }

        public DataManager(Context c)
        {
            this.Context = c;
            views = new Dictionary<string,SchemedDataTable>();
        }

        public ICollection<string> getColumnValuesFromView(string viewName, string columnName)
        {
            DataTable view = Context.Database.getView(viewName);
            return DataManager.getValues(view, columnName);
        }


        /// <summary>
        /// Uwaga: jeżeli kolumna nazywa się "Id", to ustaw ją
        /// jako readonly
        /// </summary>
        /// <param name="dataGrid">The data grid.</param>
        public DataTable bindToView(DataGridView dataGrid)
        {
            string viewName = gridNameToViewName(dataGrid.Name);
            DataTable schema = new DataTable();
            DataTable viewTable = Context.Database.getView(viewName,schema);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = viewTable;
            List<string> writeableParameters = Context.Database.getWriteableAttributes(viewName);
            for (int i = 0; i < viewTable.Columns.Count; ++i)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = viewTable.Columns[i].ColumnName;
                column.DataPropertyName = viewTable.Columns[i].ColumnName;
                if (schema.Columns[i].DataType.Name == "SqlXml")
                {
                    //                    SqlXml type = viewTable.Columns[i].
                    column.ReadOnly = true;
                    DataGridViewCellStyle helpStyle = new DataGridViewCellStyle();
                    helpStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
                    column.DefaultCellStyle = helpStyle;
                    //                    column.
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    column.ValueType = typeof(SqlXml);
                }
                else if (column.DataPropertyName != "Id")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                else
                    column.Width = 20;
                if (column.DataPropertyName == "Id"
                    || !writeableParameters.Contains(column.Name))
                {
                    DataGridViewCellStyle helpStyle = new DataGridViewCellStyle();
                    helpStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                    column.DefaultCellStyle =helpStyle;
                    column.ReadOnly = true;
                }
                dataGrid.Columns.Add(column);
            }
            views.Add(viewName, new SchemedDataTable(viewTable,schema));
            return schema;
        }

        public void updateView(DataGridView dataGrid)
        {
            string viewName = gridNameToViewName(dataGrid.Name);
            Context.Database.updateView(viewName,views[viewName].Table);
        }
    }
}
