using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Xml;
using System.Xml.Linq;
using System.IO;
namespace Logic
{
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
                    return "Employees";
                case "EmployeesForUser":
                    return "EmployeesForUser";
                case "PrzelozeniForSelect":
                    return "PrzelozeniForSelect";
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
            IntegratedView view = database.getView(viewName);
            return DataManager.getValues(view.Table, columnName);
        }

        public ICollection<string> getCurrentUserRoles()
        {
            ICollection<string> roles = new List<string>();
            IntegratedView view = database.getView("EmployeesForUser");
            string help = view.Table.Rows[0]["Roles"].ToString();

            //string help = gridView.Rows[row].Cells[valueBox.Name].Value.ToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(help));
            XElement xml = XElement.Load(new StringReader(help));

            var query = from x in xml.Elements("Item")
                        where (int)x.Attribute("Value") == 1
                        select x;

            foreach (var record in query)
            {
                roles.Add(record.Attribute("Name").Value);
            }
            return roles;
            //return DataManager.getValues(view.Table, columnName);
        }


    }
}
