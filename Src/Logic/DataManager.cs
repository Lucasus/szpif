using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Logic
{
    public class DataManager
    {
        Context Context;
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
        }

        public ICollection<string> getColumnValuesFromView(string viewName, string columnName)
        {
            DataTable view = Context.Database.getView(viewName);
            return DataManager.getValues(view, columnName);
        }
    }
}
