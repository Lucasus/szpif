using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace Logic
{
    public class DataManager
    {
        Context Context;
        Dictionary<string, DataTable> views;

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
            views = new Dictionary<string,DataTable>();
        }

        public ICollection<string> getColumnValuesFromView(string viewName, string columnName)
        {
            DataTable view = Context.Database.getView(viewName);
            return DataManager.getValues(view, columnName);
        }

        public void bindToView(DataGridView dataGrid)
        {
            string viewName = gridNameToViewName(dataGrid.Name);
            DataTable viewTable = Context.Database.getView(viewName);
            dataGrid.AutoGenerateColumns = false;
            dataGrid.DataSource = viewTable;
            for (int i = 0; i < viewTable.Columns.Count; ++i)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.Name = viewTable.Columns[i].ColumnName;
                column.DataPropertyName = viewTable.Columns[i].ColumnName;
                dataGrid.Columns.Add(column);
            }
            views.Add(viewName, viewTable);
        }

        public void updateView(DataGridView dataGrid)
        {
            string viewName = gridNameToViewName(dataGrid.Name);
            Context.Database.updateView(viewName,views[viewName]);
        }
    }
}
