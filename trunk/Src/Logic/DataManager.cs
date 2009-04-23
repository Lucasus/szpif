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
        IDatabase database;

		public DataManager(IDatabase database)
		{
			this.database = database;
		}

        public string gridNameToViewName(string gridName)
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

        public ICollection<string> getColumnValuesFromView(string viewName, string columnName)
        {
            DataTable view = database.getView(viewName);
            return DataManager.getValues(view, columnName);
        }
    }
}
