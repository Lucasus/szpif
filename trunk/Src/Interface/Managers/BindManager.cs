using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Szpif;

namespace Szpif
{
	public class BindManager
	{
        SzpifDatabase database;
		Dictionary<string, IntegratedView> views;

		public BindManager(SzpifDatabase database)
		{
			this.database = database;
			views = new Dictionary<string, IntegratedView>();
		}

        public IntegratedView reconnect(DataGridView dataGrid)
        {           
            DataManager dm = new DataManager(database);
            string viewName = dataGrid.Name;
            IntegratedView view = database.getView(viewName);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = view.Table;
            if (views.ContainsKey(viewName)) views.Remove(viewName);
                views.Add(viewName, view);

            foreach (SzpifColumn column in view.Columns.Values)
                if (column.CanUpdate && view.Table.Columns.Contains(column.Name) == false)
                    view.Table.Columns.Add(new DataColumn(column.Name));
            return view;
        }
		public IntegratedView bindToView(DataGridView dataGrid)
		{
            DataManager dm = new DataManager(database);
            string viewName = dataGrid.Name;
            IntegratedView view = database.getView(viewName);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = view.Table;

            foreach(SzpifColumn column in view.Columns.Values)
                dataGrid.Columns.Add(column.createDataGridViewColumn());
//            dataGrid.Columns[dataGrid.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (SzpifColumn column in view.Columns.Values)
                if (column.CanUpdate && view.Table.Columns.Contains(column.Name) == false)
                    view.Table.Columns.Add(new DataColumn(column.Name));
            
            if (views.ContainsKey(viewName)) views.Remove(viewName);
			views.Add(viewName, view);
			return view;
		}

		public void updateView(DataGridView dataGrid)
		{
			DataManager dm = new DataManager(database);
			string viewName = dataGrid.Name;
			database.updateView(viewName, (DataTable)dataGrid.DataSource);
        }
	}
}
