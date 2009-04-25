using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;

namespace Logic
{
	public class ViewToGridManager
	{
		IDatabase database;
		Dictionary<string, SchemedDataTable> views;

		public ViewToGridManager(IDatabase database)
		{
			this.database = database;
			views = new Dictionary<string, SchemedDataTable>();
		}

		/// <summary>
		/// Uwaga: jeżeli kolumna nazywa się "Id", to ustaw ją
		/// jako readonly
		/// </summary>
		/// <param name="dataGrid">The data grid.</param>
        public DataTable reconnect(DataGridView dataGrid)
        {
            DataManager dm = new DataManager(database);
            string viewName = dm.gridNameToViewName(dataGrid.Name);
            DataTable schema = new DataTable();
            DataTable viewTable = database.getView(viewName, schema);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = viewTable;
            return schema;
        }
		public DataTable bindToView(DataGridView dataGrid)
		{
			DataManager dm = new DataManager(database);
			string viewName = dm.gridNameToViewName(dataGrid.Name);
			DataTable schema = new DataTable();
			DataTable viewTable = database.getView(viewName, schema);
			dataGrid.AutoGenerateColumns = false;
			dataGrid.DataSource = viewTable;
			List<string> writeableParameters = database.getWriteableAttributes(viewName);
			for (int i = 0; i < viewTable.Columns.Count; ++i)
			{
				DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
				column.Name = viewTable.Columns[i].ColumnName;
				column.DataPropertyName = viewTable.Columns[i].ColumnName;
				if (schema.Columns[i].DataType.Name == "SqlXml")
				{
                    DataGridViewCellStyle helpStyle = new DataGridViewCellStyle();
                    helpStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
                    column.DefaultCellStyle = helpStyle;
                    column.ReadOnly = true;
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
					column.DefaultCellStyle = helpStyle;
					column.ReadOnly = true;
				}
				dataGrid.Columns.Add(column);
			}
            if (views.ContainsKey(viewName)) views.Remove(viewName);
			views.Add(viewName, new SchemedDataTable(viewTable, schema));
			return schema;
		}

		public void updateView(DataGridView dataGrid)
		{
			DataManager dm = new DataManager(database);
			string viewName = dm.gridNameToViewName(dataGrid.Name);
			database.updateView(viewName, views[viewName].Table);
		}
	}
}
