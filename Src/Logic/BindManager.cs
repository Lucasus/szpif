using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace Logic
{
	public class BindManager
	{
		IDatabase database;
		Dictionary<string, IntegratedView> views;

		public BindManager(IDatabase database)
		{
			this.database = database;
			views = new Dictionary<string, IntegratedView>();
		}

		/// <summary>
		/// Uwaga: jeżeli kolumna nazywa się "Id", to ustaw ją
		/// jako readonly
		/// </summary>
		/// <param name="dataGrid">The data grid.</param>
        public IntegratedView reconnect(DataGridView dataGrid)
        {           
            DataManager dm = new DataManager(database);
            string viewName = dm.gridNameToViewName(dataGrid.Name);
            IntegratedView view = database.getView(viewName);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = view.Table;
            if (views.ContainsKey(viewName)) views.Remove(viewName);
                views.Add(viewName, view);
            return view;
        }
		public IntegratedView bindToView(DataGridView dataGrid)
		{
            DataManager dm = new DataManager(database);
            string viewName = dm.gridNameToViewName(dataGrid.Name);
            IntegratedView view = database.getView(viewName);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = view.Table;
			List<string> writeableParameters = database.getWriteableAttributes(viewName);
			for (int i = 0; i < view.Table.Columns.Count; ++i)
			{
				DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = view.Table.Columns[i].ColumnName;
                column.DataPropertyName = view.Table.Columns[i].ColumnName;
				if (view.VisibleColumns[i].DataType.Name == "SqlXml")
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
            foreach (SqlParameter column in view.CanUpdate)
            {
                string name = column.ParameterName.Substring(1);
                if (dataGrid.Columns.Contains(name) == false)
                {
                    view.Table.Columns.Add(new DataColumn(name));
                    DataGridViewTextBoxColumn newColumn= new DataGridViewTextBoxColumn();
                    newColumn.Name = name;
                    newColumn.DataPropertyName = name;
                    newColumn.Visible = false;
                    dataGrid.Columns.Add(newColumn);
                }
            }
            if (views.ContainsKey(viewName)) views.Remove(viewName);
			views.Add(viewName, view);
          //  viewTable.PrimaryKey = new DataColumn[] { viewTable.Columns["Id"] };
			return view;
		}

		public void updateView(DataGridView dataGrid)
		{
			DataManager dm = new DataManager(database);
			string viewName = dm.gridNameToViewName(dataGrid.Name);
			database.updateView(viewName, (DataTable)dataGrid.DataSource);//  views[viewName].Table);
		}
	}
}
